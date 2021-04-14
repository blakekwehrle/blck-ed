using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Start is called before the first frame update
    public float shootIntervalMin = 2.3f;
    public float shootIntervalMax = 3f;
    float shootInterval;
    public GameObject bulletBall;
    float t;
    void Awake()
    {
        shootInterval = Random.Range(shootIntervalMin,shootIntervalMax);
        t = Random.Range(0,shootInterval);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > shootInterval){
            Shoot();
            t = 0;
        }
    }
    void Shoot(){
        GameObject g = Instantiate(bulletBall,new Vector3(transform.position.x,transform.position.y+1f,transform.position.z),Quaternion.identity);
        Rigidbody rb = g.GetComponent<Rigidbody>();
        rb.AddForce(transform.right * 4f,ForceMode.VelocityChange);
        Destroy(g, 6f);
    }
}
