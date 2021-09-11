using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PunCamera : MonoBehaviour
{
    public GameObject parent;
    private PhotonView view;

    void Start()
    {
        view = parent.GetComponent<PhotonView>();

        if (!view.IsMine)    // !IsLocalPlayer
        {
            this.gameObject.SetActive(false);
            this.GetComponent<Camera>().enabled = false;
        }
        else
        {
            this.gameObject.SetActive(true);
            this.GetComponent<Camera>().enabled = true;
            //this.GetComponent<Camera>().tag = "MainCamera";
        }
    }

}
