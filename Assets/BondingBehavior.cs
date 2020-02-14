using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondingBehavior : MonoBehaviour
{
    public BaseAtom bA;
    public BaseAtom selfBaseAtom;
    public MovingChargedParticle atom;
    private bool previousBonded;
    // Start is called before the first frame update
    void Start()
    {
        previousBonded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (atom.doubleBonding)
        {
            if (previousBonded != atom.isBonded() && bA.numSites != 2)
            {
                previousBonded = atom.isBonded();
                if (atom.isBonded() && bA.numSites != 2)
                {
                    bA.numSites--;

                    bA.changeSites(atom.mySelfRb.transform.position);

                    selfBaseAtom.numSites--;
                    selfBaseAtom.changeSites(bA.transform.position);

                }
                else
                {
                    bA.numSites++;
                    bA.changeSites();
                    selfBaseAtom.numSites++;
                    selfBaseAtom.changeSites();
                }

            }
            if (bA.numSites == 2 && (Vector3.Distance(atom.mySelfRb.transform.position, bA.mySelfRb.transform.position) > 5))
            {
                previousBonded = atom.isBonded();
                bA.numSites++;
                bA.changeSites();
                selfBaseAtom.numSites++;
                selfBaseAtom.changeSites();
            }
        }
        if (atom.tripleBonding)
        {
            if (previousBonded != atom.isBonded())
            {
                previousBonded = atom.isBonded();
                if (atom.isBonded() && bA.numSites != 2)
                {
                    bA.numSites-= 2;
                    bA.changeSites2triple(atom.mySelfRb.transform.position);

                    selfBaseAtom.numSites-= 2;
                    selfBaseAtom.changeSites(bA.transform.position);

                }
                else
                {
                    bA.numSites+=2;
                    bA.changeSites();
                    selfBaseAtom.numSites+=2;
                    selfBaseAtom.changeSites();
                }
            }
        }

    }
}
