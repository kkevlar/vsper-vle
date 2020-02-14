using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tailScript : MonoBehaviour
{
    public Electron electron;
    public TrailRenderer trail;

    // Update is called once per frame
    void Update()
    {
        if (electron.bonded)
        {
            trail.startColor = new Color(0, 1, 0, 0);
        }
        else
        {
            trail.startColor = new Color(1, 1, 1, 0);
        }
    }
}
