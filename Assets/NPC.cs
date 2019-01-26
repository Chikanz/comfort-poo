using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool move = true;

    public float walkSpeed = 0.001f;

	
	void Start ()
    {
        if (move)
        {
            if (Random.Range(0, 2) == 1) transform.Rotate(0, 180, 0); //turn 360 degrees and walk away amirite
            var a = GetComponent<Animator>();
            if(a) a.SetBool("Walking", true);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.forward * walkSpeed;
	}

}
