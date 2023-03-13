using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float speed = 1;
    public float turnSpeed = 100;
    public int gap = 100;
    public GameObject bodyPart;
    public List<GameObject> bodyParts = new List<GameObject>();
    private List<Vector3> positions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        

        float h = Input.GetAxis("Horizontal");
        transform.Rotate(transform.up * h * turnSpeed * Time.deltaTime);

        positions.Insert(0, transform.position);

        int index = 0;
        foreach (GameObject bodyPart in bodyParts)
        {
            Vector3 targetPosition = positions[Mathf.Min(index * gap, positions.Count - 1)];
            Vector3 direction = targetPosition - bodyPart.transform.position;
            bodyPart.transform.position += direction * speed * Time.deltaTime;
            bodyPart.transform.LookAt(targetPosition);
            index++;
        }

        if (positions.Count > bodyParts.Count * gap)
        {
            positions.RemoveAt(positions.Count - 1);
        }

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
