using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    public GameObject tile;
    public GameObject recyclingBox;
    public GameObject road;

    [HideInInspector]public float roadLeftEdgeX;
    [HideInInspector]public float roadRightEdgeX;

    private Transform boardHolder;

	void Awake()
	{
        boardHolder = new GameObject("Board").transform;
        SetUpBoard();
        //Used to put all the road and obstacle components under one board parent; purely organizational
        boardHolder.SetParent(GetComponent<GameManager>().transform);
	}


    public void SetUpBoard(){

        //Setting up the road
        GameObject roadInstance = Instantiate(road, new Vector3(0, 0, 55), Quaternion.identity);
        roadInstance.transform.SetParent(boardHolder);
        roadLeftEdgeX = roadInstance.transform.localPosition.x - (roadInstance.transform.localScale.x / 2f);
        roadRightEdgeX = roadInstance.transform.localPosition.x + (roadInstance.transform.localScale.x / 2f);


        //Setting up the recycling box
        GameObject recyclingBoxInstance = Instantiate(recyclingBox, new Vector3(0.07f, 24, -10), Quaternion.identity);
        recyclingBoxInstance.transform.SetParent(boardHolder);

        GameObject tileInstance;
        //Setting up our tiles that lie on our road
        for (int i = 0; i < 13; i++)
        {
            tileInstance = Instantiate(tile, new Vector3(0, 0.1f, 2 + 10 * i), Quaternion.identity);
            tileInstance.transform.SetParent(boardHolder);
        }

    }
}
