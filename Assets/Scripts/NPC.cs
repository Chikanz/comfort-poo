using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool move = true;

    public float walkSpeed = 0.001f;
    private static readonly int WALKING = Animator.StringToHash("Walking");
    private static readonly int OFFSET = Animator.StringToHash("Offset");


    void Start ()
    {
        if (move)
        {
            if (Random.Range(0, 2) == 1) transform.Rotate(0, 180, 0); //turn 360 degrees and walk away amirite
            var a = GetComponent<Animator>();
            if (a)
            {
	            a.SetBool(WALKING, true);
	            a.SetFloat(OFFSET, Random.Range(0.0f, 1.0f));
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.forward * walkSpeed;
	}

}
