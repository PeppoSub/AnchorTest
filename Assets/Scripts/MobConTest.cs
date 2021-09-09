using System;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class MobConTest : MonoBehaviour
{
    private CarController m_Car; 
    public VariableJoystick variableJoystick;
    public GameObject theCar;

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
    }

    //private void FixedUpdate()
    private void Update()
    { 
        float h = variableJoystick.Horizontal;
        float v = variableJoystick.Vertical;
        m_Car.Move(h, v, v, 0f);
    }
}
