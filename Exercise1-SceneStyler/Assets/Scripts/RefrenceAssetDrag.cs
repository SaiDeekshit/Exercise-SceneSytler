using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrenceAssetDrag : MonoBehaviour
{
    public float minimumX, maximumX;
    public float minimumY, maximumY;
    public float minimumZ, maximumZ;
    public Plane plane2D;

    void OnMouseDrag()
    {
  

        AssetSelectionEvent.RaiseOnAssetSelected(this);
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

   

}
