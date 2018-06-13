using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {

    public static MySceneManager instance;

	// Use this for initialization
	void Awake () {
        /*
        //If we don't currently have a game manager...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
        {
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
        }

        //makes sure the scene manager doesn't get destroyed when loading a new scene
        DontDestroyOnLoad(this.gameObject); */
	}
	

    public void TransitionToAR(){
        SceneManager.LoadScene("UnityARKitScene");
    }

    public void TransitionToNormal(){
        SceneManager.LoadScene("Main");
    }

}
