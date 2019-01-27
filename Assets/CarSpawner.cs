using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public Vector2 increment;
    public GameObject Road;
    public Transform mover;

    public bool direction;

    // Use this for initialization
    void Start()
    {
        Invoke(nameof(SpawnRoad), Random.Range(increment.x, increment.y));
    }

    void SpawnRoad()
    {
        var g = Instantiate(Road, transform.position, Quaternion.identity);
        g.transform.SetParent(mover);
        g.GetComponent<NPC>().walkSpeed += Random.Range(-0.1f, 0.1f);

        Invoke(nameof(SpawnRoad), Random.Range(increment.x, increment.y));        

        if(direction)
        {
            g.transform.Rotate(0, 180, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
