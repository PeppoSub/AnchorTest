using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;

    public void CreateRoom()
    {
        PlayerPrefs.SetInt("player", 1);        // room creator = player 1
        Debug.Log("Create Room --> Player " + PlayerPrefs.GetInt("player").ToString());
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        PlayerPrefs.SetInt("player", 2);        // room joiner = player 2
        Debug.Log("Join Room --> Player " + PlayerPrefs.GetInt("player").ToString());
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("punLevel");
    }

}
