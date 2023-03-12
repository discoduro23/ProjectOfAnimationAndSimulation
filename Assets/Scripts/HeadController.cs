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
            bodyNode.SpawnNextNode(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bodyNode.SpawnNextNode(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            bodyNode.SpawnNextNode(2);
        }

        //constant velocity forward
        if (rb.velocity.magnitude < speed)
        {
            rb.velocity = transform.right * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }


    }
}
