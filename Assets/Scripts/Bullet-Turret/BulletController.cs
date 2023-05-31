using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameManagerController gameManagerController;

    void Start()
    {
        gameManagerController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerController>();
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
            //Debug.Log("morite puto");
            gameManagerController.life -= 5;
        }
        else if(collision.gameObject.GetComponent<DroneConstraption>() != null)
        {
            collision.gameObject.GetComponent<DroneConstraption>().BoidControl.gameObject.SetActive(false);
            Destroy (collision.gameObject);
            GameObject sound = SoundManager.instance.CreateSound("DroneExplosion");
            sound.transform.position = transform.position;
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
