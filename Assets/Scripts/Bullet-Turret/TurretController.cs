using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public bool turretMode = true;
    public float maxSpeedH = 10.0f;
    public float accelH = 2f;
    public float maxSpeedV = 10.0f;
    public float accelV = 2f;

    public float FovUpAndgle = 30;
    public float FovDownAngle = 30;
    public float forceBreak = 2f;
    public float currentHSpeed;
    public float currentVSpeed;
    public GameObject TurretBase;
    public GameObject Turret;
    public GameObject Turret2;
    public GameObject TurretBarrel;

    public GameObject bullet;
    public GameObject bulletsParent;
    public GameObject bulletSpawn;
    public float bulletSpeed = 1000f;
    public int bulletCount = 10;
    private GameObject[] bullets;

    // Start is called before the first frame update
    void Start()
    {
        bullets = new GameObject[bulletCount];
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bulletClone = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            bulletClone.transform.parent = bulletsParent.transform;
            bulletClone.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.up * bulletSpeed);
            bulletClone.SetActive(false);
            bullets[i] = bulletClone;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //control the turret with the inputs with acceleration
        if (turretMode)
        {
            float dt = Time.deltaTime;
            if (Input.GetAxis("Horizontal") > 0 )
            {
                currentHSpeed = Mathf.MoveTowards(currentHSpeed, maxSpeedH, accelH * dt);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                currentHSpeed = Mathf.MoveTowards(currentHSpeed, -maxSpeedH, accelH * dt);
            }
            else
            {
                currentHSpeed = Mathf.MoveTowards(currentHSpeed, 0, forceBreak * accelH * dt);
            }

            //Debug.Log(Turret2.transform.rotation.eulerAngles);

            if (Input.GetAxis("Vertical") > 0 && (Turret2.transform.eulerAngles.x > FovUpAndgle || Turret2.transform.eulerAngles.x < 90))
            {
                currentVSpeed = Mathf.MoveTowards(currentVSpeed, -maxSpeedV, accelV * dt);
            }
            else if (Input.GetAxis("Vertical") < 0 && (Turret2.transform.eulerAngles.x < FovDownAngle || Turret2.transform.eulerAngles.x > 270))
            {
                currentVSpeed = Mathf.MoveTowards(currentVSpeed, maxSpeedV, accelV * dt);
            }
            else
            {
                currentVSpeed = Mathf.MoveTowards(currentVSpeed, 0, forceBreak * accelV * dt);
            }

            Turret.transform.Rotate(0, currentHSpeed, 0, Space.Self);
            Turret2.transform.Rotate(currentVSpeed, 0, 0, Space.Self);


            //shoot the bullet
            if (Input.GetButtonDown("Fire1"))
            {
                for (int i = 0; i < bulletCount; i++)
                {
                    if (!bullets[i].activeInHierarchy)
                    {
                        GameObject sound = SoundManager.instance.CreateSound("Shoot3");
                        sound.transform.position = transform.position;
                        bullets[i].SetActive(true);
                        bullets[i].transform.position = bulletSpawn.transform.position;
                        bullets[i].GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.up * bulletSpeed, ForceMode.Acceleration);
                        break;
                    }
                }
            }

            TurretBarrel.transform.Rotate(Vector3.forward* 1000 * dt);
        }
        
    }
}