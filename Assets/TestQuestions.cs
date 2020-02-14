using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuestions : MonoBehaviour
{
    public bool correctAns;
    public blinking finished;
    public int placement;
    public Transform hmd;
    private float time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentpos = this.transform.position;

        if ((Time.time-time) > 10.0f && (Time.time - time) < 15.0f)
        {
            this.transform.position = new Vector3(currentpos.x - 0.01f, currentpos.y, currentpos.z);
        } 

        if(Input.GetKey(KeyCode.D) && !correctAns)
        {
            this.transform.position = new Vector3(currentpos.x - 0.5f, currentpos.y, currentpos.z);
        }

    }
}
