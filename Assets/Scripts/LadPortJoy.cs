using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LadPortJoy : MonoBehaviour
{
    public VariableJoystick joystic1;
    public VariableJoystick joystic2;
    public Image background;
    public GameObject cameraCar;

    public Sprite[] axisSprites;

    void Start()
    {
        if (joystic1 == null) { joystic1 = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<VariableJoystick>(); }
        if ((joystic2 == null) && (GameObject.FindGameObjectsWithTag("GameController").Length > 1))
        {
            joystic2 = GameObject.FindGameObjectsWithTag("GameController")[1].GetComponent<VariableJoystick>();
        }
        //if(cameraCar == null) { cameraCar = GameObject.FindGameObjectsWithTag("PlayerCamera")[0]; }
    }


    void Update()
    {
        // in portraid mode use only 1 joystic
        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            joystic1.gameObject.SetActive(true);
            joystic2.gameObject.SetActive(false);
            joystic1.AxisOptions = AxisOptions.Both;
            background.sprite = axisSprites[0];
        }
        else   // in landscape use 2
        {
            joystic1.gameObject.SetActive(true);
            joystic2.gameObject.SetActive(true);
            joystic1.AxisOptions = AxisOptions.Vertical;
            background.sprite = axisSprites[1];
        }

        if (cameraCar != null) { cameraCar.transform.localScale = new Vector3(0.6f,0.6f,1f); }
    }

}
