using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//stationary charged particles

/*mainly used for bonding sites
 *  attracts atoms to sites for bonding
 */
public class chargedParticle : MonoBehaviour
{
    public float charge;
    private bool Bonded = false;
    public bool doubleBonding = false;
    public bool tripleBonding = false;

    public virtual float getCharge()
    {
        return charge;
    }

    public bool isBonded()
    {
        return Bonded;
    }

    public void setBonded(bool bonded)
    {
        Bonded = bonded;
    }
}
