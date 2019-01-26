using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMan : MonoBehaviour
{
    public GameObject[] Lanes;
    private player[] players;
    private int[] playerLanes;

    public float LampTime;

    private int[,] placementMask =
    {
        {1,0,0,0,0}, //Lamppost (not used)
        {0,1,0,0,0}, //Chair
        {0,1,0,0,0}, //Mailbox
        {1,1,1,1,1}, //test
        {0,1,0,0,0}, //Trash can
        {0,0,1,1,1}, //Doggie
    };

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
        InvokeRepeating(nameof(SpawnLampPost), 0, LampTime);
    }

    void SpawnLampPost()
    {
        SpawnObsticleInLane(Obsticles[0], 0);
    }

    void SpawnObsticle()
    {
        bool b = false;
        int laneIndex = 0;
        int obsIndex = Random.Range(1, Obsticles.Length);
        while (!b)
        {
            laneIndex = Random.Range(0, Lanes.Length);
            b = placementMask[obsIndex, laneIndex] == 1;
        }
        
        GameObject g = Obsticles[obsIndex];
        SpawnObsticleInLane(g, laneIndex);
        Invoke(nameof(SpawnObsticle), Random.Range(ObsticleTimer.x, ObsticleTimer.y));
    }

    void SpawnObsticleInLane(GameObject g,int laneIndex)
    {
        //ohhhhhh yeah
        var instance = Instantiate(g, Lanes[laneIndex].transform.position +
            Vector3.left * players[0].GetComponentInChildren<BoxCollider>().bounds.extents.magnitude / 2, g.transform.rotation);
        var p = instance.transform.position;
        instance.transform.position = new Vector3(p.x, p.y, spawnZ);
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
