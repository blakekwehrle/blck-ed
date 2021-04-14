using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    bool alive = true;
    bool waiting = false;
    public Transform cubeTransform;
    //public Rigidbody PlayerRb;
    public Animator animator;
    int x = 0;
    public float moveIntervalMin = 3;
    public float moveIntervalMax = 4;
    float moveInterval;
    float t = 0;
    public float rollSpeed = 1;
    int layerMask = 1 << 11;
    
    //layerMask shit was where i left oft.
    void Awake(){
        layerMask = ~layerMask;
        moveInterval = Random.Range(moveIntervalMin,moveIntervalMax);
        t = Random.Range(0,moveInterval);
    }
    void Update()
    {
        //print(t);
        if (cubeTransform == null){
            alive = false;
        }
        if (alive){
            t += Time.deltaTime;
            if (t > moveInterval){
                t = 0;
                Move();
            }
        }
        
    }
    void Move(){
        //waiting = false;
        while (!waiting){
            int r = Random.Range(0,4);
            //move up down left right randomly
            RaycastHit hit;

            if (r == 0){
                //Debug.DrawRay(cubeTransform.position,new Vector3 (transform.forward.x,transform.forward.y-1,transform.forward.z),Color.green,1f);
                //if (cubeTransform != null){
                    if (Physics.Raycast(cubeTransform.position,new Vector3 (transform.forward.x,transform.forward.y-1,transform.forward.z),out hit,1.2f,layerMask)){
                        //StartCoroutine(DeathBackward());
                        if (hit.collider.transform.gameObject.tag == "blankTile"|| hit.collider.transform.gameObject.tag == "fallTile"){
                            waiting = true;
                            StartCoroutine(MoveAnimating("up"));
                        }
                    }
                //}
                //raycast to see another block is moveable, if it is waiting = false!
            } else if (r == 1){
                //Debug.DrawRay(cubeTransform.position,new Vector3 (-transform.forward.x,-transform.forward.y-1,-transform.forward.z),Color.green,1f);
                //if (cubeTransform != null){
                    if (Physics.Raycast(cubeTransform.position,new Vector3 (-transform.forward.x,-transform.forward.y-1,-transform.forward.z),out hit,1.2f,layerMask)){
                        //StartCoroutine(DeathBackward());
                        if (hit.collider.transform.gameObject.tag == "blankTile"|| hit.collider.transform.gameObject.tag == "fallTile"){
                            waiting = true;
                            StartCoroutine(MoveAnimating("down"));
                        }
                    }
                //}
            } else if (r == 2){
                //Debug.DrawRay(cubeTransform.position,new Vector3 (transform.right.x,transform.right.y-1,transform.right.z),Color.green,1f);
                //if (cubeTransform != null){
                    if (Physics.Raycast(cubeTransform.position,new Vector3 (transform.right.x,transform.right.y-1,transform.right.z),out hit,1.2f,layerMask)){
                        //StartCoroutine(DeathBackward());
                        if (hit.collider.transform.gameObject.tag == "blankTile"|| hit.collider.transform.gameObject.tag == "fallTile"){
                            waiting = true;
                            StartCoroutine(MoveAnimating("right"));
                        }
                    }
                //}
            } else {
                //Debug.DrawRay(cubeTransform.position,new Vector3 (-transform.right.x,-transform.right.y-1,-transform.right.z),Color.green,1f);
                //if (cubeTransform != null){
                    if (Physics.Raycast(cubeTransform.position,new Vector3 (-transform.right.x,-transform.right.y-1,-transform.right.z),out hit,1.2f,layerMask)){
                        //StartCoroutine(DeathBackward());
                        if (hit.collider.transform.gameObject.tag == "blankTile"|| hit.collider.transform.gameObject.tag == "fallTile"){
                            waiting = true;
                            StartCoroutine(MoveAnimating("left"));
                        }
                    }
                //}
            }

        }
        
    }

    IEnumerator MoveAnimating(string direction){
        //waiting = true;
        animator.SetTrigger(direction);
        
        animator.speed = rollSpeed;
        yield return new WaitForSeconds(.3f/rollSpeed);
        //print("double yes");
        
        if (alive){
            if (direction == "up"){
                transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z+1);
            } else if (direction == "down"){
                transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z-1);
            } else if (direction == "left"){
                transform.position = new Vector3(transform.position.x-1,transform.position.y,transform.position.z);
            } else if (direction == "right"){
                transform.position = new Vector3(transform.position.x+1,transform.position.y,transform.position.z);
            }  
        }
        
        yield return new WaitForSeconds(.05f);
        //CheckFaces();
        waiting = false;
    }
}
