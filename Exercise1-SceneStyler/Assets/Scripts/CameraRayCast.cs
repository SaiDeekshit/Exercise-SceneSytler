using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCast : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButton(0))
        {
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            Debug.Log("hit " + hit.transform.name);
        }
        }
    }
}
