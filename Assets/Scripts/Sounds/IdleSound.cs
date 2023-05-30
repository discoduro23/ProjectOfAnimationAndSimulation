using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSound : MonoBehaviour
{
    public AudioSource motor;

    public AnimationCurve VolumeD;
    public AnimationCurve PitchD;
    public AnimationCurve VolumeU;
    public AnimationCurve PitchU;

    public float m_Volume;
    public float m_Pitch;
    private bool peak = false;
    private bool peakending = false;

    public int randomMinTimer = 0;
    public int randomMaxTimer = 10;
    private void Start()
    {
        
        motor.volume = m_Volume;
        motor.pitch = m_Pitch ;
        Invoke("soundactivation", Random.Range(randomMinTimer, randomMaxTimer));
    }


    private void FixedUpdate()
    {
        float timer = Time.time;

        

        if((timer%4 <= 0.2 && peak) || peakending)
        {
            peak = false;
            peakending = true;
            motor.volume = m_Volume + VolumeU.Evaluate(timer % 4);
            motor.pitch = m_Volume + PitchU.Evaluate(timer % 4);
            if(timer%4 >= 3.8) peakending = false;
        }
        else
        {
            motor.volume = m_Volume + VolumeD.Evaluate(timer % 4);
            motor.pitch = m_Volume + PitchD.Evaluate(timer % 4);
        }
        
    }

    void soundactivation()
    {
        peak = true;
        Invoke("soundactivation", Random.Range(randomMinTimer, randomMaxTimer));
    }


















    //// Start is called before the first frame update

    //public float pitchVariation;
    //private float PitchTarget;
    //public float pitch;
    //private float actualPitch;


    //private float VolumeTarget;
    //public float volumeVariation;
    //public float volume;
    //private float actualVolume;


    //private float time;
    //public float tempo;
    //public bool randomTempo;
    //public float randomVarTemp;

    //private AudioSource audio;

    //void Start()
    //{
    //    audio = GetComponent<AudioSource>();
    //    pitch = audio.pitch;
    //    volume = audio.volume;
    //}

    //// Update is called once per frame
    //void Update()
    //{




    //    var dt = Time.deltaTime;
    //    time = dt;
    //    if (time > tempo)
    //    {
    //        time = 0;
    //        PitchTarget = Random.Range(pitch-pitchVariation, pitch+pitchVariation);
    //        VolumeTarget = Random.Range(volume-volumeVariation, volume+volumeVariation);

    //        if (randomTempo)
    //        {
    //            tempo = Random.Range(randomVarTemp/10, randomVarTemp);
    //        }

    //    }

    //    actualPitch = audio.pitch;
    //    actualVolume = audio.volume;

    //    if (actualPitch < PitchTarget)
    //    {
    //        actualPitch += (pitchVariation / 10) * dt;
    //    }
    //    else if (actualPitch > PitchTarget)
    //    {
    //        actualPitch -= (pitchVariation / 10) * dt;
    //    }

    //    if (actualVolume < VolumeTarget)
    //    {
    //        actualVolume += (volumeVariation / 10) * dt;
    //    }
    //    else if (actualVolume > VolumeTarget)
    //    {
    //        actualVolume -= (volumeVariation) / 10 * dt;
    //    }

    //    audio.volume = actualVolume;
    //    audio.pitch = actualPitch;

}
