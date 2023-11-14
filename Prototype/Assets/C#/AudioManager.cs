using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }
    }

    void Start(){
        Play("CaveAMB");
        Play("LavaAMB");
    }


    public void Play (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("CAN'T FIND SOUND NAME, CHECK SPELLIN!");
            return;
        }
        else{
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.Play();
        }
    }
}
