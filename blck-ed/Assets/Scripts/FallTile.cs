using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTile : MonoBehaviour
{
    bool firstTimeContact = false;
    public Rigidbody rb;
    public bool on = false;
    //int x = 0;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (on){
            RaycastHit hit;
            //Debug.DrawRay(transform.position,transform.up,Color.green,2);
            if (Physics.Raycast(transform.position,transform.up,out hit,.72f)){
                //print("something");
                if (hit.collider.transform.gameObject.tag == "Player")
                {
                    //print(x);
                    //x+=1;
                    firstTimeContact = true;
                }
            } else {
                if(firstTimeContact == true){
                    StartCoroutine(Fall());
                }
            }
        }
    }
    IEnumerator Fall(){
        yield return new WaitForSeconds(.22f);
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.constraints = RigidbodyConstraints.FreezePositionX;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        Destroy(gameObject,6f);
    }
}
