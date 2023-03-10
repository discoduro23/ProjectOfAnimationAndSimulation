using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 atachment;
    public GameObject target, positioner;
    public float speed;
    Vector3 vel = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetpos = positioner.transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, targetpos, ref vel, speed);
        transform.LookAt(target.transform.position + Vector3.up * -4f);
    }
}
