using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndHouse : MonoBehaviour
{
    public AudioSource AS;
    public Transform mover;
    public float moveTime = 8;

	// Use this for initialization
	void Start ()
    {
        GetComponent<MeshRenderer>().enabled = false;
        
        Invoke(nameof(Appear), AS.clip.length - moveTime);
	}

    void Appear()
    {
        GetComponent<MeshRenderer>().enabled = true;
        transform.SetParent(mover);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
