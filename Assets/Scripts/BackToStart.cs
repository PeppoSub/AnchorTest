using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToStart : MonoBehaviour
{
    public GameObject startPos;
    public float tooLow = -5f;

    private Transform startingPosition;
    //private Vector3 startingPosition = new Vector3();
    //private Quaternion startingRotation = new Quaternion();

    void Start()
    {
        if (startPos != null) { startingPosition = startPos.transform; }
        else { startingPosition = this.transform; }
        //startingPosition = this.transform.position;
        //startingRotation = this.transform.rotation;
    }

    void Update()
    {
        if (this.transform.position.y < tooLow) 
        {
            ToStart();
         }
    }

    public void ToStart()
    {
        this.transform.position = startingPosition.position;
        this.transform.rotation = startingPosition.rotation;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }
}
