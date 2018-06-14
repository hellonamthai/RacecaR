using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.   

    public AudioSource menuMusic;                 //Drag a reference to the audio source which will play the music.
    public AudioSource garageMusic;
    public AudioSource driveMusic;
    public AudioSource wilhelmScream;


    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //when the game starts out, we play menu music
        PlayMenuMusic();
    }

    public void PlayMenuMusic(){
        menuMusic.Play();
        garageMusic.Stop();
        driveMusic.Stop();
    }

    public void PlayDriveMusic(){
        driveMusic.Play();
        menuMusic.Stop();
        garageMusic.Stop();
    }

    public void PlayGarageMusic(){
        garageMusic.Play();
        menuMusic.Stop();
        driveMusic.Stop();
    }

    public void PlayEndGameSound(){
        garageMusic.Stop();
        menuMusic.Stop();
        driveMusic.Stop();

        wilhelmScream.Play();
    }

}