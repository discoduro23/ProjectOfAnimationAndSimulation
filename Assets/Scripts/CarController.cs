using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public bool carMode = true;
    public GameObject wheelFL;
    public GameObject wheelFR;
    public GameObject wheelBL;
    public GameObject wheelBR;
    public GameObject visibleTierFL;
    public GameObject visibleTierFR;
    public GameObject visibleTierBL;
    public GameObject visibleTierBR;
    private WheelCollider wheelColliderFL;
    private WheelCollider wheelColliderFR;
    private WheelCollider wheelColliderBL;
    private WheelCollider wheelColliderBR;

    public float maxSteerAngle = 30;
    public float motorForce = 50;
    private Rigidbody rb;
    public GameObject centerOfMass;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.localPosition;
        wheelColliderFL = wheelFL.GetComponent<WheelCollider>();
        wheelColliderFR = wheelFR.GetComponent<WheelCollider>();
        wheelColliderBL = wheelBL.GetComponent<WheelCollider>();
        wheelColliderBR = wheelBR.GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(carMode)
        {
            //add force to wheels based on input
            wheelFL.GetComponent<WheelCollider>().motorTorque = Input.GetAxis("Vertical") * motorForce;
            wheelFR.GetComponent<WheelCollider>().motorTorque = Input.GetAxis("Vertical") * motorForce;

            //steer wheels based on input
            wheelFL.GetComponent<WheelCollider>().steerAngle = Input.GetAxis("Horizontal") * maxSteerAngle;
            wheelFR.GetComponent<WheelCollider>().steerAngle = Input.GetAxis("Horizontal") * maxSteerAngle;

            //Breaks based on input
            if (Input.GetKey(KeyCode.Space))
            {
                wheelColliderFL.brakeTorque = 1000;
                wheelColliderFR.brakeTorque = 1000;
                wheelColliderBL.brakeTorque = 1000;
                wheelColliderBR.brakeTorque = 1000;
            }
            else
            {
                wheelColliderFL.brakeTorque = 0;
                wheelColliderFR.brakeTorque = 0;
                wheelColliderBL.brakeTorque = 0;
                wheelColliderBR.brakeTorque = 0;
            }

            //update wheel visuals
            UpdateWheelVisuals(wheelFL, visibleTierFL);
            UpdateWheelVisuals(wheelFR, visibleTierFR);
            UpdateWheelVisuals(wheelBL, visibleTierBL);
            UpdateWheelVisuals(wheelBR, visibleTierBR);
        }
        else
        {
            wheelColliderFL.brakeTorque = 10000;
            wheelColliderFR.brakeTorque = 10000;
            wheelColliderBL.brakeTorque = 10000;
            wheelColliderBR.brakeTorque = 10000;
        }
        
        
    }

    void UpdateWheelVisuals(GameObject wheelCollider, GameObject wheelModel)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetComponent<WheelCollider>().GetWorldPose(out pos, out rot);
        wheelModel.transform.position = pos;

        // Rotate the wheel so it's upright and facing the correct direction
        wheelModel.transform.rotation = rot;
        //wheelModel.transform.rotation *= Quaternion.Euler(0, 180, 0);

        // keep wheel proportions  while rotating
        wheelModel.transform.localScale = new Vector3(wheelModel.transform.localScale.x, wheelModel.transform.localScale.y, wheelModel.transform.localScale.z);



    }
}
