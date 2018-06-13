using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {

    public static ScrollingObject instance;
    
    public int minSpeed = 10;
    public int maxSpeed = 30;

    private Rigidbody myRigidbody;

	void Start()
	{
        myRigidbody = GetComponent<Rigidbody>();
	}

	public void AddForce()
	{
        //adding a force to the game object
        myRigidbody.velocity = new Vector3(0, 0, -Random.Range(minSpeed, maxSpeed));
	}

    void FreezeMotion(){
        myRigidbody.isKinematic = true;
    }

    private void OnEnable()
    {
        Player.GameOver += FreezeMotion;
        UIManager.objectsBeginMoving += AddForce;
    }
    private void OnDisable()
    {
        Player.GameOver -= FreezeMotion;
        UIManager.objectsBeginMoving -= AddForce;
    }

}
