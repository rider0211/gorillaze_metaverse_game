using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    #region Singleton

    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    public Sound[] sounds;

    public bool canPlaySfx = true, canPlayBg = true;

    public AudioSource bgMusic;

    private void Start()
    {
        getPlayerPrefs();

        foreach (Sound s in sounds)
        {
            s.audioSrc = gameObject.AddComponent<AudioSource>();

            s.audioSrc.clip = s.audioClip;
            s.audioSrc.volume = s.Volume;
            s.audioSrc.pitch = s.pitch;
            s.audioSrc.loop = s.isLoop;
        }
    }
    public void playSFX(string clip)
    {
        if (canPlaySfx)
        {
            Sound s = Array.Find(sounds, item => item.clipName == clip);
            if (s == null)
            {
                Debug.LogWarning("No Clip found to play with name " + clip);
                return;
            }
            s.audioSrc.Play();
        }
    }
    public void stopSFX(string clip)
    {
        Sound s = Array.Find(sounds, item => item.clipName == clip);
        if (s == null)
        {
            Debug.LogWarning("No clip found to stop with name " + clip);
            return;
        }

        s.audioSrc.Pause();
    }

    void getPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("sfx"))
        {
            PlayerPrefs.SetInt("sfx", 1);
            PlayerPrefs.SetInt("bgMusic", 1);

            canPlaySfx = true;
            canPlayBg = true;
        }
        else
        {
            canPlaySfx = PlayerPrefs.GetInt("sfx") == 1 ? true : false;
            canPlayBg = PlayerPrefs.GetInt("bgMusic") == 1 ? true : false;

            if (!canPlayBg)
            {
                bgMusic.Stop();
            }
            else
            {
                bgMusic.Play();
            }
        }


    }
}

[Serializable]
public class Sound
{
    public string clipName;

    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float Volume;

    [Range(0.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource audioSrc;

    public bool isLoop = false;
}
