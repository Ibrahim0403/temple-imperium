using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LongRangeScript : MonoBehaviour
{
    public GameObject projectilePrefab;

    private GameObject playerObject;
    private GameObject _projectilePrefab;

    void Start()
    {
        playerObject = GameObject.Find("Player");
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 20)) //if ray hits something within range
        {
            if (_projectilePrefab == null)
            {
                _projectilePrefab = Instantiate(projectilePrefab) as GameObject;
                transform.LookAt(playerObject.transform);
                _projectilePrefab.transform.position = transform.TransformPoint(Vector3.up * 1);
                _projectilePrefab.transform.rotation = transform.rotation;
            }
        }
    }
}
