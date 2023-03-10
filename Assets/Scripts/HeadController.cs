using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if space pressed spawn next node
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<BodyNode>().SpawnNextNode(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetComponent<BodyNode>().SpawnNextNode(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetComponent<BodyNode>().SpawnNextNode(2);
        }
    }
}
