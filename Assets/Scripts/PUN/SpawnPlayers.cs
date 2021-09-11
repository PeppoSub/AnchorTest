using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Transform spawn1;
    public Transform spawn2;

    private bool spawned1 = false;

    void Start()
    {
        Transform spawnPoint;
        GameObject playerPrefab;

        if (!spawned1) 
        { 
            spawnPoint = spawn1;
            playerPrefab = player1;
            spawned1 = true;
        }
        else 
        { 
            spawnPoint = spawn2;
            playerPrefab = player2;
        }

        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        //PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity);
    }


}
