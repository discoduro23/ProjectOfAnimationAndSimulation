using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DroneConstraption : MonoBehaviour
{
    public GameObject rotorL;
    public GameObject rotorR;
    public GameObject weapon;
    public Transform target;
    private EnemyAtack enemyAtack;
    public Transform BoidControl;

    public float FOVAngle;
    public float weaponSpeed;
    public float rotationSpeed;
    public float trackDistance;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponentInParent<FlockUnit>().assignedFlock.target;
        enemyAtack = GetComponentInParent<EnemyAtack>();
        trackDistance = enemyAtack.attackDistance * 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = BoidControl.position;
        //transform.rotation = Quaternion.EulerRotation(0, BoidControl.transform.rotation.eulerAngles.y, 0);
        //transform.Rotate(Vector3.ProjectOnPlane( BoidControl.rotation.eulerAngles, transform.up));
        //rotorL.transform.localEulerAngles += new Vector3(0, 1, 0) * rotationSpeed * Time.deltaTime;
        //rotorR.transform.localEulerAngles += new Vector3(0, -1, 0) * rotationSpeed * Time.deltaTime;
        
        //aim the weapon to the target
        if(Vector3.Angle(transform.up, target.position - transform.position) >= FOVAngle && Vector3.Angle(-transform.up, target.position - transform.position) >= FOVAngle / 2 && target!=null && (target.position - transform.position).magnitude < trackDistance)
        {
            var targetRotation = Quaternion.LookRotation(target.position - weapon.transform.position);
            enemyAtack.isAiming = true;
            // Smoothly rotate towards the target point.
            weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, targetRotation, weaponSpeed * Time.deltaTime);

        }
        else
        {
            enemyAtack.isAiming=false;
        }
        //weapon.transform.LookAt(target.transform.position);
    }
}
