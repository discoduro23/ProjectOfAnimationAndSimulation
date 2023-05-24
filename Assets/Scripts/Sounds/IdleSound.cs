using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSound : MonoBehaviour
{
    // Start is called before the first frame update

    public float pitchVariation;
    private float PitchTarget;
    private float pitch;
    private float actualPitch;


    private float VolumeTarget;
    public float volumeVariation;
    private float volume;
    private float actualVolume;


    private float time;
    public float tempo;
    public bool randomTempo;
    public float randomVarTemp;

    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        pitch = audio.pitch;
        volume = audio.volume;
    }

    // Update is called once per frame
    void Update()
    {




        var dt = Time.deltaTime;
        time = dt;
        if (time > tempo)
        {
            time = 0;
            PitchTarget = Random.Range(pitch-pitchVariation, pitch+pitchVariation);
            VolumeTarget = Random.Range(volume-volumeVariation, volume+volumeVariation);

            if (randomTempo)
            {
                tempo = Random.Range(randomVarTemp/10, randomVarTemp);
            }

        }
        if (actualPitch < PitchTarget)
        {
            actualPitch += pitchVariation / 10 * dt;
        }
        else if (actualPitch > PitchTarget)
        {
            actualPitch -= pitchVariation / 10 * dt;
        }

        if (actualVolume < VolumeTarget)
        {
            actualVolume += volumeVariation / 10 * dt;
        }
        else if (actualVolume > VolumeTarget)
        {
            actualVolume -= volumeVariation / 10 * dt;
        }

        //audio.volume = actualVolume;
        audio.pitch = actualPitch;
    }
}
