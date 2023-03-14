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
    private float intervalS = 0.5f;

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
        }

        //Movement
        if (Input.GetKey(KeyCode.W))
        {
            time += Time.deltaTime;
            int i = 0;
            BodyNode node = bodyNode;
            if (time < intervalF)
            {
                tail.GetComponent<Rigidbody>().mass = 1000;
                rb.mass = 1;
                
            }
            while (node.NextNode != null)
            {
                if (time > intervalF * i)
                {
                    node.muscleCenter.spring = 100;/// Estas Aqui <------------------------------------------------------------------
                }
                node = node.NextNode.GetComponent<BodyNode>();
                i++;
            }
            if (time < intervalF * i + intervalS)
            {
                tail.GetComponent<Rigidbody>().mass = 1;
                rb.mass = 1000;
            }
            else
            {
                time = 0;
            }
        }



    }
}
