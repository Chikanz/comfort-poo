using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour {

    public float SpawnChance = 0.3f;

	// Use this for initialization
	void Awake ()
    {
        gameObject.SetActive(false);
        if (Random.Range(0.0f, 1.0f) < SpawnChance) gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
