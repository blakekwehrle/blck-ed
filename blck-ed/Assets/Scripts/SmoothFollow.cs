using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class SmoothFollow : MonoBehaviour {
     public Transform target;
     public float followSpeed = 40f;
     public float rotationSpeed = 15f;
 
     float distance;
     Vector3 position;
     Vector3 newPos;
     Quaternion rotation;
     Quaternion newRot;
     float x;
     float y;
     float z;
     void Start() {
         //distance = transform.position.y - target.position.y;
         //position = transform.position;
         x=  transform.position.x;
         y = transform.position.y;
         z = transform.position.z;
         position = newPos = new Vector3(target.position.x+x,target.position.y+y,target.position.z+z);
         //position = new Vector3(target.position.x, target.position.y, target.position.z);
         //rotation = Quaternion.Euler(new Vector3(40f, target.rotation.eulerAngles.y-45, 0f));
     }
     
     void FixedUpdate() {
         if (target) {
             //newPos = transform.position;
             newPos = new Vector3(target.position.x+x,target.position.y+y,target.position.z+z);
             //newPos.y += distance;
             //newRot = Quaternion.Euler(new Vector3(40f, target.rotation.eulerAngles.y-45, 0f));
             position = Vector3.Lerp(position, newPos, followSpeed * Time.deltaTime);
             //rotation = Quaternion.Lerp(rotation, newRot, rotationSpeed * Time.deltaTime);
             transform.position = position;
             //transform.rotation = rotation;
         }
     }
 }