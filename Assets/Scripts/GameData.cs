using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData{

    public int highScore;
    public bool[] unlockedCars;

    //updates the player's highest score
    public void UpdateHighScore(int newScore){
        if (newScore > highScore){
            highScore = newScore;
        }
    }

    //updates the player's unlocked cars
    public  void UpdateUnlockedCars(int newCar){
        unlockedCars[newCar] = true;
    }
}
