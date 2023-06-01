using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameManagerController gameManagerController;
    public ParticleSystem explosion;
    private bool collided = false;
    public GameObject BlueExpPrefab;
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
        if (!explosion.isEmitting && collided)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.SetActive(false);
            CancelInvoke();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        collided = true;
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("morite puto");
            gameManagerController.life -= 5;
            GameObject sound = SoundManager.instance.CreateSound("Crash");
            sound.transform.position = transform.position;
            explosion.Play();
        }
        else if(collision.gameObject.GetComponent<DroneConstraption>() != null)
        {
            collision.gameObject.GetComponent<DroneConstraption>().BoidControl.gameObject.SetActive(false);
            GameObject exp = Instantiate(BlueExpPrefab);
            exp.transform.position = collision.transform.position;
            Destroy(collision.gameObject);
            GameObject sound = SoundManager.instance.CreateSound("DroneExplosion");
            sound.transform.position = transform.position;
            explosion.Play();
        }        
    }

    private void Disabled()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
        CancelInvoke();
    }
}
