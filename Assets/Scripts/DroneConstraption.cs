using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class DroneConstraption : MonoBehaviour
{
    public GameObject[] rotors;
    public Transform BoidControl;
    private Transform myTransform;

    public Transform target;

    public float FOVAngle;
    public float FOVWeapon;
    public float _AttackDistance;

    public float attackTime;
    public float attackInterval;
    public bool canAttack;
    public bool isAiming;
    public GameObject weapon;
    public float weaponSpeed;
    public float trackDistance;
    public float FovUp;
    public float FovDown;
    public GameObject muzzle;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = BoidControl.position;

        foreach (var item in rotors)
        {
            item.transform.Rotate(Vector3.up * 50);
        }
        //Debug.Log("angleToUp" + Vector3.Angle(transform.up, transform.position - target.transform.position) + "AngleTODOWN" + Vector3.Angle(-transform.up, transform.position - target.transform.position));
        if(Vector3.Angle(transform.up,  transform.position - target.transform.position ) > FovUp && Vector3.Angle(-transform.up, transform.position - target.transform.position) > FovDown)
        {
            var targetRotation = Quaternion.LookRotation(target.position - weapon.transform.position);
            // Smoothly rotate towards the target point.
            weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, targetRotation, weaponSpeed * Time.deltaTime);
        }

        //Debug.Log(Vector3.Angle(weapon.transform.forward, target.position - weapon.transform.position));
        if(Vector3.Angle(weapon.transform.forward, target.position - weapon.transform.position) <= FOVWeapon)
        {
            //Debug.Log("inPosition");
            isAiming = true;
        }
        else
        {
            isAiming=false;
        }

        if (attackTime > attackInterval)
        {
            canAttack = true;
        }
        attackTime += Time.deltaTime;

        if (IsInFOV(target.position) && IsInDistance(target.position) && canAttack && isAiming)
        {
            //Debug.Log("attacked");
            GameObject sound = SoundManager.instance.CreateSound("Shoot3");
            sound.transform.position = muzzle.transform.position;
            GameObject bullet = BoidControl.GetComponent<FlockUnit>().assignedFlock.Shoot();
            bullet.transform.position = muzzle.transform.position;
            bullet.transform.rotation = muzzle.transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * 100;
            attackTime = 0;
            canAttack = false;

        }

    }


    private bool IsInFOV(Vector3 position)
    {
        return Vector3.Angle(myTransform.forward, position - myTransform.position) <= FOVAngle;
    }

    private bool IsInDistance(Vector3 position)
    {
        return (myTransform.position - position).magnitude < _AttackDistance;
    }
}
