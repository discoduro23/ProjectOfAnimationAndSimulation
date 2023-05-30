using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Invoke("Disabled", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("morite puto");
        }
        else if(collision.gameObject.GetComponent<DroneConstraption>() != null)
        {
            collision.gameObject.GetComponent<DroneConstraption>().BoidControl.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
        CancelInvoke();
    }

    private void Disabled()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
