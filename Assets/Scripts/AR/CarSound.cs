using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSound : MonoBehaviour {

    public AudioSource carStartUpSound;
    public Button forwardButton;
    public Button reverseButton;

    public void OnPress(){
        carStartUpSound.Play();
    }

    public void OnRealease(){
        carStartUpSound.Stop();
    }
}
