using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
//using TMPro;
using UnityStandardAssets.Vehicles.Car;

public class PlaceObject : MonoBehaviour
{
    public ARRaycastManager theRaycastManager;

    [SerializeField]
    ARPlaneManager thePlaneManager;

    static List<ARRaycastHit> theHits;

    public GameObject theCamera;
    public GameObject thePlacementIndicatorPrefab;
    public GameObject[] theObjectsToPlace;
    public GameObject theCanvas;

    private Vector2 screenPosition;
    private GameObject thePlacementIndicator;
    private Pose thePlacementPose;
    private bool placementPoseIsValid = false;
    private GameObject thePlacedObject;
    private bool placedTheObject;
    private Canvas thePlayerHud;
    static private int selected;

    void Start()
    {
        selected = 0;
        placedTheObject = false;
        thePlayerHud = theCanvas.GetComponent<Canvas>();

        placementPoseIsValid = false;
        thePlacementIndicator = GameObject.Instantiate(thePlacementIndicatorPrefab, this.transform.position, this.transform.rotation);
        thePlacementIndicator.SetActive(false);
    }

    void Update()
    {
        if (!placedTheObject) { UpdateCursor(); }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(index: 0);
            if (touch.phase == TouchPhase.Began) { PlaceTheObject(); }
        }

    }

    void UpdateCursor()
    {
        theHits = new List<ARRaycastHit>();
        screenPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
        if (theRaycastManager.Raycast(screenPosition, theHits, TrackableType.PlaneWithinPolygon))
        {
            placementPoseIsValid = true;
            thePlacementPose = theHits[0].pose;

            thePlacementIndicator.transform.SetPositionAndRotation(thePlacementPose.position, thePlacementPose.rotation);
            thePlacementIndicator.SetActive(true);

        }
        else
        {
            placementPoseIsValid = false;
            if (thePlacementIndicator != null) { thePlacementIndicator.SetActive(false); }
        }

    }

    public void PlaceTheObject()
    {
        if (!placedTheObject && placementPoseIsValid)
        {
            thePlacedObject = Instantiate(theObjectsToPlace[selected], thePlacementIndicator.transform.position, thePlacementIndicator.transform.rotation);
            thePlacedObject.SetActive(true);

            GameObject theCar = FindGameObjectInChildWithTag(thePlacedObject,"Player");
            //theCar.GetComponent<MobController>().variableJoystick = FindGameObjectInChildWithTag(theCanvas, "GameController").GetComponent<VariableJoystick>() ;

            placedTheObject = true;
            InitGame();
        }
    }

    void InitGame()
    {
        Instantiate(theCanvas);
        theCanvas.SetActive(true);
        Instantiate(thePlayerHud);
        thePlayerHud.enabled = true;

        // hide all planes already detected 
        foreach (var plane in thePlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
        thePlaneManager.planePrefab = null;
        // Destroy(thePlaneManager);   // should not destroy this, as we may need it again ... 

        // destroy the placement indicator
        Destroy(thePlacementIndicator);
    }

    public void ChangeSelect()
    {
        int nObj = theObjectsToPlace.Length;
        selected = (selected + 1) % nObj;
    }

    public static GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
            {
                return t.GetChild(i).gameObject;
            }

        }

        return null;
    }
}
