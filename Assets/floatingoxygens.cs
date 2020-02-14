using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingoxygens : MonoBehaviour
{
    public BaseAtom bA;
    public BaseAtom self;
    public MovingChargedParticle atom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float min = 100000000;
        Transform closestBS = null;
        if (atom.isBonded())
        {
           foreach (Transform bondingSite in self.bondingSites)
            {
                if ((bondingSite.position - bA.transform.position).magnitude < min)
                {
                    min = (bondingSite.position - bA.transform.position).magnitude;
                    closestBS = bondingSite;
                }
               
            }

            

        }
        atom.mySelfRb.angularVelocity = atom.mySelfRb.angularVelocity * 0.8f;

    }
}
