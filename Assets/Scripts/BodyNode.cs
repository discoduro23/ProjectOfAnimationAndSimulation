using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyNode : MonoBehaviour
{
    public GameObject[] bodyTypes;
    public GameObject PreviousNode;
    public GameObject NextNode;
    public GameObject head;
    public bool imLast = true;
    public SpringJoint muscleLeft;
    public SpringJoint muscleRight;
    public SpringJoint muscleUp;
    public SpringJoint muscleCenter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public GameObject SpawnNextNode( int type)
    {
        GameObject node;
        if (NextNode == null)
        {
            NextNode = Instantiate(bodyTypes[type], transform.position, transform.rotation);
            NextNode.transform.parent = transform.parent;
            NextNode.transform.position = transform.position + transform.right * -1;
            NextNode.GetComponent<BodyNode>().PreviousNode = gameObject;
            imLast = false;
            GetComponent<Rigidbody>().mass = 1;
            SearchHead(gameObject);
            node = NextNode;

            //muscleUp
            muscleUp = gameObject.AddComponent<SpringJoint>();
            muscleUp.connectedBody = NextNode.GetComponent<Rigidbody>();
            muscleUp.autoConfigureConnectedAnchor = false;
            muscleUp.anchor = new Vector3(-0.5f, 0.25f, 0);
            muscleUp.connectedAnchor = new Vector3(0.5f, 0.25f, 0);
            muscleUp.spring = 0;
            muscleUp.damper = 10;
            muscleUp.minDistance = 0;
            muscleUp.maxDistance = 0;
            muscleUp.tolerance = 0;
            muscleUp.enableCollision = false;
            muscleUp.enablePreprocessing = false;

            //muscleRight
            muscleRight = gameObject.AddComponent<SpringJoint>();
            muscleRight.connectedBody = NextNode.GetComponent<Rigidbody>();
            muscleRight.autoConfigureConnectedAnchor = false;
            muscleRight.anchor = new Vector3(-0.5f, 0, -0.25f);
            muscleRight.connectedAnchor = new Vector3(0.5f, 0, -0.25f);
            muscleRight.spring = 1000;
            muscleRight.damper = 10;
            muscleRight.minDistance = 0;
            muscleRight.maxDistance = 0;
            muscleRight.tolerance = 0;
            muscleRight.enableCollision = false;
            muscleRight.enablePreprocessing = false;

            //muscleLeft
            muscleLeft = gameObject.AddComponent<SpringJoint>();
            muscleLeft.connectedBody = NextNode.GetComponent<Rigidbody>();
            muscleLeft.autoConfigureConnectedAnchor = false;
            muscleLeft.anchor = new Vector3(-0.5f, 0, 0.25f);
            muscleLeft.connectedAnchor = new Vector3(0.5f, 0, 0.25f);
            muscleLeft.spring = 1000;
            muscleLeft.damper = 10;
            muscleLeft.minDistance = 0;
            muscleLeft.maxDistance = 0;
            muscleLeft.tolerance = 0;
            muscleLeft.enableCollision = false;
            muscleLeft.enablePreprocessing = false;

            //muscleCenter
            muscleCenter = gameObject.AddComponent<SpringJoint>();
            muscleCenter.connectedBody = NextNode.GetComponent<Rigidbody>();
            muscleCenter.autoConfigureConnectedAnchor = false;
            muscleCenter.anchor = new Vector3(-0.5f, 0, 0);
            muscleCenter.connectedAnchor = new Vector3(0.5f, 0, 0);
            muscleCenter.spring = 1000;
            muscleCenter.damper = 10;
            muscleCenter.minDistance = 0;
            muscleCenter.maxDistance = 0;
            muscleCenter.tolerance = 0;
            muscleCenter.enableCollision = false;
            muscleCenter.enablePreprocessing = false;



            
        }
        else
        {
            node = NextNode.GetComponent<BodyNode>().SpawnNextNode(type);
        }
        return node;
    }

    public void SearchHead(GameObject Ref)
    {
        if (PreviousNode == null)
        {
            Ref.GetComponent<BodyNode>().head = gameObject;
        }
        else
        {
            PreviousNode.GetComponent<BodyNode>().SearchHead(Ref);
        }
    }
}










////Spring Joint settings
//joint = gameObject.AddComponent<SpringJoint>();
//joint.connectedBody = NextNode.GetComponent<Rigidbody>();
//joint.autoConfigureConnectedAnchor = false;
//joint.anchor = new Vector3(-0.5f, 0, 0);
//joint.connectedAnchor = new Vector3(0.5f, 0, 0);
//joint.spring = 10000000;
//joint.damper = 10;
//joint.minDistance = 0;
//joint.maxDistance = 0.1f;
//joint.tolerance = 0.005f;
//joint.enableCollision = true;
//joint.enablePreprocessing = true;



//////Hingle Joint settings
//joint = gameObject.AddComponent<HingeJoint>();
//joint.connectedBody = NextNode.GetComponent<Rigidbody>();
//joint.anchor = new Vector3(-0.5f, 0, 0);
//joint.axis = new Vector3(0, 1, 0);
//joint.autoConfigureConnectedAnchor = false;
//joint.connectedAnchor = new Vector3(0.5f, 0, 0);
//joint.useSpring = false;
//JointSpring spring = new JointSpring();
//spring.spring = 10000000;
//spring.damper = 0;
//spring.targetPosition = 0;
//joint.spring = spring;
//joint.useMotor = true;
//JointMotor motor = new JointMotor();
//motor.force = 1000;
//motor.targetVelocity = 0;
//motor.freeSpin = false;
//joint.motor = motor;
//joint.useLimits = true;
//JointLimits limits = new JointLimits();
//limits.min = -90;
//limits.max = 90;
//limits.bounciness = 0;
//limits.bounceMinVelocity = 0;
//limits.contactDistance = 0;
//joint.limits = limits;
//joint.breakForce = Mathf.Infinity;
//joint.breakTorque = Mathf.Infinity;
//joint.enableCollision = true;
//joint.enablePreprocessing = true;



//character Joint settings
//joint = gameObject.AddComponent<CharacterJoint>();
//joint.connectedBody = NextNode.GetComponent<Rigidbody>();
//joint.anchor = new Vector3(-0.5f, 0, 0);
//joint.connectedAnchor = new Vector3(0.51f, 0, 0);
//joint.lowTwistLimit = new SoftJointLimit() { limit = -38.1f };
//joint.highTwistLimit = new SoftJointLimit() { limit = 10.0f };
//joint.swingLimitSpring = new SoftJointLimitSpring() { damper = 0.0f, spring = 0.0f };
//joint.swing1Limit = new SoftJointLimit() { limit = 18.1f };
//joint.swing2Limit = new SoftJointLimit() { limit = 32.5f };
//joint.enableProjection = false;
//joint.projectionDistance = 0.1f;
//joint.projectionAngle = 180.0f;
//joint.enableCollision = false;
//joint.enablePreprocessing = true;
//joint.breakForce = Mathf.Infinity;
//joint.breakTorque = Mathf.Infinity;
//joint.massScale = 1.0f;
//joint.connectedMassScale = 1.0f;