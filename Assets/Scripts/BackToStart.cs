using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToStart : MonoBehaviour
{
    public GameObject startPos;
    public float tooLow = -5f;

    static private Transform startingPosition;
    private Vector3 pos;
    static Quaternion rot;

    void Start()
    {
        if (startPos != null) { startingPosition = startPos.transform; }
        else { startingPosition = this.transform; }
        pos = new Vector3(startingPosition.position.x, startingPosition.position.y, startingPosition.position.z);
        rot = new Quaternion(startingPosition.rotation.x, startingPosition.rotation.y, startingPosition.rotation.z, startingPosition.rotation.w);

        Debug.Log("Starting Position = " + startingPosition.position.ToString());
    }

    void Update()
    {
        //Debug.Log(" Position = " + this.transform.position.ToString());

        if (this.transform.position.y < tooLow) 
        {
            Debug.Log(" Too Low = " + this.transform.position.y.ToString());
            ToStart();
        }
    }

    public void ToStart()
    {
        Debug.Log(" Back to Start = " + startingPosition.position.ToString());

        this.transform.position = pos; // startingPosition.position;
        this.transform.rotation = rot; // startingPosition.rotation;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
