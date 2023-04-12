using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAspectRatio : MonoBehaviour
{
    Camera cam;
    [SerializeField] private bool splitLeft = false;
    [SerializeField] private bool splitRight = false;
     void Start() {
         cam = GetComponent<Camera>();
     }
     void Update()
     {
         float targetaspect = 16.0f / 9.0f;
 
         float windowaspect = (float)Screen.width / (float)Screen.height;
 
         float scaleheight = windowaspect / targetaspect;
 
         if (scaleheight < 1.0f)
         {
             Rect rect = cam.rect;

             rect.width = (splitLeft || splitRight) ? 0.5f : 1.0f;
             rect.height = scaleheight;
             rect.x = splitRight ? 0.5f : 0;
             rect.y = (1.0f - scaleheight) / 2.0f;
 
             cam.rect = rect;
         }
         else
         {
             float scalewidth = 1.0f / scaleheight;
 
             Rect rect = cam.rect;
 
             rect.width = scalewidth;
             rect.height = 1.0f;
             rect.x = (1.0f - scalewidth) / 2.0f;
             rect.y = 0;
 
             cam.rect = rect;
         }
     }
}
