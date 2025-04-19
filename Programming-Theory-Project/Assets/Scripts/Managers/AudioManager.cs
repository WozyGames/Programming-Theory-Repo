using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
        }

    }

    void Start()
    {
        Play("MenuMusic");
    }

    public void Play(string name)
    {
        StopAllClips();
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.audioSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.audioSource.Play();
    }

    private void StopAllClips()
    {
        foreach (Sound s in sounds)
        {
            s.audioSource.Stop();
        }
    }

    public void PlayLevelMusic(string level)
    {

        StopAllClips();

        switch (level)
        {
            case "Level1":
                Play("Level1Music");
                break;
            case "Level2":
                Play("Level2Music");
                break;
            case "Level3":
                Play("Level3Music");
                break;
            case "Level4":
                Play("Level4Music");
                break;
            case "Level5":
                Play("Level5Music");
                break;
            case "Level6":
                Play("Level6Music");
                break;
            default:
                Play("MenuMusic");
                break;
        }
    }

}
