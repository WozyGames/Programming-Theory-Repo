using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{

    [SerializeField, HideInInspector, InspectorName("Audio Source")] public AudioSource audioSource;
    [SerializeField, InspectorName("Audio Clip")] public AudioClip audioClip;    
    [SerializeField] public string name;
    [SerializeField, Range(0f, 1f)] public float volume;
    [SerializeField, Range(0.1f, 3f)] public float pitch;
    [SerializeField] public bool loop;

}
