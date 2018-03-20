using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public static SFX Instance;
    public bool Manager;

    public Sound[] sounds;
    public bool IsReady
    {
        get { return ready; }
    }
    bool ready;
    AudioSource[] audioSources;
    #region Singleton
    void Awake()
    {
        if (!Manager)
            return;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { Destroy(this); }


    }
    #endregion
    // Use this for initialization
    void Start()
    {
        audioSources = new AudioSource[sounds.Length];

        AudioSource t;
        for (int i = 0; i < sounds.Length; i++)
        {
            t = gameObject.AddComponent<AudioSource>();
            t.clip = sounds[i].Clip;
            t.volume = sounds[i].Volume / 100;
            t.pitch = sounds[i].Pitch ;
            t.loop = sounds[i].loop;
            t.spatialBlend = sounds[i].Sound3D/100;
            t.playOnAwake = sounds[i].PlayOnAwake;
            t.minDistance = 0; t.maxDistance = sounds[i].Sound3DRange;
            t.rolloffMode = AudioRolloffMode.Linear;
            audioSources[i] = t;
        }
        ready = true;
    }

    // Update is called once per frame
    public void PlaySound(string ID)
    {
        if (!checkForID(ID))
        { return; }

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ClipId == ID)
            {
                audioSources[i].Play();

                break;
            }

        }
    }
    public void PauseSound(string ID)
    {
        if (!checkForID(ID))
        { return; }

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ClipId == ID)
            {
                audioSources[i].Pause();
                break;
            }

        }
    }
    public bool IsPlaying(string ID)
    {
        if (!checkForID(ID))
        { return false; }

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ClipId == ID)
            {
                return audioSources[i].isPlaying;
            }

        }
        return false;
    }
    public void UnPauseSound(string ID)
    {
        if (!checkForID(ID))
        { return; }

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ClipId == ID)
            {
                audioSources[i].UnPause();
                break;
            }

        }
    }

    public void StopSound(string ID)
    {
        if (!checkForID(ID))
        { return; }


        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ClipId == ID)
            {
                audioSources[i].Stop();
                break;
            }

        }
    }



    public void ChangeVolume(string ID, float Volume)
    {
        if (Volume < 0 || Volume > 100)
        {
            Debug.LogError("Sound Manager Error: Volume Must be Between 0 to 100");
            return;
        }
        if (!checkForID(ID))
        { return; }


        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ClipId == ID)
            {
                audioSources[i].volume=Volume/100;
                break;
            }

        }


    }


    public void ChangePitch(string ID, float Pitch)
    {
        if (Pitch < -300 || Pitch > 300)
        {
            Debug.LogError("Sound Manager Error: Pitch Must be Between -300 to 300");
            return;
        }
        if (!checkForID(ID))
        { return; }


        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ClipId == ID)
            {
                audioSources[i].pitch = Pitch / 300;
                break;
            }

        }


    }

    bool checkForID(string ID)
    {
        bool found = false;
        foreach (var item in sounds)
        {
            if (item.ClipId == ID)
            {
                found = true;
                break;
            }
        }

        if (found == false)
            Debug.LogError("Sound Manager Error: ID Is Unrecognizable");

        return found;
    }

}
[System.Serializable]
public class Sound
{
    public string ClipId;
    public AudioClip Clip;
    public bool loop,PlayOnAwake;
    [Range(0, 100)]

    public float Volume=100;

    [Range(-3, 3)]
    public float Pitch=1;

    [Range(0, 100)]
    public float Sound3D = 0;
    [Range(0, 100)]
    public float Sound3DRange = 100;



}

