using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player = null;


    void Start()
    {
        Debug.Assert(player != null);
    }

    void LateUpdate()
    {
        Vector3 camera_position = transform.position;
        camera_position.x = player.transform.position.x;
        transform.position = camera_position;
    }
}
