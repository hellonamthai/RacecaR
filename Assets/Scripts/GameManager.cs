using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour {

    //implementing singleton design pattern

    public static GameManager instance = null;

    private GameData gameData;
    private Text gameOverPanelText;
    private Text distanceText;
    private int playerScore;
    private string gameDataProjectFilePath = "/StreamingAssets/data.json";

    // Use this for initialization
    void Awake()
    {
        //If we don't currently have a game manager...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this){
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
        }

        //finding references to the score texts
        distanceText = GameObject.Find("ScoreCounterText").GetComponent<Text>();
        playerScore = 0;
        LoadGameData();

    }

	void Update()
	{
        distanceText.text = "Distance: " + playerScore + "m";
	}

    public void IncreaseScore()
    {
        playerScore++;
    }

    //destroys the game manager object when starting a new game
    private void DestroyThis(){
        Destroy(gameObject);
    }

    //updates high score and displays your score
    private void FinishedGame(){
        gameData.UpdateHighScore(playerScore);
        gameOverPanelText = GameObject.Find("FinalScoreText").GetComponent<Text>();
        gameOverPanelText.text = "Distance drove: " + playerScore + "m"  + "\nYour Best: " + gameData.highScore + "m";
        SaveGameData();
    } 

	private void OnEnable()
	{
        Player.GameOver += FinishedGame;
        UIManager.destroyGameManager += DestroyThis;
	}

    private void OnDisable()
    {
        Player.GameOver -= FinishedGame;
        UIManager.destroyGameManager -= DestroyThis;
    }

    //loads the game data from the json file into an instance of GameData
    private void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            gameData = new GameData();
        }
    }

    //saves the game data from an instance of GameData into the json file
    private void SaveGameData()
    {

        string dataAsJson = JsonUtility.ToJson(gameData);

        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);

    }


}
