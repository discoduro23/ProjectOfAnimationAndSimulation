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
    private float intervalF = 0.1f;
    private float intervalS = 0.5f;
    public BodyNode node;
    public int i = 0;
    public int nodeCount = 0;
    public bool growing = true;
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
                    
                    node.PreviousNode.GetComponent<BodyNode>().muscleLeft.minDistance = 0.3f;
                    node.PreviousNode.GetComponent<BodyNode>().muscleLeft.maxDistance = 0.3f;
                    node.PreviousNode.GetComponent<BodyNode>().muscleRight.minDistance = 0.3f;
                    node.PreviousNode.GetComponent<BodyNode>().muscleRight.maxDistance = 0.3f;
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
                    node.muscleLeft.minDistance = 0;
                    node.muscleLeft.maxDistance = 0;
                    node.muscleRight.minDistance = 0;
                    node.muscleRight.maxDistance = 0;
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
