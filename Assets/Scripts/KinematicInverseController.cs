using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicInverseController : MonoBehaviour
{
    
    public List<Transform> joints;
    public float maxDistance = 1f;
    public float maxIterations = 100;
    public float distanceBetweenJoints = 1f;
    public float speed = 1f;
    public float threshold = 0.01f;
    public float maxSpeed = 1f;

    public Transform target;
    public Transform positioner;
    Vector3 vel = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        joints = new List<Transform>();
        joints.Add(transform);
        while (joints[joints.Count - 1].childCount > 0)
        {
            joints.Add(joints[joints.Count - 1].GetChild(0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetpos = positioner.transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, targetpos, ref vel, speed);
        //transform.LookAt(target.transform.position + Vector3.up * -4f);
        Vector3 targetPosition = target.position;
        Vector3 endPosition = joints[joints.Count - 1].position;

        float distance = Vector3.Distance(targetPosition, endPosition);
        if (distance > maxDistance)
        {
            targetPosition = endPosition + (targetPosition - endPosition).normalized * maxDistance;
        }

        for (int i = 0; i < maxIterations; i++)
        {
            endPosition = joints[joints.Count - 1].position;
            distance = Vector3.Distance(targetPosition, endPosition);

            if (distance < threshold)
            {
                break;
            }

            joints[joints.Count - 1].position = targetPosition;

            for (int j = joints.Count - 2; j >= 0; j--)
            {
                joints[j].position = joints[j + 1].position + (joints[j].position - joints[j + 1].position).normalized * distanceBetweenJoints;
            }

            for (int j = 1; j < joints.Count; j++)
            {
                joints[j].position = joints[j - 1].position + (joints[j].position - joints[j - 1].position).normalized * distanceBetweenJoints;
            }
        }
    }
}
