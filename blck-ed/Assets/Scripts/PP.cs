using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PP : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); 
    }
    
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.up,out hit,.32f)){
            if (hit.collider.transform.gameObject.tag == "Player")
            {
                player.pp += 1;
                Destroy(gameObject);
            }
        } /*
        if (Physics.Raycast(transform.position,-transform.right,out hit,.32f)){
            if (hit.collider.transform.gameObject.tag == "Player")
            {
                player.pp += 1;
                Destroy(gameObject);
            }
        }
        if (Physics.Raycast(transform.position,transform.right,out hit,.32f)){
            if (hit.collider.transform.gameObject.tag == "Player")
            {
                player.pp += 1;
                Destroy(gameObject);
            }
        }
        if (Physics.Raycast(transform.position,transform.forward,out hit,.32f)){
            if (hit.collider.transform.gameObject.tag == "Player")
            {
                player.pp += 1;
                Destroy(gameObject);
            }
        }
        if (Physics.Raycast(transform.position,-transform.forward,out hit,.32f)){
            if (hit.collider.transform.gameObject.tag == "Player")
            {
                player.pp += 1;
                Destroy(gameObject);
            }
        } */
    }
}
