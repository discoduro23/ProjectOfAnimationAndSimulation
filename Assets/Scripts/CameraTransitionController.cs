using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitionController : MonoBehaviour
{
    public TransitionSettings transition;
    public float StartDelay;

    public GameObject Camera1;
    public GameObject Camera2;

    TransitionManager transitionManager;

    // Start is called before the first frame update
    void Start()
    {
        transitionManager = TransitionManager.Instance();
        Camera1 = GameObject.Find("CarCamera");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.C))
        {
            transitionManager.onTransitionCutPointReached += ActivateTransition;
            transitionManager.Transition(transition, StartDelay);
        }
    }

    public void ActivateTransition()
    {
        if(Camera1.activeSelf)
        {
            Camera1.SetActive(false);
            Camera2.SetActive(true);

            Camera2.GetComponentInParent<TurretController>().turretMode = true;
            Camera2.GetComponentInParent<CarController>().carMode = false;
        }
        else
        {
            Camera1.SetActive(true);
            Camera2.SetActive(false);

            Camera2.GetComponentInParent<TurretController>().turretMode = false;
            Camera2.GetComponentInParent<CarController>().carMode = true;
        }

        transitionManager.onTransitionCutPointReached -= ActivateTransition;
    }
}
