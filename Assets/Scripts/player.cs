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

    public float TurnAngle = 15f;

    private KeyCode GetLeft => playerIndex == 0 ? KeyCode.A : KeyCode.LeftArrow;
    private KeyCode GetRight => playerIndex == 0 ? KeyCode.D : KeyCode.RightArrow;
    private KeyCode GetJump => playerIndex == 0 ? KeyCode.W : KeyCode.UpArrow;

    private bool jumping;
    public float jumpForce;
    Vector3 jumpVector;

    public float jumpHeight;

    public float JumpTime = 0.2f;
    private float JumpTimer = 0;

    float startY;    

    // Use this for initialization
    void Start ()
    {
        manager = GetComponentInParent<GameMan>();
        startY = transform.position.y;

        GetComponent<Animator>().SetFloat("Offset", Random.Range(0.0f, 1.0f));
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 targetPos = manager.GetLanePosition(laneIndex, playerIndex);        
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
                JumpTimer = JumpTime;
                GetComponent<Animator>().SetTrigger("Jump");
            }
        }
        else if(jumping)
        {
            if(JumpTimer > 0.1f)
            {
                if(JumpTimer < (JumpTime - 0.25f)) targetPos.y = jumpHeight;
                JumpTimer -= Time.deltaTime;
            }                 
            else
            {
                targetPos.y = startY;
                jumping = false;
            }
        }

        laneIndex = Mathf.Clamp(laneIndex, 0, manager.Lanes.Length - 1);


        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Junk"))
        {
            manager.Hit(playerIndex == 0);
            GetComponent<Animator>().SetTrigger("Hit");


            if(other.GetComponent<NPC>())
            {
                Debug.Log("npc");
                other.GetComponent<Animator>().SetTrigger("Hit");
            }
        }

        if (other.CompareTag("Home"))
        {
            manager.reportWin(playerIndex);
        }
    }
}
