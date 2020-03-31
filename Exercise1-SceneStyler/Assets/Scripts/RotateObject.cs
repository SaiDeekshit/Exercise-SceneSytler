
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class is attached to camera for camera is rotating around object
public class RotateObject : MonoBehaviour
{
    private static RotateObject _instance;
    public static RotateObject Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Rotate Object os null");
            }
            return _instance;
        }

    }

    void Awake()
    {
        _instance = this;
    }
    //For Clamping angles 
    private const float Y_ANGLE_MIN = 0.0f;
    //For avoiding gimbal lock we are using below value instead of 90
    private const float Y_ANGLE_MAX = 89.999f;
    //To make camera to looking at object
    public Transform cameraObject;

    public float mouseSensitivity;
  

    //For loading mouse x and mouse y values
    public float currentX = 0.0f;
    public float currentY = 0.0f;

    public Vector3 direction;
    public Quaternion rotation;

    float scrollValue;
    float currentFOV;
    public float minFOV;
    public float maxFOV;
    public float scrollSensitivity;

    public bool cameraRotationX, cameraRotationY;


    public GameObject room;
    public Vector3 newCameraPosition;

    void Start()
    {
        cameraObject = transform;
    }

    void Update()
    {

        DragInUpdate();
        ZoomInOut();

    }
    private void LateUpdate()
    {

        DragInLateUpdate();
    }


    //Drag in Update
    void DragInUpdate()
    {

        if (Input.GetMouseButton(1))
        {

            currentX = cameraObject.rotation.eulerAngles.y;
            currentX += (Input.GetAxis("Mouse X") * mouseSensitivity);

            currentY = cameraObject.rotation.eulerAngles.x;

            currentY -= (Input.GetAxis("Mouse Y") * mouseSensitivity);

            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

            if (Input.GetAxis("Mouse X") != 0)
            {
                cameraRotationX = true;
            }
            else
            {
                cameraRotationX = false;
            }
            if (Input.GetAxis("Mouse Y") != 0)
            {

                cameraRotationY = true;
            }
        }
        //To disable the drag when mousebutton up
        if (Input.GetMouseButtonUp(1))
        {
            cameraRotationX = false;
            cameraRotationY = false;
            currentX = 0;
            currentY = 0;
        }
    }
    //Drag in Late Update
    void DragInLateUpdate()
    {

        if (cameraRotationX || cameraRotationY)
        {

            rotation = Quaternion.Euler(currentY, currentX, 0);
            float distance = Vector3.Distance(transform.position, room.transform.position);
            direction = new Vector3(0, 0, -distance);
            newCameraPosition = (room.transform.position) + rotation * direction;
            cameraObject.position = new Vector3(newCameraPosition.x, newCameraPosition.y, newCameraPosition.z);
            cameraObject.LookAt(room.transform.position);
        }
    }

    //  Zoom in and out scroll wheel
    void ZoomInOut()
    {
        currentFOV = cameraObject.gameObject.GetComponent<Camera>().fieldOfView;
        currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
        scrollValue = Input.GetAxis("Mouse ScrollWheel");

        if (scrollValue > 0)
        {
            currentFOV += (scrollValue * scrollSensitivity);
            cameraObject.gameObject.GetComponent<Camera>().fieldOfView = currentFOV;
        }
        if (scrollValue < 0)
        {
            currentFOV += (scrollValue * scrollSensitivity);
            cameraObject.gameObject.GetComponent<Camera>().fieldOfView = currentFOV;
        }
    }

}
