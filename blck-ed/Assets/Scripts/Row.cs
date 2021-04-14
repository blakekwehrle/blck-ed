using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject blankTile;
    public GameObject emptyTile;
    public GameObject fallTile;
    EnemyObjectPool enemyObjectPool;
    public GameObject enemy;
    public GameObject enemy2;
    public GameObject ppObj;
    //int enemyChance = 30;
    int rowLength = 5;
    //1 in every fallTileChance tiles are falltile
    int fallTileChance = 18;
    bool spawned = false;
    GenerateLevel generateLevel;
    //public int fallTilecount = 0;
    void Awake()
    {
        generateLevel = GameObject.FindGameObjectWithTag("levelGen").GetComponent<GenerateLevel>();
        enemyObjectPool = GameObject.FindGameObjectWithTag("enemyPool").GetComponent<EnemyObjectPool>();
    }
    public void MakeBlankRow(int chance){
        int whichColumn = Random.Range(-4,rowLength-1);
        spawned = false;
        for (int i = -4; i < rowLength;i++){
            PlaceTile(new Vector3(transform.localPosition.x+i,transform.localPosition.y,transform.localPosition.z));
                    
            int tr = Random.Range(0,chance);
            int pp = Random.Range(0,chance);
            if (tr==1){
            //if (tr==1&&spawned == false && whichColumn == i){
                SpawnEnemy(new Vector3(transform.localPosition.x+i,transform.localPosition.y,transform.localPosition.z));
                
                
                //spawned = true;
            }
            if (pp==1&&spawned == false && whichColumn == i){
                GameObject p = Instantiate(ppObj,new Vector3(transform.localPosition.x+i,transform.localPosition.y+1,transform.localPosition.z),Quaternion.identity);
                spawned = true;
            }
        }
    }


    public void MakeMiddleBridge(int chance){
        int whichColumn = Random.Range(-2,3);
        spawned = false;
        for (int i = -2; i < 3;i++){
            PlaceTile(new Vector3(transform.localPosition.x+i,transform.localPosition.y,transform.localPosition.z));
                    
            int tr = Random.Range(0,chance);
            int pp = Random.Range(0,chance);
            if (tr==1){
            //if (tr==1&&spawned == false && whichColumn == i){
                SpawnEnemy(new Vector3(transform.localPosition.x+i,transform.localPosition.y,transform.localPosition.z));
                
            }
            if (pp==1&&spawned == false && whichColumn == i){
                GameObject p = Instantiate(ppObj,new Vector3(transform.localPosition.x+i,transform.localPosition.y+1,transform.localPosition.z),Quaternion.identity);
                spawned = true;
            }
        }
    }

    public void MakeHoleRow(int chance){
        bool wayThrough = false;
        
        int whichColumn = Random.Range(-4,rowLength-1);
        //int tr = Random.Range(0,chance);
        //spawned = false;
        for (int i = -4; i < rowLength;i++){
            if (i == rowLength-1 && wayThrough == false){
                PlaceTile(new Vector3(transform.localPosition.x+i,transform.localPosition.y,transform.localPosition.z));
                    
            } else {
                int r = Random.Range(0,2);
                if (r == 0){
                    PlaceTile(new Vector3(transform.localPosition.x+i,transform.localPosition.y,transform.localPosition.z));
                    
                    wayThrough = true;
                    int tr = Random.Range(0,chance);
                    int pp = Random.Range(0,chance);
                    if (tr==1){
                    //if (tr==1&&spawned == false&& whichColumn == i){
                        SpawnEnemy(new Vector3(transform.localPosition.x+i,transform.localPosition.y,transform.localPosition.z));
                
                    }
                    if (pp==1&&spawned == false && whichColumn == i){
                        GameObject p = Instantiate(ppObj,new Vector3(transform.localPosition.x+i,transform.localPosition.y+1,transform.localPosition.z),Quaternion.identity);
                        spawned = true;
                    }
                } else {
                    PlaceEmptyTile(new Vector3(transform.localPosition.x+i,transform.localPosition.y,transform.localPosition.z));
                    
                }
            }
        }
    }
    //places a blank tile most of the time. with 1/fallTileChance out of every fallTileChance falltiles.
    //yeesh what a sentence.
    void PlaceTile(Vector3 pos){
        int r = Random.Range(0,fallTileChance);
        if (r == 0){
            GameObject g = Instantiate(fallTile,pos,Quaternion.identity);
            g.transform.parent = gameObject.transform;
            //fallTilecount+=1;
        } else {
            GameObject g = Instantiate(blankTile,pos,Quaternion.identity);
            g.transform.parent = gameObject.transform;
        }
        
    }

    void PlaceEmptyTile(Vector3 pos){
        GameObject g = Instantiate(emptyTile,pos,Quaternion.identity);
        g.transform.parent = gameObject.transform;
    }
    
    void SpawnEnemy(Vector3 pos){
        if (generateLevel.spawnEnemy2 == true){
            int r = Random.Range(0,10);
            if (r <= 3){
                GameObject gg = Instantiate(enemy2,pos,Quaternion.identity);
                enemyObjectPool.AddEnemyToPool1(gg);
            } else {
                GameObject gg = Instantiate(enemy,pos,Quaternion.identity);
                enemyObjectPool.AddEnemyToPool1(gg);
            }
        } else {
            GameObject gg = Instantiate(enemy,pos,Quaternion.identity);
            enemyObjectPool.AddEnemyToPool1(gg);
        }
    }
}
