using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

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
        //transform.rotation = Quaternion.EulerRotation(BoidControl.transform.rotation.eulerAngles);
        //Quaternion q;
        //Vector3 a = crossproduct(v1, v2);
        //q.xyz = a;
        //q.w = sqrt((v1.Length ^ 2) * (v2.Length ^ 2)) + dotproduct(v1, v2);

        //Vector3 vector3 = Vector3.Cross(transform.rotation.eulerAngles, Vector3.up);
        //Quaternion q = Quaternion.identity;
        //q.eulerAngles = vector3;
        //
        //q.w = Mathf.Sqrt((transform.rotation.eulerAngles.magnitude * transform.rotation.eulerAngles.magnitude) * (Vector3.up.magnitude * Vector3.up.magnitude)) + Vector3DotProduct(transform.rotation.eulerAngles, Vector3.up); 
        //
        //Quaternion fromtoUP = Quaternion.(transform.rotation.eulerAngles, Vector3.up);
        //
        //transform.rotation = fromtoUP * transform.rotation;
        //
        //Quaternion.FromToRotation(transform.rotation.eulerAngles, Vector3.Project( BoidControl.transform.rotation.eulerAngles, Vector3.up));

        ///Primer paso Up actual y el del mundo y hacer FromToRotation, Multiplicar a izquierda con la rotacion 
        ///alinear con la proyeccion con normal el Up y hacer FromToRotation.

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
