using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeyserController : MonoBehaviour
{
    //Air zone (is a collision on trigger)
    [SerializeField] private float fanForce = 2f;
    [SerializeField] private float density = 1.225f;
    [SerializeField] private float dragCoefficient = 1.05f;
    [SerializeField] private float dragForce = 0f;
    [SerializeField] private float velocity = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Calculate the velocity
            velocity = Mathf.Sqrt((2 * fanForce) / (density * dragCoefficient));
            //Calculate the drag force
            dragForce = 0.5f * density * Mathf.Pow(velocity, 2) * dragCoefficient;
            //Apply the force to the player
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * dragForce);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            velocity = 0f;
            dragForce = 0f;
        }
    }
}