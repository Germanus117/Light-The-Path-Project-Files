using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip GameClip;
    public AudioClip CombatClip;

    public EnemyManager enemy;

    AudioSource gameAudio;

    bool once;
    bool triggerNormal;

    private void Start()
    {
        gameAudio = GetComponent<AudioSource>();
        gameAudio.clip = GameClip;
    }

    private void Update()
    {
        if(enemy != null)
        {
            if (enemy.playerHasEntered == true)
            {
                triggerNormal = false;
                if (!once)
                {
                    ChangeClip(CombatClip);
                    once = true;
                }
                else
                {
                    once = false;
                    if (!triggerNormal)
                    {
                        ChangeClip(GameClip);
                        triggerNormal = true;
                    }
                    
                }
            }
        }
    }

    void ChangeClip(AudioClip music)
    {
        gameAudio.Stop();
        gameAudio.clip = music;
        gameAudio.Play();
    }
}
