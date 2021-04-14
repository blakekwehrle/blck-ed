using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer _instance;
    public static MusicPlayer Instance
    {
        get {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MusicPlayer>();
                
                if (_instance == null)
                {
                    GameObject container = new GameObject("MusicPlayer");
                    _instance = container.AddComponent<MusicPlayer>();
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
}
