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
    public SpringJoint muscleLeftUp;
    public SpringJoint muscleRightUp;
    public SpringJoint muscleLeftDown;
    public SpringJoint muscleRightDown;

    public ConfigurableJoint joint;

    public PhysicMaterial material;

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
            GetComponent<SphereCollider>().material = material;

            //muscleRightUp
            muscleRightUp = gameObject.AddComponent<SpringJoint>();
            muscleRightUp.connectedBody = NextNode.GetComponent<Rigidbody>();
            muscleRightUp.autoConfigureConnectedAnchor = false;
            muscleRightUp.anchor = new Vector3(-0.5f, 0.25f, -0.25f);
            muscleRightUp.connectedAnchor = new Vector3(0.5f, 0.25f, -0.25f);
            muscleRightUp.spring = 1000;
            muscleRightUp.damper = 10;
            muscleRightUp.minDistance = 0;
            muscleRightUp.maxDistance = 0;
            muscleRightUp.tolerance = 0;
            muscleRightUp.enableCollision = true;
            muscleRightUp.enablePreprocessing = false;

            //muscleRightDown
            muscleRightDown = gameObject.AddComponent<SpringJoint>();
            muscleRightDown.connectedBody = NextNode.GetComponent<Rigidbody>();
            muscleRightDown.autoConfigureConnectedAnchor = false;
            muscleRightDown.anchor = new Vector3(-0.5f, -0.25f, -0.25f);
            muscleRightDown.connectedAnchor = new Vector3(0.5f, -0.25f, -0.25f);
            muscleRightDown.spring = 1000;
            muscleRightDown.damper = 10;
            muscleRightDown.minDistance = 0;
            muscleRightDown.maxDistance = 0;
            muscleRightDown.tolerance = 0;
            muscleRightDown.enableCollision = true;
            muscleRightDown.enablePreprocessing = false;

            //muscleLeftUp
            muscleLeftUp = gameObject.AddComponent<SpringJoint>();
            muscleLeftUp.connectedBody = NextNode.GetComponent<Rigidbody>();
            muscleLeftUp.autoConfigureConnectedAnchor = false;
            muscleLeftUp.anchor = new Vector3(-0.5f, 0.25f, 0.25f);
            muscleLeftUp.connectedAnchor = new Vector3(0.5f, 0.25f, 0.25f);
            muscleLeftUp.spring = 1000;
            muscleLeftUp.damper = 10;
            muscleLeftUp.minDistance = 0;
            muscleLeftUp.maxDistance = 0;
            muscleLeftUp.tolerance = 0;
            muscleLeftUp.enableCollision = true;
            muscleLeftUp.enablePreprocessing = false;

            //muscleLeftDown
            muscleLeftDown = gameObject.AddComponent<SpringJoint>();
            muscleLeftDown.connectedBody = NextNode.GetComponent<Rigidbody>();
            muscleLeftDown.autoConfigureConnectedAnchor = false;
            muscleLeftDown.anchor = new Vector3(-0.5f, -0.25f, 0.25f);
            muscleLeftDown.connectedAnchor = new Vector3(0.5f, -0.25f, 0.25f);
            muscleLeftDown.spring = 1000;
            muscleLeftDown.damper = 10;
            muscleLeftDown.minDistance = 0;
            muscleLeftDown.maxDistance = 0;
            muscleLeftDown.tolerance = 0;
            muscleLeftDown.enableCollision = true;
            muscleLeftDown.enablePreprocessing = false;


            joint = gameObject.AddComponent<ConfigurableJoint>();
            joint.connectedBody = NextNode.GetComponent<Rigidbody>();
            joint.autoConfigureConnectedAnchor = false;
            joint.anchor = new Vector3(-0.4f, 0, 0);
            joint.connectedAnchor = new Vector3(0.4f, 0, 0);
            joint.xMotion = ConfigurableJointMotion.Limited;
            joint.yMotion = ConfigurableJointMotion.Locked;
            joint.zMotion = ConfigurableJointMotion.Locked;
            joint.linearLimit = new SoftJointLimit { limit = 0.5f, bounciness = 0.5f, contactDistance = 0.5f };
            joint.linearLimitSpring = new SoftJointLimitSpring { damper = 0.5f, spring =10f };
            joint.enableCollision = true;



        }
        else
        {
            node = NextNode.GetComponent<BodyNode>().SpawnNextNode(type);
            GetComponent<SphereCollider>().material = null;
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

    public void restoreMassToNodes(float NewMass)
    {
        if (NextNode != null)
        {
            GetComponent<Rigidbody>().mass = NewMass;
            NextNode.GetComponent<BodyNode>().restoreMassToNodes(NewMass);
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