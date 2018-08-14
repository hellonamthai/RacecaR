using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCarBehindPlanes : MonoBehaviour {

    public GameObject carSliver1;
    public GameObject carSliver2;
    public GameObject carSliver3;
    public GameObject carSliver4;
    public GameObject carSliver5;
    public GameObject carSliver6;
    public GameObject carSliver7;
    public GameObject carSliver8;
    public GameObject carSliver9;
    public GameObject carSliver10;
    public GameObject carSliver11;
    public GameObject carSliver12;
    public GameObject carSliver13;
    public GameObject carSliver14;
    public GameObject carSliver15;
    public GameObject carSliver16;
    public GameObject carSliver17;
    public GameObject carSliver18;

    private List<GameObject> wholeCar;


	private void Awake()
	{
        //adding all the car slivers that make up our car model so that it's easier to work with
        wholeCar = new List<GameObject>() { carSliver1, carSliver2, carSliver3, carSliver4, carSliver5, carSliver6, carSliver7,
            carSliver8, carSliver9, carSliver10, carSliver11, carSliver12, carSliver13, carSliver14, carSliver15, carSliver16, carSliver17, carSliver18};

	}

	// Update is called once per frame
	void FixedUpdate () {

        Vector3 cameraPosition = transform.position;

        if (CarControls.firstTime == false){
            
            //for each section of the car, we are constatly shooting out rays to check whether our line of sight from the camera is blocked or not
            for (int i = 0; i < wholeCar.Count; i++)
            {

                Vector3 carSliverPosition = wholeCar[i].transform.GetChild(0).position;

                //shooting out a ray from the camera towards the car to see if we collide with any planes in between. If we do, we hide the car sliver.

                RaycastHit cameraToPlayerRaycast;

                bool didHit = Physics.Raycast(cameraPosition, carSliverPosition - cameraPosition, out cameraToPlayerRaycast, Mathf.Infinity);

                Debug.DrawRay(cameraPosition, carSliverPosition - cameraPosition, Color.red);

                if (didHit)
                {

                    //if we hit a plane in between the camera and the player, we hide the car by changing it's layermask
                    if (cameraToPlayerRaycast.collider.tag == "Plane")
                    {

                        wholeCar[i].layer = 10;
                    }
                    //if we hit the player directly instead, stop hiding the car sliver
                    else if (cameraToPlayerRaycast.collider.tag == "Player")
                    {

                        wholeCar[i].layer = 0;

                    }
                }
            }
        }
	}
}
