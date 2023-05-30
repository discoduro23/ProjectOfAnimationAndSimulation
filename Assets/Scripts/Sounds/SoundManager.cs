using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class SoundManager : MonoBehaviour
{
    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    
    public List<AudioClip> sounds = new List<AudioClip>();
    public int numRoverAudioSources = 3;
    private List<AudioSource> roverAudioSources = new List<AudioSource>();
    
    public AudioSource roverEngine;

    public GameObject rover;

    // Start is called before the first frame update
    void Start()
    {
        if(sounds.Count > 0)
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioSource PlaySoundOnRover(string namesound)
    {
        if(audioClips.ContainsKey(namesound))
        {
            bool soundPlayed = false;
            int counter = 0;
            while (!soundPlayed || roverAudioSources.Count <= counter)
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
}
