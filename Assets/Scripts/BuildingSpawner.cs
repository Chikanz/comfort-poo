using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    public GameObject[] Buildings;
    public float[] DelayTime;
    public Transform Mover;

    int nextIndex;
    int lastIndex;

	// Use this for initialization
	void Start ()
    {
        nextIndex = Random.Range(0, Buildings.Length);
        lastIndex = Random.Range(0, Buildings.Length);
        SpawnBuilding();

    }

    void SpawnBuilding()
    {
        nextIndex = Random.Range(0, Buildings.Length);
        var g = Instantiate(Buildings[lastIndex], transform.position, Buildings[lastIndex].transform.rotation);
        g.transform.SetParent(Mover);

        Invoke(nameof(SpawnBuilding), DelayTime[nextIndex]);
        lastIndex = nextIndex;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
