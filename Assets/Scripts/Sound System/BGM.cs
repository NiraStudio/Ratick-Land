﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {
    public enum State
    {
        Main,BossFight,Relax
    }
    public static BGM Instance;

  

    public State state;
    public bool Manager;
    public SoundByState[] Sounds;

    public Sound BetweenSound;
    public float BetweenTime;


    public AudioSource Audio;

	// Use this for initialization
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
        


    }
    #endregion

    void Start()
    {
        Audio = gameObject.AddComponent<AudioSource>();
        Audio = GetComponent<AudioSource>();
    }


    public void ChangeState(State state)
    {
        this.state=state;
    }


    Sound soundByState(State state)
    {
        Sound t = new Sound();
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (Sounds[i].state == state)
            {
                t = Sounds[i].Clip;
            }
        }
        print(t.ClipId);
        return t;
    }
    public void PlaySound(State state)
    {
        Sound a = soundByState(state);
        ChangeAudio(a);
        Audio.Play();
    }
    public void stopSound()
    {
        Audio.Stop();
    }
    public void ResumeSound()
    {
        Audio.UnPause();
    }
    public void pauseSound()
    {
        Audio.Pause();
    }

    IEnumerator changeSong(State s)
    {
        ChangeAudio(BetweenSound);
        yield return new WaitForSeconds(BetweenTime);
        ChangeAudio(soundByState(s));
    }
    void ChangeAudio(Sound s)
    {
        if(Audio==null)
        {
            Audio = GetComponent<AudioSource>();
            if(Audio==null)
                Audio = gameObject.AddComponent<AudioSource>();

        }
        Audio.clip = s.Clip;
        Audio.volume = s.Volume;
        Audio.pitch = s.Pitch;
        Audio.loop = s.loop;
        Audio.playOnAwake = s.PlayOnAwake;
    }
    [System.Serializable]
    public class SoundByState{
        public State state;
        public Sound Clip;
    }
}
