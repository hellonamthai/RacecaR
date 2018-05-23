using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    public GameObject tile;
    public GameObject recyclingBox;
    public GameObject road;

    private Transform boardHolder;


	void Awake()
	{
        SetUpBoard();
	}


    public void SetUpBoard(){

        //Used to put all the road and obstacle components under one board parent; purely organizational
        boardHolder = new GameObject("Board").transform;
        GameObject thisInstance;

        //Setting up the road
        thisInstance = Instantiate(road, new Vector3(0, 0, 55), Quaternion.identity);
        thisInstance.transform.SetParent(boardHolder);

        //Setting up the recycling box
        thisInstance = Instantiate(recyclingBox, new Vector3(0.07f, 2f, -10), Quaternion.identity);
        thisInstance.transform.SetParent(boardHolder);

        //Setting up our tiles that lie on our road
        for (int i = 0; i < 18; i++)
        {
            thisInstance = Instantiate(tile, new Vector3(0, 0.1f, 2 + 10 * i), Quaternion.identity);
            thisInstance.transform.SetParent(boardHolder);
        }

    }
}
