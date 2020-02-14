using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraElectrons : MonoBehaviour
{
    public List<Electron> myExtraElectrons;
    public BaseAtom bA;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<Transform> avils = new List<Transform>();
        foreach (Electron extra in myExtraElectrons)
        {
            foreach (Electron electron in bA.myElectrons)
            {
                if (!electron.bonded && electron.myBondingSite != null && extra != electron)
                {
                    avils.Add(electron.myBondingSite);
                }
            }
        }


        int count = 0;

        foreach (Electron extra in myExtraElectrons)
        {
            if (!extra.bonded)
            {
                extra.tempBondingSite = avils[(count++) % avils.Count];
            }

        }

    }
}
