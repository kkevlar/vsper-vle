using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private float cycleInterval = 0.01f;
    private List<chargedParticle> chargedParticles;
    private List<MovingChargedParticle> movingChargedParticles;
    public Rigidbody baseatom;
    public Transform hmd;
    private float spacingz = -0.6f;
    private float spacingx = -0.5f;
    

    private void Start()
    {
        chargedParticles = new List<chargedParticle>(FindObjectsOfType<chargedParticle>());
        movingChargedParticles = new List<MovingChargedParticle>(FindObjectsOfType<MovingChargedParticle>());
        foreach (MovingChargedParticle mcp in movingChargedParticles)
        {
            StartCoroutine(Cycle(mcp));
        }

    }
    


    public IEnumerator Cycle(MovingChargedParticle mcp)
    {
        bool isFirst = true;
        while (true)
        {
            if (isFirst)
            {
                isFirst = false;
                yield return new WaitForSeconds(Random.Range (0,cycleInterval));
            }
            ApplyMagneticForce(mcp);
            yield return new WaitForSeconds(cycleInterval);
        }
    }

    private void ApplyMagneticForce(MovingChargedParticle mcp)
    {
        Vector3 newForce = Vector3.zero;
         
        //adds all the forces of all charges on moving charged particles
        foreach (chargedParticle cp in chargedParticles)
        {
            //if another atom is on the bonding site, then the particles will stop being attracted to the already used site.
            bool usedBondingSite = false;
           foreach (MovingChargedParticle other in movingChargedParticles)
            {
                if ((other.transform.position - cp.gameObject.transform.position).magnitude <= 0.2 && other != mcp)
                {
                    usedBondingSite = true;
                }
            }
           if (mcp == cp || usedBondingSite)
                continue;

           //adds the forces of the bonding sites.
           float distance = Vector3.Distance(mcp.transform.position, cp.gameObject.transform.position);

            float force = 1000 * mcp.getCharge() * cp.getCharge() / Mathf.Pow(distance, 2);

            Vector3 direction = mcp.transform.position - cp.transform.position;
            Vector3 baseatomDir = baseatom.transform.position - mcp.transform.position;
            
            direction.Normalize();

            if (baseatomDir.magnitude > 3.5f)
            {



                //mcp.mySelfRb.velocity = new Vector3(mcp.mySelfRb.velocity.x, mcp.mySelfRb.velocity.y  * 1.1f + 0.05f, mcp.mySelfRb.velocity.z);
                mcp.mySelfRb.velocity *= 1.005f;
            }

            if (baseatomDir.magnitude > 100f)
            {
                mcp.mySelfRb.velocity *= 0;
                Vector3 myVec = (hmd.position - baseatom.transform.position - new Vector3(0,0.5f,0));
                //Vector3 leftOf = Quaternion.AngleAxis(Random.Range(-1.0f,1.0f) > 0 ? 90 : 270 , new Vector3(0,1,0)) * (myVec.normalized * 0.75f);
                mcp.mySelfRb.transform.position = hmd.position + new Vector3(spacingx, -0.7f, spacingz);
                spacingz += 0.2f;
                if (spacingz > 0.4f)
                {
                    spacingz = -0.6f;
                    spacingx += 0.2f;
                    if(spacingx > 0)
                    {
                        spacingx = -0.5f;
                    }
                }
                //mcp.mySelfRb.transform.position = (myVec * 0.6f) +  baseatom.transform.position + leftOf + new Vector3(Random.Range(-0.4f, 0.4f), -0.3f + Random.Range(-0.3f, 0.3f), Random.Range(-0.4f, 0.4f)) ;
                mcp.pause = true;
            }

            newForce += force * direction * cycleInterval;

            if (float.IsNaN(newForce.x))
            {
                newForce = Vector3.zero;
            }

            mcp.mySelfRb.AddForce(newForce);
        }
    }
}
