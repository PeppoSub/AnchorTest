using System;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using Photon.Pun;

public class MobConTest : MonoBehaviour
{
    private CarController m_Car; 
    public VariableJoystick variableJoystick;
    public GameObject theCar;

    PhotonView view;

    private void Awake()
    {
        if (theCar != null) { m_Car = theCar.GetComponent<CarController>(); }
        else { m_Car = GetComponent<CarController>(); }
    }

    private void Start()
    {
        //if (m_Car == null) { m_Car = GetComponent<CarController>(); }
        //Debug.Log(GameObject.FindGameObjectsWithTag("GameController").ToString());

        if (variableJoystick == null) { variableJoystick = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<VariableJoystick>(); }

        // photon component
        view = GetComponent<PhotonView>();
    }


    //private void FixedUpdate()
    private void Update()
    {
        if (view.IsMine)
        {
            float h = variableJoystick.Horizontal;
            float v = variableJoystick.Vertical;
            m_Car.Move(h, v, v, 0f);
        }
    }
}
