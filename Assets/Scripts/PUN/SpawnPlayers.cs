using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawn1;
    public Transform spawn2;

    private bool spawned1 = false;

    void Start()
    {
        Transform spawnPoint;
        if (!spawned1) { spawnPoint = spawn1; }
        else { spawnPoint = spawn2; }
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        //PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f,0f,0f), Quaternion.identity);
    }


}
