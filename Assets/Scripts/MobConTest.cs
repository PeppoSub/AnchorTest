using System;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using Photon.Pun;

public class MobConTest : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public GameObject theCar;
    public PhotonView view;

    private CarController m_Car;

    private void Awake()
    {
        if (theCar != null) { m_Car = theCar.GetComponent<CarController>(); }
        else { m_Car = GetComponent<CarController>(); }
    }

    private void Start()
    {
        if (m_Car == null) { Debug.Log("Missing car controller !"); }
        //Debug.Log(GameObject.FindGameObjectsWithTag("GameController").ToString());

        // joystick component
        if (variableJoystick == null) { variableJoystick = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<VariableJoystick>(); }

        // photon component
        if (view == null) { view = GetComponent<PhotonView>(); }
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
