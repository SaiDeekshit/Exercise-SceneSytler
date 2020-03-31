using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCast : MonoBehaviour
{

    private static CameraRayCast _instance;
    public static CameraRayCast Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("CameraRayCast is empty");
            }
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
    }
    public RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
            {
                // Debug.Log("hit " + hit.transform.name);
                if (hit.transform.tag == "Ceiling")
                {
                    // Debug.Log("hit tag " + hit.transform.tag);
                }
            }
        }
    }
}
