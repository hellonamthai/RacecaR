using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : ScrollingObject {

    private bool currentlyDodging;
    private float raycastDistance;

	// Use this for initialization
	void Awake () {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        gameObject.layer = LayerMask.NameToLayer("Obstacle");
        currentlyDodging = false;
        raycastDistance = 7f;
        //if the obstacle car is a bus, we need to cast our ray further
        if(gameObject.tag == "Bus"){
            raycastDistance = 9f;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //converting Layer Mask to bit value. This is used to filter the raycast so that it only hits other obstacle cars.
        int layerMask = 1 << LayerMask.NameToLayer("Obstacle");

        //shooting a ray in front of the car to see if it's about to bump into another car
        //shooting from the left and right of the car
        RaycastHit hitLeft;
        RaycastHit hitRight;

        Vector3 posLeft = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
        Vector3 posRight = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);

        bool didHitLeft = Physics.Raycast(posLeft, transform.TransformDirection(Vector3.forward), out hitLeft, raycastDistance, layerMask);
        bool didHitRight = Physics.Raycast(posRight, transform.TransformDirection(Vector3.forward), out hitRight, raycastDistance, layerMask);

        bool dodgeLeft;

        //if any of our raycasts hit, meaning there is another car in front
        if (didHitLeft || didHitRight && !currentlyDodging)
        {
            //if the car is already too far on the right
            if(transform.position.x > GameManager.instance.GetComponent<BoardManager>().roadRightEdgeX-1){
                dodgeLeft = true;
            }
            //if the car is already too far on the left
            else if(transform.position.x < GameManager.instance.GetComponent<BoardManager>().roadLeftEdgeX+1){
                dodgeLeft = false;
            }
            else {
                //if the car's left raycast hit, it should dodge to the right
                if (didHitLeft)
                {
                    dodgeLeft = false;
                }
                //if the car's right raycast hit, it should dodge to the left
                else dodgeLeft = true;
            }

            IEnumerator dodgeCoroutine = Dodge(dodgeLeft);
            StartCoroutine(dodgeCoroutine);

        }
	}

    //makes obstacle cars dodge around each other
    IEnumerator Dodge(bool dodgeLeft){

        //choosing the car's initial turn direction
        float turnDirection;
        if (dodgeLeft)
        {
            turnDirection = 200;
        }
        else turnDirection = 160;


        //turning the car so it can dodge
        GetComponent<Transform>().rotation = Quaternion.Euler(0, turnDirection, 0);

        //calling the dodge coroutine with the boolean parameter
        currentlyDodging = true;
        IEnumerator smoothDodgeCoroutine= SmoothDodge(dodgeLeft);
        yield return StartCoroutine(smoothDodgeCoroutine);
        currentlyDodging = false;

        //turning the car back to it's original orientation
        GetComponent<Transform>().rotation = Quaternion.Euler(0, 180, 0);
    }



    //coroutine that lets obstacle cars swerve around cars while updating frame by frame
    IEnumerator SmoothDodge(bool dodgeLeft)
    {
        //choosing dodge direction
        float dodgeDirection;
        if (dodgeLeft)
        {
            dodgeDirection = -1;
        }
        else dodgeDirection = 1;


        //each frame move the car 0.01 to the left or right
        for (int i = 1; i < 7; i++)
        {
            transform.Translate(new Vector3(0.1f * dodgeDirection, 0, 0), Space.World);
            //changing how long it takes to dodge based on in game time
            if(Time.time < 15){
                yield return new WaitForSeconds(0.1f-Time.time*0.05f);
            }
            else yield return null;
        }
    }
}
