using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{

    bool alive = true;
    bool waiting = false;
    bool charging = false;
    bool alarmed = false;
    public Transform cubeTransform;
    //public Rigidbody PlayerRb;
    public Animator animator;
    int x = 0;
    public float moveIntervalMin = 3;
    public float moveIntervalMax = 4;
    float moveInterval;
    float checkInterval = .2f;
    float t = 0;
    float t2 = 0;
    public float rollSpeed = 1;
    public float chargeDistance = 5f;
    int layerMask = 1 << 11;
    string chargeDirection;
    float deathCheckInterval = .18f;
    void Awake(){
        layerMask = ~layerMask;
        moveInterval = Random.Range(moveIntervalMin,moveIntervalMax);
        t = Random.Range(0,moveInterval);
    }
    //fix stuff with the bools to make sure only rays are being draw ever .2
    void Update()
    {
        //print(charging);
        //print(t);
        if (cubeTransform == null){
            alive = false;
        }
        
        if (alive){
            /* 
            t2 +=Time.deltaTime;
            if (t2 > deathCheckInterval){
                DeathCheck();
                t2 = 0;
            }*/
            if (!charging){
                t += Time.deltaTime;
                if (t > checkInterval){
                    t = 0;
                    Peek();
                    
                }
            } else if (!waiting&&!alarmed){
                Charge(chargeDirection);
            }
        }
        
    }
    /*
    void DeathCheck(){
        RaycastHit hit;
        if (Physics.Raycast(cubeTransform.position,-transform.up,out hit,.92f)){
            if (hit.collider.transform.gameObject.tag == "trapTile"){
                StartCoroutine(Death());
            }  
        } else {
            //fall death
            StartCoroutine(Death());
        }
    }
    IEnumerator Death(){
        alive = false;
        animator.SetTrigger("die");
        
        yield return new WaitForSeconds(0f);
        Destroy(gameObject, .6f);

    } */
    
    void Peek(){
        //Debug.DrawRay(cubeTransform.position,new Vector3 (transform.forward.x,transform.forward.y,transform.forward.z),Color.green,.02f);
        //Debug.DrawRay(cubeTransform.position,new Vector3 (-transform.forward.x,-transform.forward.y,-transform.forward.z),Color.green,.02f);
        //Debug.DrawRay(cubeTransform.position,new Vector3 (transform.right.x,transform.right.y,transform.right.z),Color.green,.02f);
        //Debug.DrawRay(cubeTransform.position,new Vector3 (-transform.right.x,-transform.right.y,-transform.right.z),Color.green,.02f);
        
        RaycastHit hit;
        if (Physics.Raycast(cubeTransform.position,new Vector3 (transform.forward.x,transform.forward.y,transform.forward.z),out hit,chargeDistance,layerMask)){
            //StartCoroutine(DeathBackward());
            if (hit.collider.transform.gameObject.tag == "Player"){
                //waiting = true;
                //print("PLAYER!");
                charging = true;
                chargeDirection = "up";
                StartCoroutine(AlarmedAnimation());
                
            }
        }
        if (Physics.Raycast(cubeTransform.position,new Vector3 (-transform.forward.x,-transform.forward.y,-transform.forward.z),out hit,chargeDistance,layerMask)){
            //StartCoroutine(DeathBackward());
            if (hit.collider.transform.gameObject.tag == "Player"){
                //waiting = true;
                //print("PLAYER!");
                charging = true;
                chargeDirection = "down";
                StartCoroutine(AlarmedAnimation());
            }
        }
        if (Physics.Raycast(cubeTransform.position,new Vector3 (transform.right.x,transform.right.y,transform.right.z),out hit,chargeDistance,layerMask)){
            //StartCoroutine(DeathBackward());
            if (hit.collider.transform.gameObject.tag == "Player"){
                //waiting = true;
                //print("PLAYER!");
                charging = true;
                chargeDirection = "right";
                StartCoroutine(AlarmedAnimation());
            }
        }
        if (Physics.Raycast(cubeTransform.position,new Vector3 (-transform.right.x,-transform.right.y,-transform.right.z), out hit, chargeDistance,layerMask)){
            if (hit.collider.transform.gameObject.tag == "Player"){
                //waiting = true;
                //print("PLAYER!");
                charging = true;
                chargeDirection = "left";
                StartCoroutine(AlarmedAnimation());
            }
        }
    }
    void Charge(string direction){

        //charging = true;
        //StartCoroutine(AlarmedAnimation());
        //&& alarmed == false
        
        //move up down right left randomly
        RaycastHit hit;

        if (direction == "up"){
            //Debug.DrawRay(cubeTransform.position,new Vector3 (transform.forward.x,transform.forward.y-1,transform.forward.z),Color.green,1f);
            //if (cubeTransform != null){
                if (Physics.Raycast(cubeTransform.position,new Vector3 (transform.forward.x,transform.forward.y-1,transform.forward.z),out hit,1.2f,layerMask)){
                    //StartCoroutine(DeathBackward());
                    if (hit.collider.transform.gameObject.tag == "blankTile"|| hit.collider.transform.gameObject.tag == "fallTile"|| hit.collider.transform.gameObject.tag == "trapTile"){
                        
                        StartCoroutine(MoveAnimating("up"));
                    }
                } else {
                        charging = false;
                }
            //}
            //raycast to see another block is moveable, if it is waiting = false!
        } else if (direction == "down"){
            //Debug.DrawRay(cubeTransform.position,new Vector3 (-transform.forward.x,-transform.forward.y-1,-transform.forward.z),Color.green,1f);
            //if (cubeTransform != null){
                if (Physics.Raycast(cubeTransform.position,new Vector3 (-transform.forward.x,-transform.forward.y-1,-transform.forward.z),out hit,1.2f,layerMask)){
                    //StartCoroutine(DeathBackward());
                    if (hit.collider.transform.gameObject.tag == "blankTile"|| hit.collider.transform.gameObject.tag == "fallTile"|| hit.collider.transform.gameObject.tag == "trapTile"){
                        
                        StartCoroutine(MoveAnimating("down"));
                    }
                } else {
                        charging = false;
                }
            //}
        } else if (direction == "right"){
            //Debug.DrawRay(cubeTransform.position,new Vector3 (transform.right.x,transform.right.y-1,transform.right.z),Color.green,1f);
            //if (cubeTransform != null){
                if (Physics.Raycast(cubeTransform.position,new Vector3 (transform.right.x,transform.right.y-1,transform.right.z),out hit,1.2f,layerMask)){
                    //StartCoroutine(DeathBackward());
                    if (hit.collider.transform.gameObject.tag == "blankTile"|| hit.collider.transform.gameObject.tag == "fallTile"|| hit.collider.transform.gameObject.tag == "trapTile"){
                        
                        StartCoroutine(MoveAnimating("right"));
                    }
                } else {
                    charging = false;
                }
            //}
        } else if (direction == "left") {
            //Debug.DrawRay(cubeTransform.position,new Vector3 (-transform.right.x,-transform.right.y-1,-transform.right.z),Color.green,1f);
            //if (cubeTransform != null){
                if (Physics.Raycast(cubeTransform.position,new Vector3 (-transform.right.x,-transform.right.y-1,-transform.right.z),out hit,1.2f,layerMask)){
                    //StartCoroutine(DeathBackward());
                    if (hit.collider.transform.gameObject.tag == "blankTile"|| hit.collider.transform.gameObject.tag == "fallTile"|| hit.collider.transform.gameObject.tag == "trapTile"){
                        
                        StartCoroutine(MoveAnimating("left"));
                    } 
                } else {
                    charging = false;
                }
            //}
        }

    }

    IEnumerator MoveAnimating(string direction){
        
        waiting = true;
        
        
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
        //DeathCheck();
        yield return new WaitForSeconds(.06f);
        //CheckFaces();
        waiting = false;
    }
    IEnumerator AlarmedAnimation(){
        //print("?");
        alarmed = true;
        animator.speed = 1;
        animator.SetTrigger("alarm");
        yield return new WaitForSeconds(.5f);
        alarmed = false;
    }
}
