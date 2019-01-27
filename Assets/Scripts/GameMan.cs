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

    bool? inFront; //true is player 1 in front

    private int[,] placementMask =
    {
        {1,0,0,0,0}, //Lamppost (not used)
        {0,1,0,0,0}, //Chair
        {0,1,0,0,0}, //Mailbox        
        {0,1,0,0,0}, //Trash can
        {0,0,1,1,1}, //Doggie
        {0,0,1,1,1}, //NPC
        {0,1,0,0,0}, //Fire hydrant
        {0,0,1,1,1}, //NPC too
        {0,1,0,0,0}, //Phone booth
    };

    public float spawnZ = 34;

    public Transform mover;

    [Header("Env")]
    public GameObject[] Obsticles;
    public Vector2 ObsticleTimer;
    public float[] spawnWeight;

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
        int laneIndex = 0;
        int obsIndex = 0;

        //weight
        while(true)
        {
            obsIndex = Random.Range(1, Obsticles.Length);
            if (Random.Range(0.0f, 1.0f) < spawnWeight[obsIndex]) break;
        }
        
        //Make sure we're spawning in the right lane
        while (true)
        {
            laneIndex = Random.Range(0, Lanes.Length);
            if (placementMask[obsIndex, laneIndex] == 1) break;
        }
        
        GameObject g = Obsticles[obsIndex];
        SpawnObsticleInLane(g, laneIndex);
        Invoke(nameof(SpawnObsticle), Random.Range(ObsticleTimer.x, ObsticleTimer.y));
    }

    void SpawnObsticleInLane(GameObject g, int laneIndex)
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

    /// <summary>
    /// Put the player passed as param behind
    /// </summary>
    /// <param name="player"></param>
    public void Hit(bool player)
    {
        inFront = !player;
        //Debug.Log(inFront.Value ? "player 1" : "player 2");
    }

    public Vector3 GetLanePosition(int lane, int player)
    {
        Vector3 forwardVec = Vector3.zero;
        if (inFront != null)
        {
             forwardVec = inFront.Value && player == 0 || !inFront.Value && player == 1 ? Vector3.forward * 1f : Vector3.zero;           
        }

        playerLanes[player] = lane; //Update lane in manager        

        if (player == 0)
        {           
            return Lanes[lane].transform.position - 
                (Vector3.right * players[player == 0 ? 1 : 0].GetComponentInChildren<BoxCollider>().bounds.extents.magnitude) + forwardVec;
        }
        else
        {            
            return Lanes[lane].transform.position + forwardVec; //normal pos
        }
    }
}
