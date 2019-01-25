using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int playerIndex = 0;
    private GameMan manager;
    private int laneIndex = 0; //current lane
    public float lerpSpeed;    

    private KeyCode GetLeft => playerIndex == 0 ? KeyCode.A : KeyCode.LeftArrow;
    private KeyCode GetRight => playerIndex == 0 ? KeyCode.D : KeyCode.RightArrow;

    // Use this for initialization
    void Start ()
    {
        manager = GetComponentInParent<GameMan>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(GetLeft))
        {
            laneIndex -= 1;
        }

        if (Input.GetKeyDown(GetRight))
        {
            laneIndex += 1;            
        }

        laneIndex = Mathf.Clamp(laneIndex, 0, manager.Lanes.Length - 1);

        transform.position = Vector3.Lerp(transform.position, manager.GetLanePosition(laneIndex, playerIndex), lerpSpeed);

    }
}
