using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameMan : MonoBehaviour
{
    public GameObject[] Lanes;
    private player[] players;
    private int[] playerLanes; //Length of players, shows which players are in which lane

    public float LampTime;

    public Vector2 TimeDecrease;
    public Vector2 minSpawnTime;

    bool? inFront; //true is player 1 in front

    private int[,] placementMask =
    {
        {1,0,0,0,0}, //Lamppost (not used)
        {0,1,0,0,0}, //Chair
        {0,1,0,0,0}, //Mailbox        
        {0,1,0,0,0}, //Trash can
        {0,0,1,1,1}, //Doggie
        {0,0,1,1,1}, //NPC
        {1,0,0,0,0}, //Fire hydrant
        {0,0,1,1,1}, //NPC too
        {0,1,0,0,0}, //Phone booth
    };

    private int[] PlacementCounter;

    public float spawnZ = 34;

    public Transform mover;

    public float timeToIncrease = 20;

    [Header("Env")]
    public GameObject[] Obsticles;
    public Vector2 ObsticleTimer;
    public float[] spawnWeight;
    
    private int increaseIndex;

    public GameObject winCanvas;
    private bool? won;

    bool done;

    // Use this for initialization
    void Start ()
    {
        players = GetComponentsInChildren<player>();
        playerLanes = new int[players.Length];
        PlacementCounter = new int[Lanes.Length];

        Invoke(nameof(SpawnObsticle), Random.Range(ObsticleTimer.x, ObsticleTimer.y));
        InvokeRepeating(nameof(SpawnLampPost), 0, LampTime);

        InvokeRepeating(nameof(IncreaseIntensity), timeToIncrease, timeToIncrease);        
    }

    void normalTimeScale()
    {
        Time.timeScale = 1;
    }

    void SpawnLampPost()
    {
        SpawnObsticleInLane(Obsticles[0], 0);
    }

    void SpawnObsticle()
    {
        int laneIndex = 0;
        int obsIndex = 0;

        //spawn weight
        while(true)
        {
            obsIndex = Random.Range(1, Obsticles.Length);
            if (Random.Range(0.0f, 1.0f) < spawnWeight[obsIndex]) break;
        }
        
        //Spawn in the lane with the lowest count for uniform distribution
        //mask placement counter with placementMask
        var masked = new int[Lanes.Length];
        for (int i = 0; i < Lanes.Length; i++)
        {
            var mask = placementMask[obsIndex, i];
            masked[i] = mask == 0 ? 999 : PlacementCounter[i];
        }
        
        //Get lowest index of new list
        int lowestIndex = -1;
        int lowestVal = 999;
        for (int i = 0; i < masked.Length; i++)
        {
            if (masked[i] < lowestVal)
            {
                lowestVal = masked[i];
                lowestIndex = i;
            }
        }
        
        //Place at lowest count index and increment
        laneIndex = lowestIndex;        
        PlacementCounter[laneIndex]++;
        
        //Then spawn that bad boye
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
       if(done && Input.anyKey)
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
	}

    void IncreaseIntensity()
    {
        increaseIndex++;
        ObsticleTimer -= new Vector2(0.1f, 0.2f);
        if (ObsticleTimer.x < minSpawnTime.x) ObsticleTimer.x = minSpawnTime.x;
        if (ObsticleTimer.y < minSpawnTime.y) ObsticleTimer.y = minSpawnTime.y;
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

    public void reportWin(int index)
    {
        won = inFront;
        winCanvas.SetActive(true);
        Invoke(nameof(Reveal), 3);

        foreach (var t in players)
        {
            t.gameObject.SetActive(false);
        }
    }

    public void Reveal()
    {
        if (won != null)
        {
            var s = won.Value ? "Black" : "White";
            winCanvas.GetComponentInChildren<Text>().text = $"{s} hat made it!";
        }
        else
        {
            winCanvas.GetComponentInChildren<Text>().text = $"You both shit your pants";
        }

        winCanvas.GetComponentInChildren<AudioSource>().Play();
        done = true;
    }
}
