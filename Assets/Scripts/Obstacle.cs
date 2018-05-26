using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : ScrollingObject {

	// Use this for initialization
	void Awake () {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        gameObject.layer = LayerMask.NameToLayer("Obstacle");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //converting Layer Mask to bit value. This is used to filter the raycast so that it only hits other obstacle cars.
        int layerMask = 1 << LayerMask.NameToLayer("Obstacle");

        //shooting a ray in front of the car to see if it's about to bump into another car
        //shooting from the left, center, and right of the car
        RaycastHit hitLeft;
        RaycastHit hitRight;
        RaycastHit hit;

        Vector3 posLeft = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
        Vector3 posRight = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);

        bool didHitLeft = Physics.Raycast(posLeft, transform.TransformDirection(Vector3.forward), out hitLeft, 5, layerMask);
        bool didHitRight = Physics.Raycast(posRight, transform.TransformDirection(Vector3.forward), out hitRight, 5, layerMask);
        bool didHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5, layerMask);

        //if any of our raycasts hit, meaning there is another car in front
        if (didHitLeft || didHitRight || didHit)
        {
            Dodge();
        }
	}

    //makes obstacle cars dodge around each other
    void Dodge(){
        GetComponent<Transform>().rotation = Quaternion.Euler(0, 200, 0);
        StartCoroutine("SmoothDodge");
        StartCoroutine("SmoothRotateBack");
    }



    //coroutine that lets obstacle cars swerve around cars while updating frame by frame
    IEnumerator SmoothDodge()
    {
        //each frame move the car 0.1 to the left or right
        for (float f = 0.1f; f < 0.5f; f += 0.1f)
        {
            transform.Translate(new Vector3(0.1f, 0, 0), Space.World);
            yield return null;
        }
    }

    //coroutine that lets obstacle cars smoothly rotate back after rotating to swerve around cars
    IEnumerator SmoothRotateBack()
    {
        while (transform.rotation.eulerAngles.y > 180)
        {
            transform.Rotate(Vector3.down);
            yield return null;
        }
    }
}
