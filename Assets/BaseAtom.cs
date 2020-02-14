using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAtom : MonoBehaviour
{
    public List<Transform> bondingSites; // spots that the other atoms can bond to.
    public Rigidbody mySelfRb; //Base atom rigidBody
    public float radius; // how far the bonding spots should be
    public int numSites = 4;
    public List<Electron> myElectrons;
    public numElectrons numE;
    public Transform hmd;
    private float time;


    public void Start()
    {
        if (hmd != null)
        {
            //mySelfRb.transform.position = new Vector3(-0.5f, -0.3f, 0.5f) + hmd.position;
            mySelfRb.transform.position = new Vector3(-0.3f, -0.1f, 0.7f) + hmd.position;
        }
        changeSites();
        for (int i = 0; i < myElectrons.Count; i++)
        {

            myElectrons[i].phase = 10 * (i + 1);
            myElectrons[i].time = 0;

            if (i < bondingSites.Count)
            {
                myElectrons[i].myBondingSite = bondingSites[i].transform;
            }
        }
        time = Time.time;
    }

    public void Update()
    {

        List<Transform> avils = new List<Transform>();
        if (numE.NumOfElectrons == 8)
        {
            foreach (Electron electron in myElectrons)
            {
                if (!electron.bonded && electron.myBondingSite != null)
                {
                    avils.Add(electron.myBondingSite);
                }
            }
        }
        int count = 0;

        foreach (Electron electron in myElectrons)
        {
            if (!electron.bonded && numE.NumOfElectrons == 8)
            {
                //electron.takeUpBondingSite = true;
                if (electron.myBondingSite == null)
                {
                    electron.tempBondingSite = avils[(count++) % avils.Count];
                }
            }

        }
     
    }
    public Vector3 calcPlaneNormal(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 side1 = b - a;
        Vector3 side2 = c - a;
        return Vector3.Cross(side1, side2).normalized;
    }

    public void changeSites(Vector3 keepPoint)
    {
        if (numSites == 4)
        {
            changeSites4();
        }
        else if (numSites == 3)
        {
            changeSites3(keepPoint);
        }
        else if (numSites == 2)
        {
            changeSites2(keepPoint);
        }
    }

    public void changeSites()
    {
        if (numSites == 4)
        {
            changeSites4();
        }
        else if (numSites == 3)
        {
            changeSites3();
        }
        else if (numSites == 2)
        {
            changeSites2();
        }
    }

    public void changeSites4()
    {
        bondingSites[0].transform.position = mySelfRb.transform.position + new Vector3(-1f, -1f, -1f) * radius;
        bondingSites[1].transform.position = mySelfRb.transform.position + new Vector3(-1f, 1f, 1f) * radius;
        bondingSites[2].transform.position = mySelfRb.transform.position + new Vector3(1f, 1f, -1f) * radius;
        bondingSites[3].transform.position = mySelfRb.transform.position + new Vector3(1f, -1f, 1f) * radius;
        for (int i = 0; i < myElectrons.Count; i++)
        {


            myElectrons[i].phase = 10 * (i + 1);
            myElectrons[i].time = 0;

            if (i < bondingSites.Count)
            {
                myElectrons[i].myBondingSite = bondingSites[i].transform;
            }
        }

    }

    public void changeSites3(Vector3 keepPoint)
    {
        Vector3 normal = calcPlaneNormal(keepPoint, mySelfRb.transform.position, new Vector3(1, 1, 1));
        Vector3 fromCenter = (keepPoint - mySelfRb.transform.position);
        Quaternion rot = Quaternion.AngleAxis(120, normal);

        bondingSites[0].transform.position = mySelfRb.transform.position + fromCenter;
        bondingSites[1].transform.position = mySelfRb.transform.position + (rot * fromCenter);
        bondingSites[2].transform.position = mySelfRb.transform.position + (rot * (rot * fromCenter));
        bondingSites[3].transform.position = new Vector3(15, 15, 15);
        for (int i = 0; i < myElectrons.Count; i++)
        {


            myElectrons[i].phase = 10 * (i + 1);
            myElectrons[i].time = 0;

            if (i < bondingSites.Count)
            {
                myElectrons[i].myBondingSite = bondingSites[i].transform;
            }
        }
        for (int i = 0; i < myElectrons.Count; i++)
        {

            if (myElectrons[i].myBondingSite == bondingSites[3].transform)
            {
                myElectrons[i].myBondingSite = bondingSites[0].transform;

            }
        }

    }

    public void changeSites2(Vector3 keepPoint)
    {
        Vector3 fromCenter = keepPoint - mySelfRb.transform.position;

        bondingSites[0].transform.position = mySelfRb.transform.position + fromCenter;
        bondingSites[1].transform.position = mySelfRb.transform.position - fromCenter;
        bondingSites[2].transform.position = new Vector3(15, 15, 15);
        bondingSites[3].transform.position = new Vector3(15, 15, 15);
        for (int i = 0; i < myElectrons.Count; i++)
        {

            if (myElectrons[i].myBondingSite == bondingSites[2].transform)
            {
                myElectrons[i].myBondingSite = bondingSites[1].transform;

            }
        }
    }

    public void changeSites2triple(Vector3 keepPoint)
    {
        Vector3 fromCenter = keepPoint - mySelfRb.transform.position;

        bondingSites[0].transform.position = mySelfRb.transform.position + fromCenter;
        bondingSites[1].transform.position = mySelfRb.transform.position - fromCenter;
        bondingSites[2].transform.position = new Vector3(15, 15, 15);
        bondingSites[3].transform.position = new Vector3(15, 15, 15);
        for (int i = 0; i < myElectrons.Count; i++)
        {

            if (myElectrons[i].myBondingSite == bondingSites[2].transform || myElectrons[i].myBondingSite == bondingSites[3].transform)
            {
                myElectrons[i].myBondingSite = bondingSites[0].transform;

            }
        }
    }
    public void changeSites2()
    {
        changeSites2(mySelfRb.transform.position + new Vector3(1f, -1f, 1f) * radius);
    }

    public void changeSites3()
    {
        changeSites3(mySelfRb.transform.position + new Vector3(1f, -1f, 1f) * radius);
    }

}