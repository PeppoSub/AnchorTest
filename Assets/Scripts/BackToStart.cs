using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToStart : MonoBehaviour
{
    public GameObject startPos;
    public float tooLow = -5f;

    private Transform startingPosition;

    void Start()
    {
        if (startPos != null) { startingPosition = startPos.transform; }
        else { startingPosition = this.transform; }
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
