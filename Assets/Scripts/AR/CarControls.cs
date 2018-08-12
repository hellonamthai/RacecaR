using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControls : MonoBehaviour {

    public Button gasButton;
    public Button reverseButton;
    public Button leftButton;
    public Button rightButton;

    public Transform myTransform;
    public Rigidbody myRB;

	private void Awake()
	{

        Screen.orientation = ScreenOrientation.LandscapeLeft;

        myRB.drag = 1;
        myRB.angularDrag = 0.5f;

        //hiding the car when the game first starts
        foreach (Transform child in transform)
        {
            child.gameObject.layer = 10;
        }
	}

	private void Update()
	{
        Vector3 forwardVector = myTransform.forward;
        float currentSpeed = myRB.velocity.magnitude;


        //if gas button is pressed, give the car acceleration
        if(gasButton.GetComponent<Gas>().pressed){
            myRB.velocity = myRB.velocity + forwardVector * 0.5f;
        }

        if(reverseButton.GetComponent<Reverse>().pressed){
            myRB.velocity = myRB.velocity - forwardVector * 0.5f;
        }

        if(leftButton.GetComponent<TurnLeft>().pressed){
            myTransform.Rotate(Vector3.down * (1+currentSpeed));
        }

        if (rightButton.GetComponent<TurnRight>().pressed)
        {
            myTransform.Rotate(Vector3.up * (1+currentSpeed));
        }
	}

    public void SwitchPlacement()
    {

        UnityEngine.XR.iOS.UnityARHitTestExample.placement = true;

        //setting the car to visible
        foreach (Transform child in transform)
        {
            child.gameObject.layer = 0;
        }

    }
}
