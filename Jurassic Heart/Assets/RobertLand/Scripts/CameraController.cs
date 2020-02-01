﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

 /// <summary>
 /// I need to orbit around the "thing" the user is chiefly concerned about
 /// Zoom should always be in reference to the "thing" the user is chiefly concerned about
 /// Translate is Translate
 /// </summary>
 public class CameraController : MonoBehaviour
 {
     internal GameObject focus;
     internal Vector3 shipCenter;
     internal float shipSize;



     int moveSpeedConstant = 25;
     bool draggingCamera;
     bool rotatingCamera;
     Vector3 mouseStartingPosition;




     private bool init = false;
     // Use this for initialization
     void Start()
     {
     }

     public void Init(GameObject focus)
     {
         this.focus = focus;
         if (this.focus.transform.childCount != 0)
         {
             shipCenter = this.focus.MakeBoundingBoxForObjectMeshRenderers().center;
         }
         else
         {
             shipCenter = this.focus.transform.position;
         }

         RecalculateSize();

         SetToDefaultView();
         init = true;
     }
     

     // Update is called once per frame
     void Update()
     {
        #if UNITY_EDITOR
         if (init)
         {
             TakeMouseInput();
         }

         if (Input.GetKeyDown(KeyCode.H))
         {
             SetToDefaultView();
         }
         #endif
     }

     internal void TakeMouseInput()
     {
         if (focus != null) //We have not initialized yet
         {
             if (focus.transform.childCount != 0)
             {
                 Zoom(Input.GetAxis("Mouse ScrollWheel"));
             }

             if (Input.GetMouseButton(1))
             {
                 if (!rotatingCamera)
                 {
                     rotatingCamera = true;
                     draggingCamera = false;
                     mouseStartingPosition = Input.mousePosition;
                 }

                 Vector3 diff = .3f * (Input.mousePosition - mouseStartingPosition);
                 RevolveView(diff);
                 mouseStartingPosition = Input.mousePosition;
             }
             else if (Input.GetMouseButton(2))
             {
                 if (!draggingCamera)
                 {
                     rotatingCamera = false;
                     draggingCamera = true;
                     mouseStartingPosition = Input.mousePosition;
                 }

                 //drag camera
                 Vector3 diff = .01f * (Input.mousePosition - mouseStartingPosition);
                 Translate(diff);
                 mouseStartingPosition = Input.mousePosition;
             }
             else
             {
                 draggingCamera = false;
                 rotatingCamera = false;
             }
         }
     }

     internal void Zoom(float value)
     {
         value *= shipSize / 15f;
         //move towards the first active child of shipRoot
         transform.position = Vector3.MoveTowards(transform.position, shipCenter, value);
         if (transform.position.Equals(shipCenter) && value < 0)
             transform.position += transform.forward * value;
     }

     internal void Translate(Vector2 vector2)
     {
         transform.position -= transform.right * vector2.x;
         transform.position -= transform.up * vector2.y;
     }

     internal void RevolveView(Vector2 vector2)
     {
         vector2 *= 1.2f;
         //rotate up and down
         //axis is perpendicular to the forward and up of the camera
         transform.RotateAround(shipCenter, transform.right, -vector2.y);

         //rotate left and right
         //axis is perpendicular to the forward and right of camera
         transform.RotateAround(shipCenter, transform.up, vector2.x);

     }
     
     public void SetToDefaultView()
     {

         Bounds bounds = GetGameObjectToCenterOn().MakeBoundingBoxForObjectRenderers(false);
         transform.position = bounds.center + bounds.extents.magnitude * 2 * Vector3.one;
         transform.LookAt(bounds.center);
     }

     public void SetToCloseDefaultView()
     {
         Bounds bounds = GetGameObjectToCenterOn().MakeBoundingBoxForObjectRenderers(false);
//        Vector3 extents = bounds.extents;
//        float maxDimension = Mathf.Max(extents.x, extents.y, extents.z);
//
//        float camDistance = Mathf.Sqrt(3 * maxDimension * maxDimension);


         transform.position = bounds.center + bounds.extents.magnitude * 1f * Vector3.one;
         transform.LookAt(bounds.center);
     }

     public void FitToView(GameObject target)
     {
         Bounds bounds = target.MakeBoundingBoxForObjectRenderers(false);
         //Debug.Log("Dot(cam.forward, bounds.extents): " + Vector3.Dot(transform.forward, bounds.extents));

         //transform.position = bounds.center + transform.forward * Vector3.Dot(transform.forward, bounds.extents) * bounds.extents.magnitude * 2.5f;
         transform.position = bounds.center + (-transform.forward).Multiply(bounds.extents) -
                              transform.forward * bounds.extents.magnitude * 1.5f; //* bounds.extents.magnitude * 2.5f;

         transform.LookAt(bounds.center);
     }

     public void SetOrigin(Vector3 v3)
     {
         shipCenter = v3;
         //Debug.Log("Setting origin to: " + v3);
     }

     public void RecalculateSize()
     {
         shipSize = focus.MakeBoundingBoxForObjectRenderers().size.magnitude;
     }

     private GameObject GetGameObjectToCenterOn()
     {
         return focus;
     }
 }
