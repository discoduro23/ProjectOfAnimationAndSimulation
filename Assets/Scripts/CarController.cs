using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject wheelFL;
    public GameObject wheelFR;
    public GameObject wheelBL;
    public GameObject wheelBR;
    public GameObject visibleTierFL;
    public GameObject visibleTierFR;
    public GameObject visibleTierBL;
    public GameObject visibleTierBR;

    public float maxSteerAngle = 30;
    public float motorForce = 50;
    private Rigidbody rb;
    public GameObject centerOfMass;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //add force to wheels based on input
        wheelFL.GetComponent<WheelCollider>().motorTorque = Input.GetAxis("Vertical") * motorForce;
        wheelFR.GetComponent<WheelCollider>().motorTorque = Input.GetAxis("Vertical") * motorForce;

        //steer wheels based on input
        wheelFL.GetComponent<WheelCollider>().steerAngle = Input.GetAxis("Horizontal") * maxSteerAngle;
        wheelFR.GetComponent<WheelCollider>().steerAngle  = Input.GetAxis("Horizontal") * maxSteerAngle;

        //Breaks based on input
        if (Input.GetKey(KeyCode.Space))
        {
            wheelFL.GetComponent<WheelCollider>().brakeTorque = 1000;
            wheelFR.GetComponent<WheelCollider>().brakeTorque = 1000;
            wheelBL.GetComponent<WheelCollider>().brakeTorque = 1000;
            wheelBR.GetComponent<WheelCollider>().brakeTorque = 1000;
        }
        else
        {
            wheelFL.GetComponent<WheelCollider>().brakeTorque = 0;
            wheelFR.GetComponent<WheelCollider>().brakeTorque = 0;
            wheelBL.GetComponent<WheelCollider>().brakeTorque = 0;
            wheelBR.GetComponent<WheelCollider>().brakeTorque = 0;
        }

        //update wheel visuals
        UpdateWheelVisuals(wheelFL, visibleTierFL);
        UpdateWheelVisuals(wheelFR, visibleTierFR);
        UpdateWheelVisuals(wheelBL, visibleTierBL);
        UpdateWheelVisuals(wheelBR, visibleTierBR);
        
    }

    void UpdateWheelVisuals(GameObject wheelCollider, GameObject wheelModel)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetComponent<WheelCollider>().GetWorldPose(out pos, out rot);
        wheelModel.transform.position = pos;
        wheelModel.transform.localPosition += new Vector3(-0.5f, 0, 0);

        // Rotate the wheel so it's upright and facing the correct direction
        wheelModel.transform.rotation = rot;
        wheelModel.transform.rotation *= Quaternion.Euler(0, 90, 0);

        // keep wheel proportions  while rotating
        wheelModel.transform.localScale = new Vector3(wheelModel.transform.localScale.x, wheelModel.transform.localScale.y, wheelModel.transform.localScale.z);



    }
}
