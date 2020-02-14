using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axes : MonoBehaviour

{
    public BaseAtom bA;
    public numElectrons finished;
    public Transform eyes;

    private bool bonded;
    private int bonds;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.002f;
        lineRenderer.positionCount = 2;
        lineRenderer.startColor = new Color(1, 1, 0);
        lineRenderer.endColor = new Color(1, 1, 0);
        bonded = false;
    }

    // Update is called once per frame
    void Update()
    {
        bonds = 0;
        color = new Color(0,0,0);
        foreach (Electron electron in bA.myElectrons)
        {
            if(electron.myBondingSite == this.transform && electron.bonded)
            {
                bonded = true;
                bonds++;
       
            }
            else if (electron.myBondingSite == this.transform && !electron.bonded)
            {
                bonded = false;
            }
        }
        if ((this.transform.position - bA.transform.position).magnitude < 10)
        {
            if (bonded)
            {
                LineRenderer lineRenderer = GetComponent<LineRenderer>();
                var points = new Vector3[2];
                points[0] = this.transform.position;
                points[1] = bA.transform.position;

                if (bonds >= 2)
                {    
                    color = new Color(0, 1, 1);
                    GameObject lr= new GameObject();
                    lr.AddComponent<LineRenderer>();
                    LineRenderer second = lr.GetComponent<LineRenderer>();
                    second.material = new Material(Shader.Find("Sprites/Default"));
                    second.widthMultiplier = 0.002f;
                    second.positionCount = 2;
                    second.startColor = color;
                    second.endColor = color;





                    var points2 = new Vector3[2];

                    
                    Vector3 eyedir = eyes.position - (points[0] + points[1]) / 2.0f;
                    Vector3 dirdir = points[1] - points[0];
                    Vector3 modDir = Vector3.Cross(eyedir, dirdir);
                    modDir.Normalize();




                    points2[0] = points[0] + modDir * 0.01f ;
                    points2[1] = points[1] + modDir * 0.01f ;
                    second.SetPositions(points2);
                    GameObject.Destroy(lr,0.02f);

                }
                if (bonds == 3)
                {
                    GameObject lr = new GameObject();
                    lr.AddComponent<LineRenderer>();
                    LineRenderer third = lr.GetComponent<LineRenderer>();
                    third.material = new Material(Shader.Find("Sprites/Default"));
                    third.widthMultiplier = 0.002f;
                    third.positionCount = 2;
                    third.startColor = color;
                    third.endColor = color;





                    var points2 = new Vector3[2];


                    Vector3 eyedir = eyes.position - (points[0] + points[1]) / 2.0f;
                    Vector3 dirdir = points[1] - points[0];
                    Vector3 modDir = Vector3.Cross(eyedir, dirdir);
                    modDir.Normalize();




                    points2[0] = points[0] + modDir * 0.02f;
                    points2[1] = points[1] + modDir * 0.02f;
                    third.SetPositions(points2);
                    GameObject.Destroy(lr, 0.02f);

                }
                if (color != new Color(0,0,0))
                {
                    lineRenderer.startColor = color;
                    lineRenderer.endColor = color;
                }
                lineRenderer.SetPositions(points);
               
    
            }
            else
            {
                LineRenderer lineRenderer = GetComponent<LineRenderer>();
                var points = new Vector3[2];
                Vector3 point0 = this.transform.position;
                points[1] = bA.transform.position;
                points[0] = 1.5f * (point0 - points[1]) + points[1];
                lineRenderer.SetPositions(points);
            }
        }
        else
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            var points = new Vector3[2];
            points[0] = this.transform.position;
            points[1] = this.transform.position;
            lineRenderer.SetPositions(points);
        }
        if (finished.NumOfElectrons == 8 && !bonded)
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startColor = new Color(1, 0, 0);
            lineRenderer.endColor = new Color(1, 0, 0);

        }
        if (finished.NumOfElectrons != 8 && color == new Color(0,0,0))
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startColor = new Color(1, 1, 0);
            lineRenderer.endColor = new Color(1, 1, 0);
        }
    }

   
}
