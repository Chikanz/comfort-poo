using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int playerIndex = 0;
    private GameMan manager;
    private int laneIndex = 0; //current lane
    public float lerpSpeed;
    public Vector3 Velocity;
    public Vector3 Acceleration;

    private KeyCode GetLeft => playerIndex == 0 ? KeyCode.A : KeyCode.LeftArrow;
    private KeyCode GetRight => playerIndex == 0 ? KeyCode.D : KeyCode.RightArrow;
    private KeyCode GetJump => playerIndex == 0 ? KeyCode.W : KeyCode.UpArrow;

    private bool jumping;
    public float jumpForce;
    Vector3 jumpVector;
    public float windTime = 1;

    public float addTime = 0.2f;
    private float addTimer = 0;

    float startY;

    // Use this for initialization
    void Start ()
    {
        manager = GetComponentInParent<GameMan>();
        startY = transform.position.y;        
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Clamp Jump
        if (transform.position.y < startY)
        {
            transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        }

        if (!jumping)
        {
            if (Input.GetKeyDown(GetLeft))
            {
                laneIndex -= 1;
            }

            if (Input.GetKeyDown(GetRight))
            {
                laneIndex += 1;
            }

            if(Input.GetKeyDown(GetJump))
            {                                
                jumping = true;
                addTimer = addTime;
            }
        }
        else if(jumping)
        {
            if(addTimer > 0)
            {
                Velocity += new Vector3(0, jumpForce, 0);
                addTimer -= Time.deltaTime;
            }            

            transform.position += Velocity;
            Velocity += Acceleration;   

            if(transform.position.y <= startY)
            {
                jumping = false;                
            }
        }

        laneIndex = Mathf.Clamp(laneIndex, 0, manager.Lanes.Length - 1);

        transform.position = Vector3.Lerp(transform.position, manager.GetLanePosition(laneIndex, playerIndex), lerpSpeed);

    }
}
