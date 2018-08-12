using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    public GameObject tile;
    public GameObject recyclingBox;
    public GameObject road;
    public GameObject terrain;
    public GameObject mountain;

    [HideInInspector]public float roadLeftEdgeX;
    [HideInInspector]public float roadRightEdgeX;

    private Transform boardHolder;

	void Awake()
	{
        boardHolder = new GameObject("Board").transform;
        SetUpBoard();
        //Used to put all the road and obstacle components under one board parent; purely organizational
        boardHolder.SetParent(GetComponent<BoardManager>().transform);
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

        //Setting up our tiles that lie on our road
        GameObject tileInstance;
        for (int i = 0; i < 13; i++)
        {
            tileInstance = Instantiate(tile, new Vector3(0, 0.1f, 2 + 10 * i), Quaternion.identity);
            tileInstance.transform.SetParent(boardHolder);
        }

        //Setting up the tree landscape 
        GameObject terrainInstance;
        for (int i = 0; i < 2; i++){
            for (int k = 0; k < 2; k++){
                terrainInstance = Instantiate(terrain, new Vector3(43+20*i, -85, 106+119*k), Quaternion.identity);
                terrainInstance.transform.SetParent(boardHolder);
            }
        }

        //Setting up the tree landscape 
        for (int i = 0; i < 2; i++)
        {
            for (int k = 0; k < 2; k++)
            {
                terrainInstance = Instantiate(terrain, new Vector3(-14.5f + 20 * i, -85, 106 + 119 * k), Quaternion.identity);
                terrainInstance.transform.SetParent(boardHolder);
            }
        }

        //Setting up the mountain
        GameObject mountainInstance = Instantiate(mountain, new Vector3(0, -6, 147), Quaternion.identity);
        mountainInstance.transform.localScale = new Vector3(2.4f, 2.4f, 2.4f);
        mountainInstance.transform.SetParent(boardHolder);
    }
}
