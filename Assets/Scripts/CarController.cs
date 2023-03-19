using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject wheelFL;
    public GameObject wheelFR;
    public GameObject wheelBL;
    public GameObject wheelBR;

    public float maxSteerAngle = 30;
    public float motorForce = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //add force to wheels based on input
        wheelFL.GetComponent<WheelCollider>().motorTorque = Input.GetAxis("Vertical") * motorForce;
        wheelFR.GetComponent<WheelCollider>().motorTorque = Input.GetAxis("Vertical") * motorForce;

        //steer wheels based on input
        wheelFL.GetComponent<WheelCollider>().steerAngle = Input.GetAxis("Horizontal") * maxSteerAngle;
        wheelFR.GetComponent<WheelCollider>().steerAngle = Input.GetAxis("Horizontal") * maxSteerAngle;
    }
}
