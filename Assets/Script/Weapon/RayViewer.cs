using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour
{
    private float range;
    private new Camera camera;

    void Start()
    {
        //Assign
        camera = Camera.main;
        range = Gun.weaponRange;
    }

    void Update()
    {
        //VFX
        Vector3 lineOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Debug.DrawRay(lineOrigin, camera.transform.forward * range, Color.green);
    }
}
