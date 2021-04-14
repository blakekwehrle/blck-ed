using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    int whichPower;
    public MeshRenderer mr;
    public List<Material> powerMaterialList = new List<Material>();
    int[] costs = new int[] { 2, 1, 1, 1, 1 };
    void Awake()
    {
        whichPower = Random.Range(0,3);
        mr.material = powerMaterialList[whichPower];
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); 
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.up,out hit,.32f)){
            if (hit.collider.transform.gameObject.tag == "Player")
            {
                player.powerChanger = whichPower;
                player.powerupCost = costs[whichPower];
                //player.pp += 1;
                Destroy(gameObject);
            }
        } 
    }
}
