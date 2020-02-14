using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//charged particles that move towards the forces pulling them
/* mainly used for atoms
 *    moves towards bonding sites until it attaches to one*/

public class MovingChargedParticle : chargedParticle
{
    public List<chargedParticle> bondingSites; //list of possible bonding sites for the atom
    public Rigidbody mySelfRb;           //rigid body of atom
    public OVRGrabbable myGrabbableScript;
    public bool pause;
    public BaseAtom baseAtom;
    public List<Electron> myElectrons;
    private bool justpurged = true;
    public chargedParticle myBondedBondingsite;
    public int electronsWillShare = 1;
    private Vector3 previousrotation = new Vector3(0, 0, 0);


    public void Start()
    {
        for (int i = 0; i < myElectrons.Count; i++)
        {
            myElectrons[i].owner = mySelfRb.transform;
            myElectrons[i].phase = i * 10;
        }
    }
    /* public void Update()
     {
         if (myGrabbableScript.isGrabbed)
         {
             previousrotation = new Vector3(mySelfRb.transform.localScale.x, mySelfRb.transform.localScale.y, mySelfRb.transform.localScale.z);
         }
         mySelfRb.transform.rotation = Quaternion.Euler(previousrotation);
     }*/

    public override float getCharge()
    {
        Vector3 selfV = mySelfRb.transform.position;
        float chg = charge;
        bool inRangeofBond = false;
        if (myGrabbableScript.isGrabbed)
            pause = false;
        foreach (chargedParticle bS in bondingSites)
        {
            Vector3 bV = bS.gameObject.transform.position;
            if ((baseAtom.numE.NumOfElectrons == 8 || (baseAtom.numE.NumOfElectrons > 5 && myElectrons.Count ==5) || (baseAtom.numE.NumOfElectrons > 6 && myElectrons.Count == 6)) && !(isBonded()))
            {
                bS.charge = 0.3f;
            }
            else
            {
                bS.charge = -0.5f;
            }

            mySelfRb.velocity = new Vector3(mySelfRb.velocity.x * 0.9999f, mySelfRb.velocity.y * 0.9999f, mySelfRb.velocity.z * 0.9999f);


            if ((bV - selfV).magnitude < 0.2)
            {
                mySelfRb.velocity = new Vector3(mySelfRb.velocity.x * 0.8f, mySelfRb.velocity.y * 0.8f, mySelfRb.velocity.z * 0.8f);
                inRangeofBond = true;
                //slows the speed if atom is near a bonding site
            }

            if ((bV - selfV).magnitude < 0.1 && !(baseAtom.numE.NumOfElectrons > 8))
            {
                mySelfRb.velocity = new Vector3(mySelfRb.velocity.x * 0.1f, mySelfRb.velocity.y * 0.1f, mySelfRb.velocity.z * 0.1f);
                setBonded(true, bS);

                if ((bV - selfV).magnitude < 0.05)
                {
                    mySelfRb.velocity = new Vector3(mySelfRb.velocity.x * 0.01f, mySelfRb.velocity.y * 0.01f, mySelfRb.velocity.z * 0.01f);

                    chg = 0;
                }
                // stops movement and attraction to other bonding sites once it touches bonding site
            }

            if (!isBonded() && myBondedBondingsite != null)
            {
                purgeBond(myBondedBondingsite);
                myBondedBondingsite = null;
            }



        }

        if (!inRangeofBond)
        {
            setBonded(false);
        }

        if (pause)
        {
            //  mySelfRb.velocity *= 0;
            chg = 0;
        }

        return chg;
        //if it didn't reach a bonding site, the charge did not change (still attracted to all bonding sites
    }

    public void setBonded(bool bonded, chargedParticle bondingSite)
    {
        setBonded(bonded);

        myBondedBondingsite = bondingSite;

        if (bonded)
        {

            float count = 0;

            for (int i = 0; i < baseAtom.myElectrons.Count; i++)
            {
                if (null == baseAtom.myElectrons[i].myBondingSite)
                    continue;

                Vector3 diff = bondingSite.transform.position - baseAtom.myElectrons[i].myBondingSite.position;
                if (diff.magnitude < 0.001)
                {
                    baseAtom.myElectrons[i].bondee = this.transform;
                    baseAtom.myElectrons[i].bonded = true;
                    baseAtom.myElectrons[i].bondedPhase = count;
                    if (justpurged)
                        baseAtom.myElectrons[i].time = 0;
                    if (myElectrons.Count >= 1 && electronsWillShare >= 1)
                        baseAtom.myElectrons[i].bondeeOrbitDistance = myElectrons[0].ownerOrbitDistance;
                    count += 5;
                }
                else if (baseAtom.myElectrons[i].bondee == this.transform)
                {
                    baseAtom.myElectrons[i].bondee = null;
                    baseAtom.myElectrons[i].bonded = false;
                }
            }


            myElectrons[0].bondee = baseAtom.transform;
            myElectrons[0].bonded = true;
            myElectrons[0].bondedPhase = count + 180;
            if (justpurged)
                myElectrons[0].time = 0;
            count += 5;


            if (myElectrons.Count >= 4 && electronsWillShare >= 2)
            {
                myElectrons[3].bondee = baseAtom.transform;
                myElectrons[3].bonded = true;
                myElectrons[3].bondedPhase = count + 180;
                if (justpurged)
                    myElectrons[3].time = 0;
                count += 5;
            }
            if (myElectrons.Count >= 4 && electronsWillShare >= 3)
            {
                myElectrons[2].bondee = baseAtom.transform;
                myElectrons[2].bonded = true;
                myElectrons[2].bondedPhase = count + 180;
                if (justpurged)
                    myElectrons[2].time = 0;
                count += 5;
            }

            justpurged = false;

        }
    }

    public void purgeBond(chargedParticle bondingSite)
    {
        justpurged = true;

        for (int i = 0; i < myElectrons.Count; i++)
        {
            myElectrons[i].bonded = false;
        }

        for (int i = 0; i < baseAtom.myElectrons.Count; i++)
        {
            if (null == baseAtom.myElectrons[i].myBondingSite)
                continue;

            Vector3 diff = bondingSite.transform.position - baseAtom.myElectrons[i].myBondingSite.position;
            if (diff.magnitude < 0.001)
            {
                baseAtom.myElectrons[i].bondee = this.transform;
                baseAtom.myElectrons[i].bonded = false;
            }
        }

        for (int i = 0; i < baseAtom.myElectrons.Count; i++)
        {
            if (null == baseAtom.myElectrons[i].tempBondingSite)
                continue;

            Vector3 diff = bondingSite.transform.position - baseAtom.myElectrons[i].tempBondingSite.position;
            if (diff.magnitude < 0.001)
            {
                baseAtom.myElectrons[i].bondee = this.transform;
                baseAtom.myElectrons[i].bonded = false;
            }
        }

    }





}


