using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {
    
    public int minSpeed = 10;
    public int maxSpeed = 30;

    private Rigidbody myRigidbody;
    private GameObject playerCar;

	void Start()
	{
        //adding a force to the game object
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = new Vector3(0, 0, -Random.Range(minSpeed, maxSpeed));
        playerCar = GameObject.FindWithTag("Player");
	}

	void Update()
	{
        if(playerCar.GetComponent<Player>().carHit == true){
            myRigidbody.isKinematic = true;
        }
	}

}
