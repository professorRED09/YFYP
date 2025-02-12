using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script will manage almost all sounds that be used in the game
public class SoundObserver : MonoBehaviour, IObserver 
{
    [Header("Audio Source")]
    [SerializeField] AudioSource audioPlayer;

    [Header("SFX")]
    [SerializeField] AudioClip addSFX;

    [SerializeField] AudioClip removeSFX;

    [SerializeField] AudioClip correctSFX;

    [SerializeField] AudioClip wrongSFX;

    [SerializeField] AudioClip errorSFX;

    [SerializeField] AudioClip bugSFX;

    [SerializeField] AudioClip degitSFX;

    [SerializeField] AudioClip redoSFX;

    [SerializeField] AudioClip regisSFX;

    [Header("Subject")]
    [SerializeField] Subject playerSubject;
    [SerializeField] Subject RandomBugTimer;

    void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
    }
     
    public void OnNotify(PlayerAction action)
    {
        switch (action)
        {
            case (PlayerAction.add):               
                
                audioPlayer.clip = addSFX;
                audioPlayer.Play();                
                return;
            case (PlayerAction.remove):
                
                audioPlayer.clip = removeSFX;
                audioPlayer.Play();
                return;
            case (PlayerAction.correct):

                audioPlayer.clip = correctSFX;
                audioPlayer.Play();
                return;
            case (PlayerAction.wrong):

                audioPlayer.clip = wrongSFX;
                audioPlayer.Play();
                return;
            case (PlayerAction.error):

                audioPlayer.clip = errorSFX;
                audioPlayer.Play();
                return;
            case (PlayerAction.bug):

                audioPlayer.clip = bugSFX;
                audioPlayer.Play();                
                return;
            case (PlayerAction.degit):

                audioPlayer.clip = degitSFX;
                audioPlayer.Play();
                return;
            case (PlayerAction.redo):

                audioPlayer.clip = redoSFX;
                audioPlayer.Play();
                return;           
            default:
                return;
        }
    }

    void OnEnable()
    {
        playerSubject.AddObserver(this);
        RandomBugTimer.AddObserver(this);
    }

    void OnDisable()
    {
        playerSubject.RemoveObserver(this);
        RandomBugTimer.RemoveObserver(this);
    }
}
