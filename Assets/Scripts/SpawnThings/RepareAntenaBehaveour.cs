using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepareAntenaBehaveour : MonoBehaviour
{
    //Beam particles
    public ParticleSystem beamPrefab;
    private ParticleSystem beamInstance;
    public Transform beamPlace;

    //Antena
    public GameObject antena;

    public float timeToRepare = 30f;
    private float timeReparing = 0f;
    private float percentage = 0f;
    private AudioSource audioSource;
    private void Start()
    {
    }

    public void Repare()
    {
        if (GameManagerController.instance.RequestRepair())
        {
            if (timeReparing > timeToRepare)
            {
                
                gameObject.tag = "Untagged";
                beamInstance = Instantiate(beamPrefab, beamPlace.position, beamPlace.rotation);
                GameManagerController.Instance.RepairCompleted();
                audioSource = SoundManager.instance.CreateSound("Shoot2").GetComponent<AudioSource>();
                audioSource.gameObject.transform.position = transform.position;

            }
            else
            {

                if (audioSource == null)
                {
                    audioSource = SoundManager.instance.CreateSound("LegoBuild").GetComponent<AudioSource>();
                    audioSource.gameObject.transform.position = transform.position;

                }
                else
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource = SoundManager.instance.CreateSound("LegoBuild").GetComponent<AudioSource>();
                        audioSource.gameObject.transform.position = transform.position;

                    }
                }
                timeReparing += Time.deltaTime;

                antena.transform.Rotate(0, 0.1f * timeReparing, 0);

                GameManagerController.Instance.isPercentage = true;
                percentage = timeReparing / timeToRepare * 100;
                GameManagerController.Instance.percentage = percentage;

            }
            Debug.Log("Time" + timeReparing);
        }
        
    }


}
