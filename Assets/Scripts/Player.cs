using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    public static Player instance = null;

    public delegate void gameOverDelegate();
    public static event gameOverDelegate GameOver;

    private Transform playerPosition;
    private int direction;
    private float playerSwerveSpeed = 0.3f;
    private int turnAngle = 20;
    private GameObject myCamera;
    private bool playerCanMove;



    // Use this for initialization
    void Awake()
    {
        //If we don't currently have a player...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

        //initializing the player position and the edges of the road
        playerPosition = GetComponent<Transform>();

        //findin a reference to the main camera
        myCamera = GameObject.FindWithTag("MainCamera");
        playerCanMove = true;
    }


	// Update is called once per frame
	void FixedUpdate () {
        if(playerCanMove){
            Swerve();
        }
    }

    private void Swerve(){

        //getting input
        direction = (int)Input.GetAxisRaw("Horizontal");

        //moving player based on position
        if (direction > 0)
        {
            //moving right and rotating the car a bit to that direction
            playerPosition.rotation = Quaternion.Euler(0, 1*turnAngle, 0);
            //can only swerve more to the right if not at the right end of the road
            if(playerPosition.position.x < GameManager.instance.GetComponent<BoardManager>().roadRightEdgeX-0.5f){
                //moving both the player and the camera
                playerPosition.Translate(Vector3.right * playerSwerveSpeed, Space.World);
                myCamera.transform.Translate(Vector3.right * playerSwerveSpeed, Space.World);
            }
        }
        else if (direction < 0)
        {
            //moving left and rotating the car a bit to that direction
            playerPosition.rotation = Quaternion.Euler(0, -1*turnAngle, 0);
            //can only swerve more to the right if not at the right end of the road
            if(playerPosition.position.x > GameManager.instance.GetComponent<BoardManager>().roadLeftEdgeX+0.5f){
                //moving both the player and the camera
                playerPosition.Translate(Vector3.left * playerSwerveSpeed, Space.World);
                myCamera.transform.Translate(Vector3.left * playerSwerveSpeed, Space.World);
            }
        }
        else
        {
            //if the car was moving right but isn't anymore, slowly rotate it left back to starting position
            if (playerPosition.rotation.eulerAngles.y < turnAngle+1 && playerPosition.rotation.eulerAngles.y > 1)
            {
                playerPosition.Rotate(Vector3.down);
            }
            //if the car was moving left but isn't anymore, slowly rotate it right back to starting position
            else if (playerPosition.rotation.eulerAngles.y > 360-(turnAngle+1) && playerPosition.rotation.eulerAngles.y < 359)
            {
                playerPosition.Rotate(Vector3.up);
            }
        }
    }

    //stop the game if our player car is hit by another car
	private void OnTriggerEnter(Collider other)
	{
        if(other.tag == "Tile"){
            GameManager.instance.IncreaseScore();
        }
        else {
            //calling the delegate
            playerCanMove = false;
            GameOver();
        }
	}

}
