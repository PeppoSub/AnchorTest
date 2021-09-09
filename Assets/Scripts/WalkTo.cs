using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalkTo : MonoBehaviour
{
    public float walkingSpeed = 100f;
    public float acceleration = 10f;
    public float deceleration = 50f;
    public float meowTime = 2.5f;
    public float almostZero = 0.05f;
    public float approaching = 0.1f;
    public Text catSpeed;

    private Rigidbody rBody;
    private Animator theAnimator;
    private GameObject[] theLaserPointer;
    //private Vector3 theTarget = new Vector3();
    private float seconds;
    private float distance;
    private float currentSpeed;

    void Start()
    {
        rBody = gameObject.GetComponent<Rigidbody>(); 
        theAnimator = gameObject.GetComponent<Animator>();

        seconds = 0f;
        currentSpeed = 0f;
        theAnimator.ResetTrigger("meow");
        theAnimator.SetFloat("speed", currentSpeed);
    }

    void Update()
    {
        seconds += Time.deltaTime;

        //currentSpeed = this.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        theAnimator.SetFloat("speed", currentSpeed/10f);
        if (catSpeed != null) { catSpeed.text = "Cat Speed = " + currentSpeed.ToString("n2"); }

        theLaserPointer = GameObject.FindGameObjectsWithTag("Laser");
        if (theLaserPointer.Length > 0) { Walk(theLaserPointer[0].transform.position); }
        else
        {
            Sit();
            if (seconds >= meowTime) { Meow(); }
        }

    }

    public void Walk(Vector3 theTarget)
    {
        Vector3 relativePos = theTarget - this.transform.position;
        relativePos.y = 0f;
        distance = relativePos.magnitude;
        relativePos.Normalize();
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        this.transform.rotation = rotation;

        if (distance <= approaching) 
        {
            if (currentSpeed > almostZero) { currentSpeed = currentSpeed - deceleration * Time.deltaTime; }
            else { currentSpeed = 0f; }
            //Sit();
        }
        else 
        { 
            if (currentSpeed < walkingSpeed) { currentSpeed = currentSpeed + acceleration * Time.deltaTime; }
            else                             { currentSpeed = walkingSpeed; }
        }
        rBody.velocity = relativePos * currentSpeed * Time.deltaTime;
        //this.transform.Translate(relativePos * walkingSpeed * Time.deltaTime);

        if (distance <= almostZero) { Sit(); }

        //theAnimator.SetTrigger("walk");
        //this.transform.position = Vector3.Lerp(this.transform.position, theTarget, walkingSpeed * Time.deltaTime);
        seconds = 0f;
    }

    public void Sit()
    {
        //Vector3 direction = rBody.velocity;
        //direction.Normalize();
        //if (currentSpeed > almostZero) { currentSpeed = currentSpeed - deceleration * Time.deltaTime; }
        //else                           { currentSpeed = 0f; }
        //rBody.velocity = direction * currentSpeed * Time.deltaTime;

        // this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        theAnimator.SetTrigger("sit");
    }

    public void Meow()
    {
        theAnimator.SetTrigger("meow");
        seconds = 0f;
    }
}