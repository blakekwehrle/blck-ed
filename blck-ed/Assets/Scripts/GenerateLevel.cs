using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject row;
    public GameObject turret;
    public GameObject blankTile;
    public GameObject beacon;
    [Range (1,20)]
    public int enemyScaleFactor = 10;
    
    Row rowscr;
    GameObject tmpRow;
    int levelLength = 70;
    int chance = 1;
    int counter = 0;
    bool buildingBridge = false;
    bool extendingGap = false;
    int bridgeLength;
    int bridgeLengthCounter = 0;
    int gapLength;
    int gapLengthCounter = 0;
    public Player player;
    int GenerateAhead = 36;
    bool tryBeacon = false;
    int beaconFrequency = 80;
    int beaconAdder = 30;
    public bool spawnEnemy2 = false;
    
    //int fallTilecountTotal = 0;
    //int levelStart = 0;
    void Awake()
    {
        spawnEnemy2 = false;
        //generate just he main track here.
        GenerateChunk(0);
        //print(fallTilecountTotal);
        //generate the line of turrets.
    }
    void IncrementChance(){
        counter+= 1;
        if (counter > 200){
            chance = enemyScaleFactor;
            spawnEnemy2 = true;
        }
        else if (counter > 150){
            chance = 2*enemyScaleFactor;
            spawnEnemy2 = true;
        } else if (counter > 100){
            chance = 3*enemyScaleFactor+2;
            spawnEnemy2 = true;
        } else if (counter > 50){
            chance = 3*enemyScaleFactor;
        } else if (counter > 10){
            chance = 4*enemyScaleFactor;
        } 
    }
    // Update is called once per frame
    void Update()
    {
        //
        if (player.score+GenerateAhead> levelLength-1){
            //print("DO IT!");
            levelLength += GenerateAhead;
            GenerateChunk(player.score+GenerateAhead);
        }
    }
    void GenerateChunk(int levelStart){
        for (int i = levelStart; i < levelLength;i++){
            if (counter >= beaconAdder){
                beaconAdder += beaconFrequency;
                tryBeacon = true;
            }
            int bridgeChance = Random.Range(0,40);
            int gapChance = Random.Range(0,16);
            if (bridgeChance == 0 && !buildingBridge && !extendingGap && !(i < 5)){
                //print("BRIDGE");
                buildingBridge = true;
                bridgeLength = Random.Range(6,10);
            } else if (gapChance == 0 && !buildingBridge && !extendingGap && !(i < 5)){
                extendingGap = true;
                gapLength = Random.Range(4,6);
            }
            //base case
            if (!buildingBridge && !extendingGap){
                GameObject g = Instantiate(row,new Vector3(0,0,i),Quaternion.identity);
                rowscr = g.GetComponent<Row>();
                if (i % 2 == 0 || i == levelStart){
                    
                    if (tryBeacon){
                        GameObject bea = Instantiate(beacon,new Vector3(5,0,i+1),Quaternion.identity);
                        tryBeacon = false;
                    }
                    rowscr.MakeBlankRow(chance);
                    //fallTilecountTotal += rowscr.fallTilecount;
                    /*
                    int rr = Random.Range(0,2);
                    if (rr == 0){
                        rowscr.MakeBlankRow(chance);
                    } else {
                        
                        rowscr.MakeBlankRow(chance);
                        //maybe there could be an alternative?
                    }
                     */
                } else {
                    rowscr.MakeHoleRow(chance);
                }
            //building a bridge
            } else if (buildingBridge && !extendingGap) {
                GameObject g = Instantiate(row,new Vector3(0,0,i),Quaternion.identity);
                rowscr = g.GetComponent<Row>();
                if (bridgeLengthCounter == 0 || bridgeLengthCounter == bridgeLength-1){
                    rowscr.MakeBlankRow(chance);
                    //fallTilecountTotal += rowscr.fallTilecount;
                } else {
                    rowscr.MakeMiddleBridge(chance);
                }
                bridgeLengthCounter += 1;
                if (bridgeLength == bridgeLengthCounter){
                    buildingBridge = false;
                    bridgeLengthCounter = 0;
                }

            //builing a gap
            } else if (extendingGap && !buildingBridge){
                if (gapLengthCounter == 0 || gapLengthCounter == gapLength-1){
                    GameObject g = Instantiate(row,new Vector3(0,0,i),Quaternion.identity);
                    rowscr = g.GetComponent<Row>();
                    rowscr.MakeBlankRow(chance);
                    //fallTilecountTotal += rowscr.fallTilecount;
                } else {
                    if (gapLengthCounter == 1){
                        GameObject g = Instantiate(row,new Vector3(0,0,i),Quaternion.identity);
                        rowscr = g.GetComponent<Row>(); 
                        rowscr.MakeHoleRow(chance);
                        tmpRow = g;
                    } else {
                        GameObject g = Instantiate(tmpRow,new Vector3(0,0,i),Quaternion.identity);
                    }
                }
                gapLengthCounter += 1;
                if (gapLength == gapLengthCounter){
                    extendingGap = false;
                    gapLengthCounter = 0;
                }
            }
             
            IncrementChance();
        }


        for (int i = levelStart; i < levelLength ;i++){
            if (i <= 3){
                GameObject g = Instantiate(blankTile,new Vector3(-6,0,i),Quaternion.identity);
            } else {
                int r = Random.Range(0,4);
                if (r == 0){   
                    GameObject g = Instantiate(turret,new Vector3(-6,0,i),Quaternion.identity);
                } else {
                    GameObject g = Instantiate(blankTile,new Vector3(-6,0,i),Quaternion.identity);
                }
            }
            
        }

    }
}
