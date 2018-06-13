using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnTouch : MonoBehaviour {

    private int dragDirection;
    private float speed = 1f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // Move object across XY plane
            transform.Rotate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
        }

	}

}
