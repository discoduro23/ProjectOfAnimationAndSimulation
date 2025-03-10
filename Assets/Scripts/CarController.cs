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
    private bool releasedAccel;
    private bool releasedBrake;
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

            if (Input.GetAxis("Vertical") < 0.1f)
            {
                releasedAccel = false;
                wheelColliderFL.brakeTorque = 100;
                wheelColliderFR.brakeTorque = 100;
            }
            else if(!releasedAccel)
            {
                releasedAccel = true;
                GameObject sound = SoundManager.instance.CreateSound("Acceleration");
                if (sound != null) sound.transform.position = transform.position;
            }


            //Breaks based on input
            if (Input.GetKey(KeyCode.Space))
            {
                if(releasedBrake)
                {
                    GameObject sound = SoundManager.instance.CreateSound("Brake");
                    if (sound != null) sound.GetComponent<AudioSource>().volume = 0.2f;
                    if (sound != null) sound.transform.position = transform.position;
                    releasedBrake = false;
                }
                
                

                wheelColliderFL.brakeTorque = 1000;
                wheelColliderFR.brakeTorque = 1000;
                wheelColliderBL.brakeTorque = 500;
                wheelColliderBR.brakeTorque = 500;
            }
            else
            {
                wheelColliderFL.brakeTorque = 0;
                wheelColliderFR.brakeTorque = 0;
                wheelColliderBL.brakeTorque = 0;
                wheelColliderBR.brakeTorque = 0;
                releasedBrake = true;

            }


        }
        else
        {
            wheelColliderFL.brakeTorque = 10000;
            wheelColliderFR.brakeTorque = 10000;
            wheelColliderBL.brakeTorque = 10000;
            wheelColliderBR.brakeTorque = 10000;
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

        // Rotate the wheel so it's upright and facing the correct direction
        wheelModel.transform.rotation = rot;
        //wheelModel.transform.rotation *= Quaternion.Euler(0, 180, 0);

        // keep wheel proportions  while rotating
        wheelModel.transform.localScale = new Vector3(wheelModel.transform.localScale.x, wheelModel.transform.localScale.y, wheelModel.transform.localScale.z);



    }
}
