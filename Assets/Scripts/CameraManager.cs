using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintDots
{


    public class CameraManager : MonoBehaviour
    {
        private Camera myCam;

        private void Awake()
        {
            myCam = GetComponent<Camera>();
            
        }

       public  void  ZoomPerspectiveCamera(float width,float height)
       {
           height = 2.0f * ((width > height ? width : height + 0.5f) / 2) * Mathf.Atan(myCam.fieldOfView);

           if (height < 5.5f)
               height = 5.5f;

           transform.position = new Vector3((width - 1) / 2f,height,myCam.transform.position.z);

       }

       public void ZoomOrthograpichSizeCamera(float width, float height)
       {
           myCam.orthographicSize = (width > height ? width : height + 0.5f) / 2 ; 
       }
    }
}