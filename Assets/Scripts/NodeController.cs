using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{

    public GameObject[] bodyTypes;
    public GameObject PreviousNode;
    public GameObject NextNode;
    public GameObject head;
    public bool imLast = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnNextNode(int type)
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
}
