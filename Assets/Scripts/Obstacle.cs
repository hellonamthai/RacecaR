using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : ScrollingObject {

	// Use this for initialization
	void Awake () {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //shooting a ray in front of
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 8) && hit.collider.GetComponent<Obstacle>() != null)
        {
            Dodge();
        }
	}

    void Dodge(){
        GetComponent<Transform>().Translate(1.5f, 0, 0);
    }


    IEnumerator SmoothDodge()
    {
        for (float f = 0.1f; f < 1.5f; f += 0.1f)
        {
    
            yield return new WaitForSeconds(.1f);
        }
    }
}
