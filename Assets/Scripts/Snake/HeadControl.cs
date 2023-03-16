using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadControl : MonoBehaviour
{

    public int torqueForce;
    public int steer;
    private int steerVariable;

    public WheelCollider wheelR;
    public WheelCollider wheelF;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //if w pressed then move forward
        if (Input.GetKey(KeyCode.W))
        {
            wheelR.motorTorque = torqueForce;
            wheelF.motorTorque = torqueForce;
        }

        //if s pressed then move backward
        if (Input.GetKey(KeyCode.S))
        {
            wheelR.motorTorque = -torqueForce;
            wheelF.motorTorque = -torqueForce;
        }

        //if a pressed then move left
        if (Input.GetKey(KeyCode.A))
        {
            wheelR.steerAngle = steerVariable;
            wheelF.steerAngle = -steerVariable;
        }

        //if d pressed then move right
        if (Input.GetKey(KeyCode.D))
        {
            wheelR.steerAngle = -steerVariable;
            wheelF.steerAngle = steerVariable;
        }

        //if no key pressed then stop
        if (!Input.anyKey)
        {
            wheelR.motorTorque = 0;
            wheelF.motorTorque = 0;
            wheelF.steerAngle = 0;
            wheelR.steerAngle = 0;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            wheelR.brakeTorque = 100;
            wheelF.brakeTorque = 100;
        }
        else
        {
            wheelR.brakeTorque = 0;
            wheelF.brakeTorque = 0;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            steerVariable = steer * 4;
        }
        else
        {
            steerVariable = steer;
        }


    }
}
