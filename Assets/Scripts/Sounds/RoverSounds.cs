using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverSounds : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
            audioSource.Play();
        }
    }
}
