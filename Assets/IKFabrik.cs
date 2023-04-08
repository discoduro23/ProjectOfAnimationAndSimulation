using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFabrik : MonoBehaviour
{
    [SerializeField]
    Transform[] joints;
    float[] lengthJoints;

    [SerializeField]
    int solverIterations = 10; // How many times to run the solver

    [SerializeField]
    Transform targetPos;


    // Start is called before the first frame update
    void Start()
    {
        lengthJoints = new float[joints.Length - 1];

        //Keep in mind that the last joint has no length
        for (int i = 0; i < lengthJoints.Length; i++)
        {
            lengthJoints[i] = Vector3.Distance(joints[i].position, joints[i + 1].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        IKSolver();
    }

    void IKSolver()
    {
        Vector3[] finalJointsPos = new Vector3[joints.Length]; // The final positions of the joints

        // Save the actual position of the joints
        for (int i = 0; i < finalJointsPos.Length; i++)
        {
            finalJointsPos[i] = joints[i].position;
        }

        // Apply the solver iterations to get a more precise result
        for (int i = 0; i < solverIterations; i++)
        {
            finalJointsPos = SolveForwardPos(SolveInversePos(finalJointsPos));
        }

        // Apply the final positions to the joints
        for (int i = 0; i < finalJointsPos.Length; i++)
        {
            joints[i].position = finalJointsPos[i];

            //Apply rotation to the joints
            if (i < finalJointsPos.Length - 1)
            {
                joints[i].LookAt(finalJointsPos[i + 1]);
            }

        }
    }

    Vector3[] SolveForwardPos(Vector3[] inversePos)
    {
        Vector3[] forwardPos = new Vector3[inversePos.Length];

        for (int i = 0; i < inversePos.Length; i++)
        {
            if (i == 0)
            {
                forwardPos[i] = joints[i].position;
            }
            else
            {
                Vector3 posCurrent = inversePos[i];
                Vector3 posPrevious = forwardPos[i - 1];
                Vector3 direction = (posCurrent - posPrevious).normalized;
                float lenght = lengthJoints[i - 1];
                forwardPos[i] = posPrevious + (direction * lenght);
            }
        }

        return forwardPos;
    }


    Vector3[] SolveInversePos(Vector3[] forwardPos)
    {
        Vector3[] inversePos = new Vector3[forwardPos.Length];

        for (int i = (forwardPos.Length - 1); i >= 0; i--)
        {
            if (i == (forwardPos.Length - 1))
            {
                inversePos[i] = targetPos.position;
            }
            else
            {
                Vector3 posNext = inversePos[i + 1];
                Vector3 posBaseCurrent = forwardPos[i];
                Vector3 direction = (posBaseCurrent - posNext).normalized;
                float lenght = lengthJoints[i];
                inversePos[i] = posNext + (direction * lenght);
            }
        }

        return inversePos;
    }


    void OnDrawGizmos()
    {
        // Set gizmo color
        Gizmos.color = Color.red;

        // Draw a line between each joint
        for (int i = 0; i < joints.Length - 1; i++)
        {
            Gizmos.DrawLine(joints[i].position, joints[i + 1].position);
        }
    }
}

/*
 
This code is an implementation of the Inverse Kinematics (IK) algorithm using the Forward And Backward Reaching Inverse Kinematics (FABRIK) algorithm for a chain of joints in Unity.

The code begins by importing the necessary libraries, including System.Collections, System.Collections.Generic, and UnityEngine.

The IKFabrik class is defined, which inherits from the MonoBehaviour class in Unity. This means that the class can be attached to a GameObject in the scene and its methods will be executed by the engine.

The class has several member variables, including Transform arrays to store the joint transforms and a float array to store the length of the joint chain. It also has integer variables to set the number of solver iterations and a Transform variable to store the target position.

In the Start() method, the lengthJoints array is initialized by calculating the distance between each joint in the joint array. This is done using a for loop that iterates over the array and uses the Vector3.Distance() method to calculate the distance between each joint.

In the Update() method, the IKSolver() method is called to perform the IK calculations and update the joint positions and rotations.

The IKSolver() method begins by initializing a new array of Vector3s to store the final positions of the joints. The current positions of the joints are then saved in this array using a for loop.

Next, a for loop runs the solverIterations number of times to apply the FABRIK algorithm to the joint chain. The SolveInversePos() and SolveForwardPos() methods are called in turn to calculate the positions of the joints.

Finally, the final positions of the joints are applied to the joint transforms using another for loop. Additionally, the LookAt() method is used to set the rotation of each joint to point towards the next joint in the chain.

The SolveForwardPos() method takes an array of inverse positions and returns an array of forward positions. It calculates the position of each joint in the chain based on the position of the previous joint and the distance between them.

The SolveInversePos() method takes an array of forward positions and returns an array of inverse positions. It calculates the position of each joint in the chain based on the position of the next joint and the distance between them.

Overall, this code provides an implementation of the FABRIK algorithm for a chain of joints in Unity, allowing for efficient calculation of IK solutions for complex joint chains.


//MORE

Sure, let me explain the SolveForwardPos() and SolveInversePos() methods and their utility.

SolveForwardPos() calculates the positions of the joints in forward direction, starting from the base joint to the end effector. It takes an array of joint positions in the inverse direction as input and returns an array of joint positions in the forward direction.

Here is how it works:

The position of the first joint is taken from the joints array.
For each subsequent joint, the direction vector is calculated by subtracting the previous joint position from the current joint position and normalizing it. The length of the bone is obtained from the lengthJoints array.
The position of the current joint is calculated by adding the direction vector multiplied by the bone length to the position of the previous joint.
SolveInversePos() calculates the positions of the joints in inverse direction, starting from the end effector to the base joint. It takes an array of joint positions in the forward direction as input and returns an array of joint positions in the inverse direction.

Here is how it works:

The position of the end effector is taken from the targetPos variable.
For each joint, the direction vector is calculated by subtracting the next joint position from the current joint position and normalizing it. The length of the bone is obtained from the lengthJoints array.
The position of the current joint is calculated by adding the direction vector multiplied by the bone length to the position of the next joint.
These two methods are used together to implement the Inverse Kinematics (IK) solver. The idea is to start with an initial guess for the joint positions and then iteratively improve the guess until the end effector reaches the target position. This is done by alternating between the forward and inverse calculations.

In each iteration, the SolveInversePos() method is called first to obtain the joint positions in the inverse direction, followed by the SolveForwardPos() method to obtain the joint positions in the forward direction. This process is repeated for the specified number of solver iterations, and the final joint positions are used to update the positions and rotations of the joints.

*/