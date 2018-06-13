using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public GameObject gameManager;
    public Camera GameCamera;

    public GameObject gameCanvas;
    public GameObject pregameButton;
    public GameObject postGamePanel;
    public GameObject scoreCounter;

    public GameObject homeGarageCanvas;
    public GameObject garageScreen;
    public GameObject homeScreen;

    public List<GameObject> allCars;

    public int playerChosenCar;

    public delegate void StartGameDelegate();
    public static StartGameDelegate objectsBeginMoving;

    public delegate void PreGameDelegate();
    public static PreGameDelegate destroyGameManager;

    private GameObject carInstance;
    private Transform modelCarHolder;
    private List<GameObject> carList;

	void Awake()
	{

        //If we don't currently have a UI manager...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
        {
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
        }

        //start up at home page
        TransitionToHome();

        carList = new List<GameObject>();

        //creating an organizational tool
        modelCarHolder = new GameObject("Model Car Holder").transform;

        //instantiating all model cars
        for (int i = 0; i < 8; i++){
            InstantiateModelCar(i);
        }

        RectTransform myCanvasTransform = homeGarageCanvas.GetComponent<RectTransform>();
        modelCarHolder.SetParent(myCanvasTransform);

        SetCarmodel(playerChosenCar, carList);

	}


    //transitions back to home page
    public void TransitionToHome()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        gameCanvas.SetActive(false);

        homeGarageCanvas.SetActive(true);
        homeScreen.SetActive(true);
        garageScreen.SetActive(false);

        GameCamera.enabled = false;
    }
	
    //transitions to the pregame page
    public void TransitionToPregame()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        GameCamera.enabled = true;

        gameCanvas.SetActive(true);
        homeGarageCanvas.SetActive(false);

        scoreCounter.GetComponent<Text>().enabled = false;
        postGamePanel.SetActive(false);
        pregameButton.SetActive(true);

        SetupGame();

        GameCamera.GetComponent<Transform>().position = new Vector3(0, 5.95f, -10);

    }

    //transitions to postgame page
    public void TransitionToPostGame()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        GameCamera.enabled = true;

        gameCanvas.SetActive(true);
        homeGarageCanvas.SetActive(false);

        scoreCounter.SetActive(true);
        postGamePanel.SetActive(true);
        pregameButton.SetActive(false);

    }

    //transistions to garage page
    public void TransitionToGarage()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        GameCamera.enabled = false;

        gameCanvas.SetActive(false);

        homeGarageCanvas.SetActive(true);
        homeScreen.SetActive(false);
        garageScreen.SetActive(true);

    }

    // starts the game from the instructionsButton 
    private void StartGame()
    {
        pregameButton.SetActive(false);
        scoreCounter.GetComponent<Text>().enabled = true;
        //setting all the moving objects in motion
        objectsBeginMoving();

    }

    private void InstantiateModelCar(int chosenCar){
        //creating the car on the homepage
        carInstance = Instantiate(allCars[chosenCar], new Vector3(800, -360, -300), Quaternion.Euler(193, 90, -160));
        carInstance.AddComponent<RectTransform>();
        carInstance.GetComponent<RectTransform>().localScale = new Vector3(160, 160, 160);

        if (carInstance.tag == "Bus")
        {
            carInstance.GetComponent<RectTransform>().localScale = new Vector3(100, 100, 100);
        }

        //setting the homepage car to be under our model car holder
        carInstance.transform.SetParent(modelCarHolder);

        carInstance.AddComponent<RotateOnTouch>();

        //adding all the car models to our list
        carList.Add(carInstance);
    }

    //initializes the gameManager which starts the game
    private void SetupGame()
    {
        //if the game is already running, destroy it before starting a new game
        //if it's not running, just start
        if (GameManager.instance == null)
        {
            Instantiate(gameManager, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            destroyGameManager();
            GameManager.instance = null;
            Player.instance = null;
            Instantiate(gameManager, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

	private void OnEnable()
	{
        Player.GameOver += TransitionToPostGame;
	}

	private void OnDisable()
	{
        Player.GameOver -= TransitionToPostGame;
	}

    private void SetCarmodel(int carModel, List<GameObject> carModelList){
        for (int i = 0; i < carModelList.Capacity; i++){
            if (i == carModel)
            {
                carModelList[i].SetActive(true);
            }
            else carModelList[i].SetActive(false);
        }
    }

    //car picking functions

    public void PickRedCar(){
        playerChosenCar = 0;
        SetCarmodel(playerChosenCar, carList);
    }

    public void PickYellowCar()
    {
        playerChosenCar = 1;
        SetCarmodel(playerChosenCar, carList);
    }

    public void PickGreenCar()
    {
        playerChosenCar = 2;
        SetCarmodel(playerChosenCar, carList);
    }

    public void PickBlueCar()
    {
        playerChosenCar = 3;
        SetCarmodel(playerChosenCar, carList);
    }

    public void PickPoliceCar()
    {
        playerChosenCar = 4;
        SetCarmodel(playerChosenCar, carList);
    }

    public void PickTaxiCar()
    {
        playerChosenCar = 5;
        SetCarmodel(playerChosenCar, carList);
    }

    public void PickBlueBus()
    {
        playerChosenCar = 6;
        SetCarmodel(playerChosenCar, carList);
    }

    public void PickYellowBus()
    {
        playerChosenCar = 7;
        SetCarmodel(playerChosenCar, carList);
    }
}
