using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float maxSpeedH = 10.0f;
    public float accelH = 2f;
    public float maxSpeedV = 10.0f;
    public float accelV = 2f;
    public float forceBreak = 2f;
    public float currentHSpeed;
    public float currentVSpeed;
    public GameObject TurretBase;
    public GameObject Turret;
    public GameObject Turret2;
    public GameObject TurretBarrel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //control the turret with the inputs with acceleration

        if (Input.GetAxis("Horizontal") > 0)
        {
            currentHSpeed = Mathf.MoveTowards(currentHSpeed, maxSpeedH, accelH * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            currentHSpeed = Mathf.MoveTowards(currentHSpeed, -maxSpeedH, accelH * Time.deltaTime);
        }
        else
        {
            currentHSpeed = Mathf.MoveTowards(currentHSpeed, 0,forceBreak * accelH * Time.deltaTime);
        }

        if(Turret2.transform.rotation.x < 30 && Turret2.transform.rotation.x > -10)
        if (Input.GetAxis("Vertical") > 0)
        {
            currentVSpeed = Mathf.MoveTowards(currentVSpeed, maxSpeedV, accelV * Time.deltaTime);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            currentVSpeed = Mathf.MoveTowards(currentVSpeed, -maxSpeedV, accelV * Time.deltaTime);
        }
        else
        {
            currentVSpeed = Mathf.MoveTowards(currentVSpeed, 0, forceBreak * accelV * Time.deltaTime);
        }

        Turret.transform.Rotate(0, currentHSpeed, 0, Space.Self);
        Turret2.transform.Rotate(currentVSpeed, 0, 0, Space.Self);

    }
}