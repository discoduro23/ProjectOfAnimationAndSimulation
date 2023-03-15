using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    public float rotationSpeed = 1;
    public float speed = 1f;
    public float force = 1f;
    private Rigidbody rb;
    private BodyNode bodyNode;
    public GameObject tail;

    public float time = 0;
    private float intervalF = 0.2f;
    private float intervalS = 1;
    public BodyNode node;
    public int i = 0;
    public int nodeCount = 0;
    public bool growing = true;

    public float maxDistanceBetweenParts = 0;
    public float distanceBetweenParts = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bodyNode = GetComponent<BodyNode>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //if space pressed spawn next node
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            tail = bodyNode.SpawnNextNode(0);
            node = tail.GetComponent<BodyNode>();
            tail.GetComponent<Rigidbody>().mass = 1000;
            nodeCount++;
        }

        //Movement
        if (Input.GetKey(KeyCode.W))
        {
            time += Time.deltaTime;
            //if (i == 0)
            //{
            //    node = node.PreviousNode.GetComponent<BodyNode>();
            //}

            if (time < intervalF)
            {
                tail.GetComponent<Rigidbody>().mass = 1000;
                rb.mass = 1;

            }
            else if (node.PreviousNode != null && growing)
            {
                if (time > intervalF * (i + 1))
                {
                    
                    node.PreviousNode.GetComponent<BodyNode>().muscleLeftUp.minDistance = distanceBetweenParts;
                    node.PreviousNode.GetComponent<BodyNode>().muscleLeftUp.maxDistance = distanceBetweenParts;
                    node.PreviousNode.GetComponent<BodyNode>().muscleRightUp.minDistance = distanceBetweenParts;
                    node.PreviousNode.GetComponent<BodyNode>().muscleRightUp.maxDistance = distanceBetweenParts;
                    node.PreviousNode.GetComponent<BodyNode>().muscleLeftDown.minDistance = distanceBetweenParts;
                    node.PreviousNode.GetComponent<BodyNode>().muscleLeftDown.maxDistance = distanceBetweenParts;
                    node.PreviousNode.GetComponent<BodyNode>().muscleRightDown.minDistance = distanceBetweenParts;
                    node.PreviousNode.GetComponent<BodyNode>().muscleRightDown.maxDistance = distanceBetweenParts;
                    node = node.PreviousNode.GetComponent<BodyNode>();
                    i++;

                }
            }
            else if (time < (intervalF * i) + intervalS)
            {
                tail.GetComponent<Rigidbody>().mass = 1;
                rb.mass = 1000;
                growing = false;
            }
            else if (node.NextNode != null && !growing)
            {
                if (time > intervalF * (i + 1))
                {
                    
                    node.muscleLeftUp.minDistance = maxDistanceBetweenParts;
                    node.muscleLeftUp.maxDistance = maxDistanceBetweenParts;
                    node.muscleRightUp.minDistance = maxDistanceBetweenParts;
                    node.muscleRightUp.maxDistance = maxDistanceBetweenParts;
                    node.muscleLeftDown.minDistance = maxDistanceBetweenParts;
                    node.muscleLeftDown.maxDistance = maxDistanceBetweenParts;
                    node.muscleRightDown.minDistance = maxDistanceBetweenParts;
                    node.muscleRightDown.maxDistance = maxDistanceBetweenParts;
                    node = node.NextNode.GetComponent<BodyNode>();
                    i++;

                }
            }
            else
            {
                time = 0;
                i = 0;
                node = tail.GetComponent<BodyNode>();
                growing = true;
            }
        }
        




    }
}
