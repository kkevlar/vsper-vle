using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class blinking : MonoBehaviour
{
    public TextMeshPro txt;
    public Transform numberOfBonds;
    public Transform picture;
    public numElectrons finished;
    public List<MovingChargedParticle> Atoms;
    public int numOxygen;
    public int numHydrogen;
    public int numNitrogen;
    private int oxy;
    private int hyd;
    private int nit;
    private static int count;
    public bool correct;
    // Start is called before the first frame update
    void Start()
    {
        txt.color = new Color(1, 1, 1);
        count = 0;
        correct = false;
        oxy = 0;
        hyd = 0;
        nit = 0;
        numberOfBonds.position = new Vector3(-11.22f, -30, 10.86f);
        picture.position = new Vector3(-16.52f, -30, 2.45f);
    }

    // Update is called once per frame
    void Update()
    {
        oxy = 0;
        hyd = 0;
        nit = 0;
        foreach (MovingChargedParticle atom in Atoms)
        {
            if (atom.isBonded() && atom.myElectrons.Count == 1)
            {
                hyd++;
            }
            if (atom.isBonded() && atom.myElectrons.Count == 6)
            {
                oxy++;
            }
            if (atom.isBonded() && atom.myElectrons.Count == 5)
            {
                nit++;
            }
        }
        if (oxy == numOxygen && hyd == numHydrogen && nit == numNitrogen)
        {
            correct = true;
        }
        else
        {
            correct = false;
        }
        if(finished.NumOfElectrons == 8 && correct)
        {
            if (count < 10)
            {
                txt.color = new Color(0, 1, 0);
                count++;
            }
            else if (count >= 10 && count < 20)
            {
                txt.color = new Color(1, 1, 1);
                count++;
                
            }
            else
            {
                count = 0;
            }
            numberOfBonds.position = new Vector3(-11.22f, 3.44f, 10.86f);
            picture.position = new Vector3(-14.5f, 4.06f, 7.09f);

    }
        else {
            txt.color = new Color(1, 1, 1);
          
                }
    }
}
