using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsManager : MonoBehaviour {

    public static CarsManager instance;

    private List<GameObject> carsArray;
    private int playerCar;
    private Transform obstacleHolder;

	// Use this for initialization
	public void Awake () {
        carsArray = new List<GameObject>(UIManager.instance.allCars);
        playerCar = UIManager.instance.playerChosenCar;
        obstacleHolder = new GameObject("Obstacles").transform;
        InitializePlayerCar();
        carsArray.RemoveAt(playerCar);
        InitializeObstacleCars();
        //used to organize my obstacle cars
        obstacleHolder.SetParent(GetComponent<GameManager>().transform);
	}

    private void InitializePlayerCar(){
        //Setting the player object as the car model the player chose 
        GameObject playerInstance = Instantiate(carsArray[playerCar], new Vector3(0, 0.5f, -4.5f), Quaternion.identity);
        if (playerInstance.tag == "Bus")
        {
            playerInstance.GetComponent<Transform>().Translate(new Vector3(0, 0.3f, 0));
        }
        playerInstance.name = "Player";
        playerInstance.tag = "Player";
        playerInstance.AddComponent<Player>();
        playerInstance.transform.SetParent(GetComponent<GameManager>().transform);
    }

    private void InitializeObstacleCars(){

        //Setting up our set of obstacles that we're going to recycle
        for (int i = 0; i < 12; i++)
        {
            int randomCar = Random.Range(0, carsArray.Capacity-1);
            Vector3 randomPosition = new Vector3(Random.Range(-8.5f, 8.5f), 0.5f, Random.Range(20, 110));

            //creating a car obstacle randomly from my array and setting it at a random position on the platform
            GameObject obstacleInstance = Instantiate(carsArray[randomCar], randomPosition, Quaternion.Euler(0, 180, 0));
                            
            //if the obstacle is a bus, we need to move instantiate it a bit higher
            if (obstacleInstance.tag == "Bus")
            {
                obstacleInstance.GetComponent<Transform>().Translate(new Vector3(0, 0.3f, 0));
            }

            //attaching the obstacle script
            obstacleInstance.AddComponent<Obstacle>();

            //organizing under hierarchy
            obstacleInstance.transform.SetParent(obstacleHolder);
        }
    }
}
