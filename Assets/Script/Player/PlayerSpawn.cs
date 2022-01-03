using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    /// <summary>
    /// This Script is to spawn the player
    /// </summary>

    //Player Prefab
    public GameObject playerPrefab;

    //Spawn Point
    private GameObject spawnPoint;

    void Start()
    {
        //Assin
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

        //Spawn
        Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);

    }
}
