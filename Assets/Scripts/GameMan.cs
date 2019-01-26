using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMan : MonoBehaviour
{
    public GameObject[] Lanes;
    private player[] players;
    private int[] playerLanes;

    public float spawnZ = 34;

    public Transform mover;

    [Header("Env")]
    public GameObject[] Obsticles;
    public Vector2 ObsticleTimer;    

	// Use this for initialization
	void Start ()
    {
        players = GetComponentsInChildren<player>();
        playerLanes = new int[players.Length];

        Invoke(nameof(SpawnObsticle), Random.Range(ObsticleTimer.x, ObsticleTimer.y));
    }

    void SpawnObsticle()
    {
        var g = Obsticles[Random.Range(0, Obsticles.Length)];
        var instance = Instantiate(g, Lanes[Random.Range(0, Lanes.Length)].transform.position, g.transform.rotation);
        var p = instance.transform.position;
        instance.transform.position = new Vector3(p.x, p.y, spawnZ);
        Invoke(nameof(SpawnObsticle), Random.Range(ObsticleTimer.x, ObsticleTimer.y));
        instance.transform.SetParent(mover);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public Vector3 GetLanePosition(int lane, int player)
    {
        playerLanes[player] = lane; //Update lane in manager        

        if (player == 0)
        {           
            return Lanes[lane].transform.position - 
                (Vector3.right * players[player == 0 ? 1 : 0].GetComponentInChildren<BoxCollider>().bounds.extents.magnitude);
        }
        else
        {            
            return Lanes[lane].transform.position; //normal pos
        }
    }
}
