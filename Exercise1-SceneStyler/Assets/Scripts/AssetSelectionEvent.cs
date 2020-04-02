using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetSelectionEvent 
{
   //Creating delegate for selection of category
   public delegate void OnAssetSelection(RefrenceAssetDrag asset);
   //Event for selection of category delegate
   public static OnAssetSelection onAssetSelection;

    //Raise on event
   public static void RaiseOnAssetSelected(RefrenceAssetDrag asset)
   {
       if(onAssetSelection != null)
       {
        onAssetSelection(asset);
       }
   }
}
