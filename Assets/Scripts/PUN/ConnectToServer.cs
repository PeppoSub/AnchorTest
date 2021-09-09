using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public Text connectStatus;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        connectStatus.text = "Connecting ...";
    }

    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
        connectStatus.text = "Joining Lobby ...";
    }

    public override void OnJoinedLobby()
    {
        //base.OnJoinedLobby();
        //SceneManager.LoadScene("Lobby");
        connectStatus.text = "Connected to Lobby!";
    }

}
