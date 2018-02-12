using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public static SFX Instance;
    public bool Manager;

    public Sound[] sounds;

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
            t.pitch = sounds[i].Pitch / 300;
            audioSources[i] = t;
        }

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
    [Range(0, 100)]

    public float Volume;

    [Range(-300, 300)]
    public float Pitch;



}

