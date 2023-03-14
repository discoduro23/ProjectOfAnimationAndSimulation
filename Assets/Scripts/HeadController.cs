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

    private float time = 0;
    private float intervalF = 0.2f;
    private float intervalS = 1f;
    private BodyNode node;
    private int i = 0;


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
        }

        //Movement
        if (Input.GetKey(KeyCode.W))
        {
            if(i==0){
                node = node.PreviousNode.GetComponent<BodyNode>();
            }
            time += Time.deltaTime;
            if (time < intervalF)
            {
                tail.GetComponent<Rigidbody>().mass = 1000;
                rb.mass = 1;
                
            }
            if (node.PreviousNode != null)
            {
                if (time > intervalF * i)
                {
                    node.muscleCenter.minDistance = 0.2f;
                    node.muscleCenter.maxDistance = 0.2f;
                    node = node.PreviousNode.GetComponent<BodyNode>();
                    i++;
                }
                
            }
            else if (time > intervalF * i + intervalS)
            {
                tail.GetComponent<Rigidbody>().mass = 1;
                rb.mass = 1000;
            }
            else if (node.NextNode != null)
            {
                if (time > intervalF * i)
                {
                    node.muscleCenter.minDistance = 0;
                    node.muscleCenter.maxDistance = 0;
                    node = node.NextNode.GetComponent<BodyNode>();
                    i++;
                }

            }
            else
            {
                time = 0;
                i = 0;
            }
        }



    }
}
