using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public Vector2 increment;
    public GameObject Road;
    public Transform mover;    

    // Use this for initialization
    void Start()
    {
        Invoke(nameof(SpawnRoad), Random.Range(increment.x, increment.y));
    }

    void SpawnRoad()
    {
        var g = Instantiate(Road, transform.position, Quaternion.identity);
        g.transform.SetParent(mover);

        Invoke(nameof(SpawnRoad), Random.Range(increment.x, increment.y));        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
