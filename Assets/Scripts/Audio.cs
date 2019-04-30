using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio {
    public string name;
    public AudioClip clip;

    public bool theme;
    public bool unique;
    public bool loop;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;
}
