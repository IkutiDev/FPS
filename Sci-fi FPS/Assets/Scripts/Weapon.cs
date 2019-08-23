using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera FPCamera;
    [SerializeField] private float range = 100f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        Physics.Raycast(FPCamera.transform.position,FPCamera.transform.forward,out hit, range);
        if (hit.transform == null)
        {
            Debug.Log("Nothing was shot");
        }
        else
        {
            Debug.Log(hit.transform.name+" was shot");
        }
    }
}
