using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //implementing singleton design pattern

    public static GameManager instance = null;

    // Use this for initialization
    void Awake()
    {
        //If we don't currently have a game manager...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }
}
