using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicInverseController : MonoBehaviour
{
    public List<GameObject> joints;
    public float maxDistance = 1f;
    public float maxIterations = 100;
    public float distanceBetweenJoints = 1f;
    public Vector3 fixedPointOnStartEffector = new Vector3(0, 0, 0);
    public Transform target;

    void LateUpdate()
    {
        for (int i = 0; i < maxIterations; i++)
        {
            for (int j = joints.Count - 1; j >= 0; j--)
            {
                if (j == joints.Count - 1)
                {
                    joints[j].transform.LookAt(target);
                }
                else
                {
                    joints[j].transform.LookAt(joints[j + 1].transform);
                }
                joints[j].transform.rotation *= Quaternion.Euler(fixedPointOnStartEffector);
                if (j != 0)
                {
                    joints[j - 1].transform.position = joints[j].transform.position + (joints[j - 1].transform.position - joints[j].transform.position).normalized * distanceBetweenJoints;
                }
                if (Vector3.Distance(joints[j].transform.position, fixedPointOnStartEffector) > maxDistance)
                {
                    joints[j].transform.position = fixedPointOnStartEffector + (joints[j].transform.position - fixedPointOnStartEffector).normalized * maxDistance;
                }
            }
        }
    }
}