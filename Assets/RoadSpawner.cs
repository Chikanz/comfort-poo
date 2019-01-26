using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public float increment;
    public GameObject Road;
    public Transform mover;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating(nameof(SpawnRoad), 0, increment);
	}

    void SpawnRoad()
    {
        var g = Instantiate(Road, transform.position, Quaternion.identity);
        g.transform.SetParent(mover);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
