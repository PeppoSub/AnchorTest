using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    public float CameraDistance = 10f;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = player.transform.position + offset;

        Vector3 difference = target.transform.localPosition - player.transform.localPosition;
        Vector3 direction = difference.normalized;
        float angle = GetAngle(direction);

        Vector3 dirxz = new Vector3(direction.x, 0, direction.z);
        dirxz *= CameraDistance;
        offset = player.transform.position - dirxz;
        offset.y = 8f;
        transform.position = offset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);


        //// try this;  https://stackoverflow.com/questions/53505980/move-and-rotate-camera-towards-target-game-object
        //Quaternion targetRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,30);
    }

    private static float GetAngle(Vector3 direction)
    {
        return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    }
}
