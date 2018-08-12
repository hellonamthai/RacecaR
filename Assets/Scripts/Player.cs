using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    public static Player instance = null;

    public delegate void gameOverDelegate();
    public static event gameOverDelegate GameOver;

    private Transform playerPosition;
    private float playerSwerveSpeed = 0.3f;
    private int turnAngle = 20;
    private GameObject myCamera;
    private bool playerCanMove;
    private float screenCenter = Screen.width / 2;
    private Vector2 tapPoint;
    private float direction;

    private float roadRightEdge;
    private float roadLeftEdge;

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

        //finding the right and left edges of the road through the board manager
        GameObject myBoardManager = GameObject.Find("BoardManager");
        roadLeftEdge = myBoardManager.GetComponent<BoardManager>().roadLeftEdgeX;
        roadRightEdge = myBoardManager.GetComponent<BoardManager>().roadRightEdgeX;
    }


	// Update is called once per frame
	void FixedUpdate () {
        if(playerCanMove){
            Swerve();
        }
    }

    private void Swerve(){
        
        //getting input for controls depending on build version

        #if UNITY_STANDALONE || UNITY_WEBPLAYER
     
        direction = Input.GetAxisRaw("Horizontal");

        #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

        if (Input.touchCount > 0)
        {
            tapPoint = Input.GetTouch(0).position;
            direction = tapPoint.x;
        }
        else
        {
            direction = screenCenter;
        }

        #endif //End of mobile platform dependendent compilation section started above with #elif


        //float direction = Input.GetAxisRaw("Horizontal");

        //moving player based on position
        if (direction > screenCenter)
        {
            //moving right and rotating the car a bit to that direction
            playerPosition.rotation = Quaternion.Euler(0, 1*turnAngle, 0);
            //can only swerve more to the right if not at the right end of the road
            if(playerPosition.position.x < roadRightEdge-0.5f){
                //moving both the player and the camera
                playerPosition.Translate(Vector3.right * playerSwerveSpeed * Time.deltaTime*60, Space.World);
                myCamera.transform.Translate(Vector3.right * playerSwerveSpeed * Time.deltaTime*60, Space.World);
            }
        }
        else if (direction < screenCenter)
        {
            //moving left and rotating the car a bit to that direction
            playerPosition.rotation = Quaternion.Euler(0, -1*turnAngle, 0);
            //can only swerve more to the right if not at the right end of the road
            if(playerPosition.position.x > roadLeftEdge+0.5f){
                //moving both the player and the camera
                playerPosition.Translate(Vector3.left * playerSwerveSpeed * Time.deltaTime*60, Space.World);
                myCamera.transform.Translate(Vector3.left * playerSwerveSpeed * Time.deltaTime*60, Space.World);
            }
        }
        else {
            //if the car was moving right but isn't anymore, slowly rotate it left back to starting position
            if (playerPosition.rotation.eulerAngles.y < turnAngle + 1 && playerPosition.rotation.eulerAngles.y > 1)
            {
                playerPosition.Rotate(Vector3.down * Time.deltaTime * 60);
            }
            //if the car was moving left but isn't anymore, slowly rotate it right back to starting position
            else if (playerPosition.rotation.eulerAngles.y > 360 - (turnAngle + 1) && playerPosition.rotation.eulerAngles.y < 359)
            {
                playerPosition.Rotate(Vector3.up * Time.deltaTime * 60);
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
            //other.isTrigger = false;
            //GetComponent<Collider>().isTrigger = false;
            //Rigidbody playerRB = GetComponent<Rigidbody>();
            //Rigidbody otherRB = other.GetComponent<Rigidbody>();
            //CarCrash(ref playerRB, ref otherRB);
        }
	}


    //simulating a car crash
    private void CarCrash(ref Rigidbody playerCar, ref Rigidbody otherCar){
        
        otherCar.isKinematic = false;
        otherCar.constraints = RigidbodyConstraints.None;

        playerCar.isKinematic = false;
        playerCar.constraints = RigidbodyConstraints.None;

        Vector3 crashPositionPlayer = playerCar.GetComponent<Transform>().position + new Vector3(5, 0, -5);
        Vector3 crashPositionOther = otherCar.GetComponent<Transform>().position + new Vector3(-5, 0, 5);

        playerCar.AddExplosionForce(4000f, crashPositionPlayer, 10);
        otherCar.AddExplosionForce(4000f, crashPositionOther, 10);
        

        
    }

}
