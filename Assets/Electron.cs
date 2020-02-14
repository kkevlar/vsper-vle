using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : MonoBehaviour
{
    public float ownerOrbitDistance = 0.25f;
    public float bondeeOrbitDistance = 0.15f;
    private Vector3 randPt;
    public Transform owner;
    public Transform myPos;
    public Transform bondee;
    public bool bonded;
    public Transform myBondingSite;
    public bool takeUpBondingSite = true;
    public float time;
    public Vector3 orbitDir = new Vector3(1, 100, -423);
    public float speed = 2;
    public float travelDegreesOwner = 120;
    public float travelDegreesBondee = 120;
    public float loopTimePercent = 0.7f;
    public float phase = 0.0f;
    private bool lastBondProcessed = false;
    public float bondedPhase;
    public Transform tempBondingSite;

    public void Start()
    {
        randPt = new Vector3(10, 10, 10);//new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f));

    }

    Vector3 calcPlaneNormal(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 side1 = b - a;
        Vector3 side2 = c - a;

        return Vector3.Cross(side1, side2).normalized;
    }

    private Vector3 bondedSelfPos0to1(Vector3 orbitMax, Vector3 normal, float progress)
    {
        return owner.position + (Quaternion.AngleAxis((progress * (2 * travelDegreesOwner)) - travelDegreesOwner,  normal) * orbitMax);
    }

    private Vector3 bondedBondedPos0to1(Vector3 orbitMax, Vector3 normal, float progress)
    {
        return bondee.position + (Quaternion.AngleAxis((progress * (2 * travelDegreesBondee)) - travelDegreesBondee,  -1 * normal) * orbitMax);
    }

    private float singleLoopTime()
    {
        return (loopTimePercent / 2.0f);
    }

    private float singleTransferTime()
    {
        return (1 - (loopTimePercent)) / 2.0f;
    }
    float map(float val, float in_min, float in_max, float out_min, float out_max)
    {
        return (val - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    public void Update()
    {
        if (!bonded)
        {
            lastBondProcessed = false;
            Vector3 orbitDefault = (orbitDir.normalized * ownerOrbitDistance);
          
            if (takeUpBondingSite && (myBondingSite != null || tempBondingSite != null))
            {
                if (null != myBondingSite)
                    tempBondingSite = myBondingSite;

                Vector3 oof = tempBondingSite.position - owner.position;
                Vector3 rotOrbitDir = owner.position + (Quaternion.AngleAxis(Time.time * speed*  30.0f, oof) * (orbitDir + owner.position));
                orbitDefault = (rotOrbitDir.normalized * ownerOrbitDistance);

               
                               

                Vector3 normal = calcPlaneNormal(
          owner.position, owner.position + orbitDefault, tempBondingSite.position);

                Vector3 intermediatePos = owner.position + (Quaternion.AngleAxis(getTime() * 4, normal) * orbitDefault);
                Vector3 ownerToBondSite = (tempBondingSite.position - owner.position);
                Vector3 bulgeDirection = Vector3.Cross(ownerToBondSite, normal).normalized;
                float bondingSiteExtendDot = Vector3.Dot((intermediatePos - owner.position).normalized, ownerToBondSite.normalized);
                float bulgeDot = Vector3.Dot((intermediatePos - owner.position), bulgeDirection);
                float bulgeFactor = 1.0f;

                Vector3 result = intermediatePos;
                if (bondingSiteExtendDot > 0)
                {
                    result += ownerToBondSite * bondingSiteExtendDot;


                    /*  if (bondingSiteExtendDot < 0.05)
                          bulgeFactor = 0.3f;*/

                    result += bulgeDirection * bondingSiteExtendDot * bulgeDot * bulgeFactor;
                }
                else
                    result += bulgeDirection * bondingSiteExtendDot * bulgeDot * bulgeFactor * 0.2f;





                myPos.position = result;
               
            }
            else
            {
                Vector3 normal = calcPlaneNormal(
          owner.position, owner.position + orbitDefault, randPt);

                Vector3 intermediatePos = owner.position + (Quaternion.AngleAxis(getTime() * 2, normal) * orbitDefault);
                myPos.position = intermediatePos;
            }
        }
        else if (bonded)
        {
           /* if (!lastBondProcessed)
            {
                randPt = myPos.position;
                lastBondProcessed = true;
            }*/
            Vector3 normal = calcPlaneNormal(owner.position, randPt, bondee.position);
            Vector3 selfOrbitBase = ((owner.position - bondee.position).normalized * (ownerOrbitDistance));
            Vector3 bondeeOrbitBase = ((bondee.position - owner.position).normalized * (bondeeOrbitDistance));

            if (getTime() < (singleLoopTime() * 360.0f))
            {

                myPos.position = bondedSelfPos0to1(selfOrbitBase, normal, getTime() / (singleLoopTime() * 360.0f));
            }
            else if (getTime() < ((singleLoopTime() + singleTransferTime()) * 360))
            {
                Vector3 start = bondedSelfPos0to1(selfOrbitBase, normal, 1.0f);
                Vector3 end = bondedBondedPos0to1(bondeeOrbitBase, normal, 0.0f);

                float factor = ((1.0f / (singleTransferTime() * 360.0f)) * (getTime() - (singleLoopTime() * 360.0f)));

                factor = map(
                    getTime(),
                    (singleLoopTime() * 360.0f),
                    (singleLoopTime() + singleTransferTime()) * 360,
                    0,
                    1);


                Vector3 dir = (end - start);
                myPos.position = (dir * factor) + start;
            }
            else if (getTime() < ((2 * singleLoopTime() + singleTransferTime()) * 360.0f))
            {
                myPos.position = bondedBondedPos0to1(bondeeOrbitBase, normal, (getTime() - ((singleLoopTime() + singleTransferTime()) * 360.0f)) / (singleLoopTime() * 360.0f));
            }
            else
            {
                Vector3 start = bondedBondedPos0to1(bondeeOrbitBase, normal, 1.0f);
                Vector3 end = bondedSelfPos0to1(selfOrbitBase, normal, 0.0f);

                float factor = ((1.0f / (singleTransferTime() * 360.0f)) * (getTime() - ((1.0f - singleTransferTime()) * 360.0f)));

                factor = map(
                    getTime(),
                    ((2 * singleLoopTime() + singleTransferTime()) * 360.0f),
                    360,
                    0,
                    1);

                myPos.position = ((end - start) * factor) + start;
            }
        }
        time += speed;
    }

    float getTime()
    {
        while (time > 360)
            time -= 360;
        float result = time + (bonded ? bondedPhase : phase);
        while (result < 0)
            result += 360;
        while (result > 360)
        {
            result -= 360;
           

        }
        return result;
    }
}
