using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    //all items necessary for gameplay
    public GameObject gameManager;
    public GameObject soundManager;
    public Camera GameCamera;

    //all gameobjects necessary for gameplay UI
    public GameObject gameCanvas;
    public GameObject pregameButton;
    public GameObject postGamePanel;
    public GameObject scoreCounter;

    //all items necessary for general UI
    public GameObject homeGarageCanvas;
    public GameObject garageScreen;
    public GameObject homeScreen;

    //a list of all car prefabs(models) available to use
    public List<GameObject> allCars;
    public int playerChosenCar;

    //delegate that calls functions when we start the game
    public delegate void StartGameDelegate();
    public static StartGameDelegate objectsBeginMoving;

    //delegate that calls functions when we transition to the pregame page
    public delegate void PreGameDelegate();
    public static PreGameDelegate destroyGameManager;

    private GameObject carInstance;
    private Transform modelCarHolder;
    private List<GameObject> carList;

    private Vector3 gameCameraPosition = new Vector3(0, 5.95f, -10);
    private Vector3 modelCarPosition = new Vector3(800, -380, -300);
    private Quaternion modelCarRotation = Quaternion.Euler(193, 90, -160);
    private Vector3 modelCarScale = new Vector3(160, 160, 160);
    private Vector3 modelBusScale = new Vector3(100, 100, 100);

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

        //creating organizational tools
        carList = new List<GameObject>();
        modelCarHolder = new GameObject("Model Car Holder").transform;

        //instantiating all model cars
        for (int i = 0; i < 8; i++){
            //instantiates all the different types of car prefabs at the same position in the home screen and puts them under the above orgranizational tools
            InstantiateModelCar(i);
        }

        //sets the model car holder (that holds all the model cars) to be under the homeGarage canvas gameobject
        RectTransform myCanvasTransform = homeGarageCanvas.GetComponent<RectTransform>();
        modelCarHolder.SetParent(myCanvasTransform);

        //sets the current car model to the car chosen by the player
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

        soundManager.GetComponent<SoundManager>().PlayMenuMusic();
    }
	
    //transitions to the pregame page
    public void TransitionToPregame()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        GameCamera.enabled = true;

        gameCanvas.SetActive(true);
        homeGarageCanvas.SetActive(false);

        postGamePanel.SetActive(false);
        pregameButton.SetActive(true);

        SetupGame();

        GameCamera.GetComponent<Transform>().position = gameCameraPosition;

        soundManager.GetComponent<SoundManager>().PlayDriveMusic();

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

        soundManager.GetComponent<SoundManager>().PlayEndGameSound();
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

        soundManager.GetComponent<SoundManager>().PlayGarageMusic();
    }

    // starts the game when the pregame panel Button is clicked
    private void StartGame()
    {
        pregameButton.SetActive(false);
        scoreCounter.GetComponent<Text>().enabled = true;
        //setting all the moving objects in motion
        objectsBeginMoving();

    }

    private void InstantiateModelCar(int chosenCar){
        //creating the car on the homepage
        carInstance = Instantiate(allCars[chosenCar], modelCarPosition, modelCarRotation);
        carInstance.AddComponent<RectTransform>();
        carInstance.GetComponent<RectTransform>().localScale = modelCarScale;

        if (carInstance.tag == "Bus")
        {
            carInstance.GetComponent<RectTransform>().localScale = modelBusScale;
        }

        //setting the homepage car to be under our model car holder
        carInstance.transform.SetParent(modelCarHolder);

        //adds the script so that the model car can be rotated by scrubbing
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
            Instantiate(gameManager, Vector3.zero, Quaternion.identity);
        }
        else
        {
            destroyGameManager();
            GameManager.instance = null;
            Player.instance = null;
            Instantiate(gameManager, Vector3.zero, Quaternion.identity);
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

    //sets the car model to be the one currently chosen by the player
    //it does this by setting all the other cars' status to false
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
    //each is called when their respective button is pressed and sets the model car to the chosen car

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
