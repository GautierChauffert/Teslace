using UnityEngine.Audio;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    public Audio[] audios;
	// Use this for initialization
	void Awake () {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
            

		foreach(Audio audio in audios)
        {
            audio.source = gameObject.AddComponent<AudioSource>();

            audio.source.clip = audio.clip;
            audio.source.loop = audio.loop;
            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
        }
	}

    public void Play(string soundName)
    {
        Audio audio = audios.Where(a => a.name == soundName).FirstOrDefault();

        if(audio == null)
        {
            Debug.LogWarning("Le son " + soundName + " n'existe pas.");
            return;
        }

        if (audio.theme)
        {
            // On arrête tous les sons qui loop avant.
            foreach (Audio a in audios.Where(a => a.theme && a.source.isPlaying))
                a.source.Stop();
        }
        if (audio.unique && audio.source.isPlaying)
            return;
        audio.source.Play();
    }

    public void Stop(string soundName)
    {
        Audio audio = audios.Where(a => a.name == soundName).FirstOrDefault();

        if (audio == null)
        {
            Debug.LogWarning("Le son " + soundName + " n'existe pas.");
            return;
        }

        audio.source.Stop();
    }


}
