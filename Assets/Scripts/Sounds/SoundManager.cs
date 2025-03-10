using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using static SoundManager;

public class SoundManager : Singleton<SoundManager>
{
    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    
    public List<AudioClip> sounds = new List<AudioClip>();
    public int numRoverAudioSources = 3;
    private List<AudioSource> roverAudioSources = new List<AudioSource>();
    
    public AudioSource roverEngine;

    public GameObject rover;

    public int numAudioSources = 20;
    public float volumeAudioSources = 0.4f;
    private List<GameObject> Audiosources = new List<GameObject>();
    private GameObject audiosourcesparent;

    public AudioSource radioGalaxia;
    public List<AudioClip> radioGalaxiaClips = new List<AudioClip>();
    public List<AudioClip> radioGalaxiaInterclip = new List<AudioClip>();
    public bool radioGalaxiaIsSong;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        if (sounds.Count > 0)
        {
            foreach(var clip in sounds)
            {
                audioClips.Add(clip.name, clip);
            }
        }
        sounds.Clear();

        rover = GameObject.FindGameObjectWithTag("Player");

        for(int i = 0; i < numRoverAudioSources; i++)
        {
            AudioSource source = rover.AddComponent<AudioSource>();
            source.loop = false;
            source.priority = 140;
            roverAudioSources.Add(source);
        }

        audiosourcesparent = new GameObject("AudioSources");
        for(int i = 0; i < numAudioSources; i++)
        {
            GameObject source = new GameObject("AudioSource");
            source.transform.parent = audiosourcesparent.transform;
            AudioSource audio = source.AddComponent<AudioSource>();
            source.GetComponent<AudioSource>().volume = volumeAudioSources;
            audio.spatialBlend = 1;
            audio.loop = false;
            audio.minDistance = 10;
            Audiosources.Add(source);
            source.SetActive(false);
        }
        radioGalaxia.priority = 150;
    }

    // Update is called once per frame
    void Update()
    {
        if (!radioGalaxia.isPlaying)
        {
            if(radioGalaxiaIsSong)
            {
                int randomClip = Random.Range(0, radioGalaxiaClips.Count);
                radioGalaxia.clip = radioGalaxiaClips[randomClip];
                radioGalaxia.Play();
                radioGalaxiaIsSong = false;
            }
            else
            {
                radioGalaxiaIsSong = true;
                int randomClip = Random.Range(0, radioGalaxiaInterclip.Count);
                radioGalaxia.clip = radioGalaxiaInterclip[randomClip];
                radioGalaxia.Play();
            }
        }

        foreach (var source in Audiosources)
        {
            if (source.activeInHierarchy)
            {
                if(source.GetComponent<AudioSource>().isPlaying == false)
                {
                    source.SetActive(false);
                }
            }
        }
    }

    public AudioSource PlaySoundOnRover(string namesound)
    {
        if(audioClips.ContainsKey(namesound))
        {
            bool soundPlayed = false;
            int counter = 0;
            while (!soundPlayed || roverAudioSources.Count < counter)
            {
                if (!roverAudioSources[counter].isPlaying)
                {
                    soundPlayed = true;
                    roverAudioSources[counter].clip = audioClips.GetValueOrDefault(namesound);
                    roverAudioSources[counter].Play();
                    return roverAudioSources[counter];
                }
                counter++;
            }
        }
        return null;
    }

    public GameObject CreateSound(string soundName)
    {
        int countSounds = 0;
        if (audioClips.ContainsKey(soundName))
        {

            for( int i = 0; i < Audiosources.Count; i++)
            {
                if (!Audiosources[i].GetComponent<AudioSource>().isPlaying)
                {
                    Audiosources[i].SetActive(true);
                    Audiosources[i].GetComponent<AudioSource>().clip = audioClips.GetValueOrDefault(soundName);
                    Audiosources[i].GetComponent<AudioSource>().Play();
                    return Audiosources[i];
                }
            }
            for (int i = 0; i < Audiosources.Count; i++)
            {
                if (Audiosources[i].GetComponent<AudioSource>().clip.name == "Rotor")
                {
                    if(countSounds < 2)
                    {
                        countSounds++;
                    }
                    else
                    {
                        Audiosources[i].SetActive(true);
                        Audiosources[i].GetComponent<AudioSource>().clip = audioClips.GetValueOrDefault(soundName);
                        Audiosources[i].GetComponent<AudioSource>().Play();
                        return Audiosources[i];
                    }
                }
            }
        }
        return null;
    }
}
