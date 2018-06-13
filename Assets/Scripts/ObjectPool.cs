using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    private float minSpeedScale = 0.4f;
    private float maxSpeedScale = 0.7f;

    //recycling obstacles by placing them at the end again and giving them a random x position and speed
	private void OnTriggerEnter(Collider other)
	{
        //if it's a tile let it keep the same speed and always send it to the center
        if(other.tag == "Tile"){
            other.GetComponent<Transform>().position = new Vector3(0, 0.1f, 124f);
        }
        else if(other.tag == "Terrain"){
            float oldXpos = other.GetComponent<Transform>().position.x;
            float oldYpos = other.GetComponent<Transform>().position.y;
            other.GetComponent<Transform>().position = new Vector3(oldXpos, oldYpos, 224f);
        }
        else {
            //resetting the obstacle cars to the end of the platform at a random x position and increase their speed
            other.GetComponent<Transform>().position = new Vector3(Random.Range(-9, 9), 0.5f, 110f);
            other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -Random.Range(GameManager.instance.playerScore*minSpeedScale + 10, GameManager.instance.playerScore*maxSpeedScale + 30));

            //if bus, spawn it a bit higher and have it's velocity be slower
            if (other.tag == "Bus")
            {
                other.GetComponent<Transform>().Translate(new Vector3(0, 0.3f, 0));
                other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -Random.Range(GameManager.instance.playerScore*maxSpeedScale + 10, GameManager.instance.playerScore*maxSpeedScale + 30)*0.5f);
            }
        }
	}
}
