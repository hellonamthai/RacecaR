using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsManager : MonoBehaviour {

    public GameObject playerCar;
    public GameObject[] obstacleArray;

    private Transform obstacleHolder;

	// Use this for initialization
	public void Awake () {
        InitializePlayerCar();
        InitializeObstacleCars();
	}

    private void InitializePlayerCar(){
        //Setting the player object as the car model we assigned in the editor
        GameObject playerInstance = Instantiate(playerCar, new Vector3(0, 0.5f, -4.5f), Quaternion.identity);
        playerInstance.name = "Player";
        playerInstance.tag = "Player";
        playerInstance.AddComponent<Player>();
    }

    private void InitializeObstacleCars(){
        //used to organize my obstacle cars
        obstacleHolder = new GameObject("Obstacles").transform;


        //Setting up our set of obstacles that we're going to recycle
        for (int i = 0; i < 12; i++)
        {
            //creating a car obstacle randomly from my array and setting it at a random position on the platform
            GameObject obstacleInstance = Instantiate(obstacleArray[Random.Range(0, obstacleArray.Length)],
                                                      new Vector3(Random.Range(-9, 9), 0.5f, Random.Range(12, 110)), Quaternion.Euler(0, 180, 0));

                            
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
