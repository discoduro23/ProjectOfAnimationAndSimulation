using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBodySpawn : MonoBehaviour
{
    public int bodyparts = 0;
    public GameObject bodyPrefab;
    public GameObject tailPrefab;
    public GameObject head;
    public GameObject[] body;
    
    // Start is called before the first frame update
    void Start()
    {
        body = new GameObject[bodyparts];
        for (int i = 0; i < bodyparts; i++)
        {
            if (i > 0)
            {
                if (i == bodyparts - 1)
                {
                    body[i] = Instantiate(tailPrefab, body[i - 1].transform.position, body[i - 1].transform.rotation);
                    body[i].transform.parent = transform;
                    body[i].transform.position = body[i - 1].transform.position + new Vector3(-1, 0, 0);
                    body[i - 1].GetComponent<CharacterJoint>().connectedBody = body[i].GetComponent<Rigidbody>();
                }
                else
                {
                    body[i] = Instantiate(bodyPrefab, body[i - 1].transform.position, body[i - 1].transform.rotation);
                    body[i].transform.parent = transform;
                    body[i].transform.position = body[i - 1].transform.position + new Vector3(-1, 0, 0);
                    body[i - 1].GetComponent<CharacterJoint>().connectedBody = body[i].GetComponent<Rigidbody>();
                }
                
                
            }
            else
            {
                body[i] = Instantiate(bodyPrefab, head.transform.position, head.transform.rotation);
                body[i].transform.parent = transform;
                body[i].transform.position = head.transform.position + new Vector3(-1, 0, 0);
                head.GetComponent<CharacterJoint>().connectedBody = body[i].GetComponent<Rigidbody>();
            }
            

            if (i == bodyparts - 1)
            {
                //errase component characterjoint from last bodypart
                Destroy(body[i].GetComponent<CharacterJoint>());

            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
