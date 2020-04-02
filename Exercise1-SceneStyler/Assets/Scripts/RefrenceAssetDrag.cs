using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrenceAssetDrag : MonoBehaviour
{
    public float minimumX, maximumX;
    public float minimumY, maximumY;
    public float minimumZ, maximumZ;
    public Plane plane2D;
    public float xRotations, yRotations, zRotations;
    public bool assetRotationX, assetRotationY, assetRotationZ;

    public bool isSpin;

    void OnMouseDrag()
    {
        AssetSelectionEvent.RaiseOnAssetSelected(this);

        if (!AssetManager.Instance.isSpin.isOn)
        {
            MoveAsset();
        }
        else
        {
            SpinAsset();
        }

    }

    void MoveAsset()
    {
        if (CameraRayCast.Instance.hit.transform != null)
        {
            switch (CameraRayCast.Instance.hit.transform.tag)
            {
                case "CeilingAsset":
                    minimumX = plane2D.minimumX;
                    maximumX = plane2D.maximumX;

                    minimumZ = plane2D.minimumZ;
                    maximumZ = plane2D.maximumZ;

                    minimumY = transform.position.y;
                    maximumY = transform.position.y;

                    break;
                case "WallAsset":

                    switch (this.transform.eulerAngles.y)
                    {

                        case 0:

                            minimumY = plane2D.minimumY;
                            maximumY = plane2D.maximumY;

                            minimumZ = plane2D.minimumZ;
                            maximumZ = plane2D.maximumZ;

                            minimumX = transform.position.x;
                            maximumX = transform.position.x;


                            break;
                        case 90:
                            // Debug.Log("front");
                            minimumY = plane2D.minimumY;
                            maximumY = plane2D.maximumY;

                            minimumX = plane2D.minimumZ;
                            maximumX = plane2D.maximumZ;

                            minimumZ = transform.position.z;
                            maximumZ = transform.position.z;
                            break;
                        case 180:
                            // Debug.Log("right");
                            minimumY = plane2D.minimumY;
                            maximumY = plane2D.maximumY;

                            minimumZ = -plane2D.maximumZ;
                            maximumZ = -plane2D.minimumZ;

                            minimumX = transform.position.x;
                            maximumX = transform.position.x;

                            break;
                    }



                    break;
                case "FloorAsset":

                    minimumX = plane2D.minimumX;
                    maximumX = plane2D.maximumX;

                    minimumZ = plane2D.minimumZ;
                    maximumZ = plane2D.maximumZ;

                    minimumY = transform.position.y;
                    maximumY = transform.position.y;
                    break;

            }

        }

        Vector3 hitPosition;
        hitPosition = CameraRayCast.Instance.hit.point;
        transform.position = new Vector3(Mathf.Clamp(hitPosition.x, minimumX, maximumX), Mathf.Clamp(hitPosition.y, minimumY, maximumY), Mathf.Clamp(hitPosition.z, minimumZ, maximumZ));
    }

    void SpinAsset()
    {
        if (Input.GetMouseButton(0))
        {
            // Debug.Log("Spin Asset " + AssetManager.Instance.CurrentAsset.tag);


            switch (AssetManager.Instance.CurrentAsset.tag)
            {
                case "CeilingAsset":
                    Debug.Log("Spin Asset Ceiling");
                    yRotations = transform.rotation.eulerAngles.y;
                    yRotations -= (Input.GetAxis("Mouse X"));
                    if (Input.GetAxis("Mouse X") != 0)
                    {
                        assetRotationY = true;
                    }
                    break;
                case "WallAsset":


                    switch (this.transform.eulerAngles.y)
                    {

                        case 0:
                            xRotations = AssetManager.Instance.temporaryParent.transform.rotation.eulerAngles.x;

                            AssetManager.Instance.temporaryParent.transform.position = GetComponent<Collider>().bounds.center;
                            transform.SetParent(AssetManager.Instance.temporaryParent.transform);

                            xRotations += (Input.GetAxis("Mouse X"));

                            if (Input.GetAxis("Mouse X") != 0)
                            {
                                AssetManager.Instance.temporaryParent.transform.localEulerAngles = new Vector3( xRotations * AssetManager.Instance.mouseSensitivity,0,0);

                            }

                            break;
                        case 90:
                            // Debug.Log("front");
                            zRotations = AssetManager.Instance.temporaryParent.transform.rotation.eulerAngles.z;

                            AssetManager.Instance.temporaryParent.transform.position = GetComponent<Collider>().bounds.center;
                            transform.SetParent(AssetManager.Instance.temporaryParent.transform);

                            zRotations += (Input.GetAxis("Mouse X"));

                            if (Input.GetAxis("Mouse X") != 0)
                            {
                                AssetManager.Instance.temporaryParent.transform.localEulerAngles = new Vector3(0, 0, zRotations * AssetManager.Instance.mouseSensitivity);

                            }
                            break;
                        case 180:
                            // Debug.Log("right");
                            xRotations = AssetManager.Instance.temporaryParent.transform.rotation.eulerAngles.x;

                            AssetManager.Instance.temporaryParent.transform.position = GetComponent<Collider>().bounds.center;
                            transform.SetParent(AssetManager.Instance.temporaryParent.transform);

                            xRotations += (Input.GetAxis("Mouse X"));

                            if (Input.GetAxis("Mouse X") != 0)
                            {
                                AssetManager.Instance.temporaryParent.transform.localEulerAngles = new Vector3( xRotations * AssetManager.Instance.mouseSensitivity,0,0);

                            }

                            break;
                    }



                    break;
                case "FloorAsset":
                    yRotations = transform.rotation.eulerAngles.y;
                    yRotations -= (Input.GetAxis("Mouse X"));
                    if (Input.GetAxis("Mouse X") != 0)
                    {
                        assetRotationY = true;
                    }
                    break;

            }


        }

    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            assetRotationY = false;
            assetRotationZ = false;
            transform.SetParent(null);
        }
    }
    void LateUpdate()
    {
        if (assetRotationY)
        {
            transform.localEulerAngles = new Vector3(0, yRotations * AssetManager.Instance.mouseSensitivity, 0);
        }
        
    }

}



