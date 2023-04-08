using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCDCustomControl : MonoBehaviour
{
    // ATTRIBUTES
    public bool isPureCCD = true;  // Pure CCD switch
    public bool isSearchEnabled = false; // Switch controlling active search
    public GameObject target = null;  // Target
    public Transform[] parts;                   // Object collection

    // Use this for initialization
    void Start()
    {
        // Initialize the parts array
        parts = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            parts[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSearchEnabled)
        {
            foreach (Transform currentPart in parts)
            {
                // Calculate current direction
                Vector3 currentDirection = currentPart.parent != null ?
                    currentPart.parent.InverseTransformDirection(currentPart.forward) :
                    transform.InverseTransformDirection(currentPart.forward);

                // Calculate goal direction and orientation
                Vector3 goalDirection = target.transform.position - currentPart.position;
                Quaternion goalOrientation = Quaternion.FromToRotation(currentDirection, goalDirection) * currentPart.rotation;

                // Apply angle limits
                if (currentPart.parent == null ||
                    currentPart.GetComponent<AngleLimit>() == null ||
                    Vector3.Angle(currentPart.parent.InverseTransformDirection(currentPart.forward), goalOrientation * Vector3.forward) < currentPart.GetComponent<AngleLimit>().value)
                {
                    // Interpolate rotation towards goal orientation
                    Quaternion newOrientation = Quaternion.Slerp(currentPart.rotation, goalOrientation, 1.0f * Time.deltaTime);
                    currentPart.rotation = newOrientation;
                }
            }
        }
    }
}
