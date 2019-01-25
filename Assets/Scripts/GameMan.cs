using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMan : MonoBehaviour
{
    public GameObject[] Lanes;
    private player[] players;
    private int[] playerLanes;

	// Use this for initialization
	void Start ()
    {
        players = GetComponentsInChildren<player>();
        playerLanes = new int[players.Length];
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public Vector3 GetLanePosition(int lane, int player)
    {
        playerLanes[player] = lane; //Update lane in manager

        if (player == 0)
        {           
            return Lanes[lane].transform.position - (Vector3.left * players[player == 0 ? 1 : 0].GetComponentInChildren<BoxCollider>().bounds.extents.magnitude);
        }
        else
        {            
            return Lanes[lane].transform.position; //normal pos
        }
    }
}
