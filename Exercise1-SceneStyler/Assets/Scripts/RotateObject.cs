
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
    //For radial distance of camera and object
   
    //For loading mouse x and mouse y values
    public float currentX = 0.0f;
    public float currentY = 0.0f;

    //    public static Vector3 dir;
    public Vector3 direction;
    public Quaternion rotation;
    public Vector3 OldPos;
    public Vector3 intialCameraPosition;
    public Vector3 intialProductPosition;
    float scrollValue;
    float currentFOV;
    public float minFOV;
    public float maxFOV;
    public float scrollSensitivity;

    public bool cameraRotationX, cameraRotationY;


    public GameObject product;
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
            newCameraPosition = (product.transform.position) + rotation * direction;
            cameraObject.position = new Vector3(newCameraPosition.x, newCameraPosition.y, newCameraPosition.z);
            cameraObject.LookAt(product.transform.position);
        }
    }

    //  Zoom in and out scroll wheel
      void ZoomInOut()
        {
            currentFOV = cameraObject.gameObject.GetComponent<Camera>().fieldOfView;
            currentFOV = Mathf.Clamp(currentFOV,minFOV,maxFOV);
            scrollValue = Input.GetAxis("Mouse ScrollWheel");

            if(scrollValue > 0 )
            {
                currentFOV += (scrollValue * scrollSensitivity);
                cameraObject.gameObject.GetComponent<Camera>().fieldOfView = currentFOV;
            }
            if( scrollValue < 0)
            {
               currentFOV += (scrollValue * scrollSensitivity);
                cameraObject.gameObject.GetComponent<Camera>().fieldOfView = currentFOV;
            }
        }

}

//     //Fit to the screen
//     public void FitToScreen()
//     {
//          distance  = Vector3.Distance(ProductHandlerManager.Instance.product.transform.position + ProductHandlerManager.Instance.product.GetComponent<BoxCollider>().center,cameraObject.position);
//         direction = new Vector3(0,0,-distance);
//         Vector3 boundScale = ProductHandlerManager.Instance.product.GetComponent<BoxCollider>().size;


//         bounds = new Bounds(Recentre(ProductHandlerManager.Instance.product.transform.position,ProductHandlerManager.Instance.product.GetComponent<BoxCollider>().center),boundScale * scaleOfFitObject);
//         cameraObject.transform.position = new Vector3(-bounds.max.x,bounds.max.y,-bounds.max.z);
//         cameraObject.LookAt(ProductHandlerManager.Instance.product.transform.position + ProductHandlerManager.Instance.product.GetComponent<BoxCollider>().center);

//         distance  = Vector3.Distance(ProductHandlerManager.Instance.product.transform.position + ProductHandlerManager.Instance.product.GetComponent<BoxCollider>().center,cameraObject.position);
//         direction = new Vector3(0,0,-distance);
//         ManageOrbitRoam.Instance.cameraIntialPosition = new Vector3(cameraObject.transform.position.x,cameraObject.transform.position.y,cameraObject.transform.position.z);
//         ManageOrbitRoam.Instance.cameraIntialEulerAngles = new Vector3(cameraObject.transform.eulerAngles.x,cameraObject.transform.eulerAngles.y,cameraObject.transform.eulerAngles.z);



//     }

//     Vector3 Recentre(Vector3 _centreOfProduct,Vector3 _centreOfGravity)
//     {
//         return new Vector3((_centreOfProduct.x + _centreOfGravity.x),(_centreOfProduct.y + _centreOfGravity.y),(_centreOfProduct.z + _centreOfGravity.z));
//     }
//     //Zoom in and out scroll wheel
//       void ZoomInOut()
//         {
//             currentFOV = cameraObject.gameObject.GetComponent<Camera>().fieldOfView;
//             currentFOV = Mathf.Clamp(currentFOV,minFOV,maxFOV);
//             scrollValue = Input.GetAxis("Mouse ScrollWheel");

//             if(scrollValue > 0 )
//             {
//                 currentFOV += (scrollValue * scrollSensitivity);
//                 cameraObject.gameObject.GetComponent<Camera>().fieldOfView = currentFOV;
//             }
//             if( scrollValue < 0)
//             {
//                currentFOV += (scrollValue * scrollSensitivity);
//                 cameraObject.gameObject.GetComponent<Camera>().fieldOfView = currentFOV;
//             }
//         }