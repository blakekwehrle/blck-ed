using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public bool usingController = false;
    public int bestScore = 0;
    public static Tracker _instance;
    public static Tracker Instance
    {
        get {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Tracker>();
                
                if (_instance == null)
                {
                    GameObject container = new GameObject("Tracker");
                    _instance = container.AddComponent<Tracker>();
                }
            }
        
            return _instance;
        }
    }
    void Awake()
    {
    // if the singleton hasn't been initialized yet
    if (_instance != null && _instance != this)
    {
       Destroy(this.gameObject);
       return;//Avoid doing anything else
    }
 
    _instance = this;
    DontDestroyOnLoad( this.gameObject );
    }
    void Update()
    {
        
    }
}
