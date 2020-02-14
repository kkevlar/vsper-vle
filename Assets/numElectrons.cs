using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class numElectrons : MonoBehaviour
{
    public chargedParticle atom;
    public numElectrons bAtxt;
    public int NumOfElectrons;
    public TextMeshPro txt;
    private bool previousBonded;
    private int electrons = 1;
    // Start is called before the first frame update
    void Start()
    {
        txt.text = NumOfElectrons.ToString();
        txt.transform.position = new Vector3(atom.gameObject.transform.position.x + 0.1f * atom.gameObject.transform.localScale.x, atom.gameObject.transform.position.y, atom.gameObject.transform.position.z);
        txt.transform.rotation = Quaternion.Euler(new Vector3(0, -92, 0));
        previousBonded = false;
        txt.color = new Color(0, 0, 0, 1);
        if (atom.doubleBonding)
        {
            electrons = 2;
        }
        if (atom.tripleBonding)
        {
            electrons = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (previousBonded != atom.isBonded())
        {
            previousBonded = atom.isBonded();
            if (atom.isBonded())
            {
                bAtxt.NumOfElectrons+= electrons;
                NumOfElectrons += electrons;
            }
            else
            {
                txt.color = new Color(0, 0, 0, 1);
                bAtxt.NumOfElectrons -= electrons;
                NumOfElectrons -= electrons;
            }
            
        }
        if (NumOfElectrons == 2 || NumOfElectrons == 8)
        {
            txt.color = new Color(1, 0, 0, 1);
        }
        else
        {
            txt.color = new Color(0, 0, 0, 1);
        }
        txt.text = NumOfElectrons.ToString();
    }
}
