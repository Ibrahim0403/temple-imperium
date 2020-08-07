using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraScript : MonoBehaviour
{
    //billboard for camera follow
    public Transform fpsCam;

    void Start()
    {
        fpsCam = GameObject.Find("FPS Camera").transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + fpsCam.forward); //enemy health bar looks at player
    }
}
