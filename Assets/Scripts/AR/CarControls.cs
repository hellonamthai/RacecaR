using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControls : MonoBehaviour {

    public Button gasButton;
    public Button leftButton;
    public Button rightButton;

    public Transform myTransform;

	private void Update()
	{
        if(gasButton.GetComponent<Gas>().pressed){
            myTransform.Translate(Vector3.forward * 0.04f);
        }

        if(leftButton.GetComponent<TurnLeft>().pressed){
            myTransform.Rotate(Vector3.down);
        }

        if (rightButton.GetComponent<TurnRight>().pressed)
        {
            myTransform.Rotate(Vector3.up);
        }
	}

    public void SwitchPlacement()
    {
        Invoke("Switch", 2);
    }

    private void Switch()
    {
        UnityEngine.XR.iOS.UnityARHitTestExample.placement = !UnityEngine.XR.iOS.UnityARHitTestExample.placement;
    }
}
