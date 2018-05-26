using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    public bool carHit = false;

    private Transform playerPosition;
    private int direction;


	// Update is called once per frame
	void FixedUpdate () {
        Swerve();
    
    }

    private void Swerve(){
        //Current player position 
        playerPosition = GetComponent<Transform>();

        //getting input
        direction = (int)Input.GetAxisRaw("Horizontal");

        //moving player based on position
        if (direction > 0)
        {
            //moving right and rotating the car a bit to that direction
            playerPosition.rotation = Quaternion.Euler(0, 20, 0);
            playerPosition.Translate(Vector3.right * 0.3f, Space.World);
        }
        else if (direction < 0)
        {
            //moving left and rotating the car a bit to that direction
            playerPosition.rotation = Quaternion.Euler(0, -20, 0);
            playerPosition.Translate(Vector3.left * 0.3f, Space.World);
        }
        else
        {
            //if the car was moving right but isn't anymore, slowly rotate it left back to starting position
            if (playerPosition.rotation.eulerAngles.y < 30 && playerPosition.rotation.eulerAngles.y > 1)
            {
                playerPosition.Rotate(Vector3.down);
            }
            //if the car was moving left but isn't anymore, slowly rotate it right back to starting position
            else if (playerPosition.rotation.eulerAngles.y > 330 && playerPosition.rotation.eulerAngles.y < 359)
            {
                playerPosition.Rotate(Vector3.up);
            }
        }
    }

    //stop the game if our player car is hit by another car
	private void OnTriggerEnter(Collider other)
	{
        if(other.tag != "Tile"){
            print("lose");
            carHit = true;
        }
	}

}
