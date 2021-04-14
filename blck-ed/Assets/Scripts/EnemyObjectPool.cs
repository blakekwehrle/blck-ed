using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject pooledObject;
    public int pooledAmount = 20;
    public bool willGrow = true;
    public List<GameObject> pooledObjects1;
    public Material Yellow;
    public Material Orange;
    public Material WireFrame;
    void Awake ()
    {
        pooledObjects1 = new List<GameObject>();    
    }
    public void AddEnemyToPool1(GameObject obj){
        pooledObjects1.Add(obj);
    }
    public void MakeEnemiesWireFrame(){
        foreach(GameObject g in pooledObjects1){
            if (g.transform.childCount > 0){
                GameObject ChildGameObject1 = g.transform.GetChild(0).gameObject;
                MeshRenderer m = ChildGameObject1.GetComponent<MeshRenderer>();
                m.material = WireFrame;
                ChildGameObject1.layer = 2;
            }
        }
    }
    public void MakeEnemiesNormal(){
        foreach(GameObject g in pooledObjects1){
            if (g.transform.childCount > 0){
                GameObject ChildGameObject1 = g.transform.GetChild(0).gameObject;
                if (g.tag == "enemy"){
                    MeshRenderer m = ChildGameObject1.GetComponent<MeshRenderer>();
                    m.material = Yellow;
                    ChildGameObject1.layer = 8;
                }
                else if (g.tag == "enemy2"){
                    MeshRenderer m = ChildGameObject1.GetComponent<MeshRenderer>();
                    m.material = Orange;
                    ChildGameObject1.layer = 8;
                }
                
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
