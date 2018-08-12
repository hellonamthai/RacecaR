using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSound : MonoBehaviour {

    public AudioSource carStartUpSound;
    public AudioSource carEngineLoop;
    public Button forwardButton;
    public Button reverseButton;

    public void OnPress(){
        carStartUpSound.Play();
        Invoke("PlayLoop", 5.5f);

    }

    public void OnRealease(){
        carStartUpSound.Stop();
        carEngineLoop.Stop();
    }

    private void PlayLoop(){
        carEngineLoop.Play();
    }
}
