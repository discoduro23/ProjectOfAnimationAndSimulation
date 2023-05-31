using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArmIKBehaviour : MonoBehaviour
{
    [SerializeField] Transform[] joints; // The joints of the chain
    float[] lengthJoints; // length of each joint

    [SerializeField] int solverIterations = 10; // How many times to run the solver
    private int solverIterationsLocal = 1;

    [SerializeField] Vector3 defaultPosition = Vector3.zero; // The default position of the end effector
    [SerializeField] GameObject target = null; // The target to reach
    private Transform smoothTempTarget = null; // The target position temporal, to have smooth
    [SerializeField] float smoothTime = 0.3f;
    Vector3 velocity = Vector3.zero;

    [SerializeField] float sphericalRadiusPresence = 0.1f; // The radius of the sphere that the target is in

    [SerializeField] float nearDistance = 1;





    // Start is called before the first frame update
    void Start()
    {
        lengthJoints = new float[joints.Length - 1];

        // Keep in mind that the last joint has no length
        for (int i = 0; i < lengthJoints.Length; i++)
        {
            // Calculate the length of each joint
            lengthJoints[i] = Vector3.Distance(joints[i].position, joints[i + 1].position);
        }

        //Set the smooth target
        smoothTempTarget = joints[joints.Length - 1].transform;

        // Set the solver iterations
        solverIterationsLocal = solverIterations;


    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && Vector3.Distance(target.transform.position, joints[0].position) < sphericalRadiusPresence)
        {
            Vector3 targetPosition = target.transform.position;
            smoothTempTarget.position = Vector3.SmoothDamp(smoothTempTarget.position, targetPosition, ref velocity, smoothTime);

            solverIterationsLocal = solverIterations;
        }
        else
        {
            // Set the default position of the end effector
            Vector3 defaultDirection = this.transform.rotation * Vector3.forward;
            Vector3 tempDefPosition = joints[0].transform.position + defaultDirection * defaultPosition.magnitude;
            smoothTempTarget.position = Vector3.SmoothDamp(smoothTempTarget.position, tempDefPosition, ref velocity, smoothTime);
            smoothTempTarget.rotation = Quaternion.LookRotation(defaultDirection, Vector3.up);
            solverIterationsLocal = 1;
        }
        IKSolver();
    }

    void IKSolver()
    {
        Vector3[] finalJointsPos = new Vector3[joints.Length]; // The final positions of the joints to be applied

        // Save the actual position of the joints
        for (int i = 0; i < finalJointsPos.Length; i++)
        {
            finalJointsPos[i] = joints[i].position;
        }

        // Apply the solver iterations to get a more precise result
        for (int i = 0; i < solverIterationsLocal; i++)
        {
            /* FABRIK ALGORITHM ITSELF
            *
            *  1st: InversePos = Calculate the " x' " position from the end effector to the anchor, 
             *  taking into account the distances between the diferent joints
             *  
             *  2nd: ForwardPos = Calculate the definitive " x " position from the anchor to the end 
             *  effector, knowing that the first joint will be always in the same position (without that, 
             *  the chain itself will not be fixed in the anchor) and with the " x' " position calculated.
             */
            finalJointsPos = SolveForwardPos(SolveInversePos(finalJointsPos));
        }

        // Apply the final positions to the joints 
        for (int i = 0; i < finalJointsPos.Length; i++)
        {
            joints[i].position = finalJointsPos[i];

            //Apply rotation to the joints (optional)
            if (i < finalJointsPos.Length - 1)
            {
                joints[i].LookAt(finalJointsPos[i + 1]);
            }

        }
    }
    /// <summary>
    /// Calculate the " x' " position from the end effector to the anchor, taking into account the distances between the diferent joints
    /// </summary>
    /// <param name="forwardPos"></param>
    /// <returns> inversePos </returns>
    Vector3[] SolveInversePos(Vector3[] forwardPos)
    {
        // This should be the first part of the algorithm
        Vector3[] inversePos = new Vector3[forwardPos.Length];

        for (int i = (forwardPos.Length - 1); i >= 0; i--)
        {
            if (i == (forwardPos.Length - 1)) // If it's the last joint
            {
                inversePos[i] = smoothTempTarget.position; // The position is the same
            }
            else
            {
                Vector3 posCurrent = forwardPos[i]; // The current position of the joint
                Vector3 posNewNext = inversePos[i + 1]; // The next position from the inverse array
                Vector3 direction = (posCurrent - posNewNext).normalized; // The direction from the next position to the current position
                float lenght = lengthJoints[i]; // The length of the joint
                inversePos[i] = posNewNext + (direction * lenght); // The new position of the joint
            }
        }

        return inversePos; // Return the array of positions
    }

    /// <summary>
    /// Calculate the definitive " x " position from the anchor to the end effector, knowing that the first joint will be always in the same position (without that, the chain itself will not be fixed in the anchor) and with the " x' " position calculated
    /// </summary>
    /// <param name="inversePos"></param>
    /// <returns>forwardPos</returns>
    Vector3[] SolveForwardPos(Vector3[] inversePos)
    {
        // This should be the 2 part of the algorithm

        Vector3[] forwardPos = new Vector3[inversePos.Length];


        for (int i = 0; i < inversePos.Length; i++)
        {
            if (i == 0) // If it's the first joint
            {
                forwardPos[i] = joints[i].position; // The position is the same
            }
            else
            {
                Vector3 posNewCurrent = inversePos[i]; // The current position of the joint
                Vector3 posNewNewPrevious = forwardPos[i - 1]; // The previous position from the forward array
                Vector3 direction = (posNewCurrent - posNewNewPrevious).normalized; // The direction from the previous position to the current position
                float lenght = lengthJoints[i - 1]; // The length of the joint
                forwardPos[i] = posNewNewPrevious + (direction * lenght); // The new position of the joint
            }
        }

        return forwardPos; // Return the array of positions
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < joints.Length - 1; i++)
        {
            Gizmos.DrawLine(joints[i].position, joints[i + 1].position);
        }

        Gizmos.DrawWireSphere(joints[0].position, sphericalRadiusPresence);

    }

    private void OnTriggerStay(Collider other)
    {

        joints[joints.Length - 1].LookAt(other.gameObject.transform);
        
        if (other.CompareTag("Mine"))
        {
            Debug.Log("Mine detected");
            target = other.gameObject;

            float distance = Vector3.Distance(target.transform.position, joints[joints.Length - 1].position);

            if (distance < nearDistance)
            {
                other.gameObject.gameObject.GetComponent<MaterialMineBehaveour>().Mine();
            }
            else
            {
                GameManagerController.instance.isPercentage = false;
            }

        }
        else if (other.CompareTag("Build"))
        {
            Debug.Log("Repare Detected");
            target = other.gameObject;

            float distance = Vector3.Distance(target.transform.position, joints[joints.Length - 1].position);

            if (distance < nearDistance)
            {
                other.gameObject.gameObject.GetComponent<RepareAntenaBehaveour>().Repare();
            }
            else
            {
                GameManagerController.instance.isPercentage = false;
            }


        }
    }

}
