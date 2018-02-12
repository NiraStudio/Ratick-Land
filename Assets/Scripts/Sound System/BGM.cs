using System.Collections;
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


    AudioSource Audio;

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

        return t;
    }



    IEnumerator changeSong(State s)
    {
        ChangeAudio(BetweenSound);
        yield return new WaitForSeconds(BetweenTime);
        ChangeAudio(soundByState(s));
    }
    void ChangeAudio(Sound s)
    {
        Audio.clip = s.Clip;
        Audio.volume = s.Volume;
        Audio.pitch = s.Pitch;
    }
    public class SoundByState{
        public State state;
        public Sound Clip;
    }
}
