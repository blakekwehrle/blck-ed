using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    bool waiting = false;
    bool alive = true;
    bool powerup = false;
    bool buildMode = false;
    int blocksBuilt = 0;
    public Transform cubeTransform;
    public GameObject cubeReplacement;

    public GameObject playerMadeTile;
    public EnemyObjectPool enemyObjectPool;
    public Rigidbody PlayerRb;
    public Animator animator;
    [Range (.5f,.95f)]
    public float sensitivity = .6f;
    float rollSpeed = 1;
    int x = 0;
    public GameObject controllerText;
    public MeshRenderer pmr;
    public Material PlayerMaterial;
    public Material InvincMaterial;
    public Material SpeedMaterial;
    public Material BuildMaterial;
    public Text totalPP;
    public Text scoreText;
    public Text powerText;
    public Text bestScoreText;
    public Text powerCostText;
    public int score = 0;
    
    public int pp = 0;
    public int powerChanger = -1;
    public int powerupCost = 1;
    Tracker tracker;
    void Start(){
        tracker = GameObject.FindGameObjectWithTag("tracker").GetComponent<Tracker>();
        //just being safe
        animator.speed = 1;
    }
    
    //yuck move this asap
    //scrapped this quick its just about controllers
    IEnumerator SwitchInput(){
        Text t = controllerText.GetComponent<Text>();
        if (tracker.usingController){
            tracker.usingController = false;
            t.text = "Input Changed to Keyboard";
        } else {
            tracker.usingController = true;
            t.text = "Input Changed to Controller";
        }
        controllerText.SetActive(true);
        yield return new WaitForSeconds(3);
        controllerText.SetActive(false);
    } 
    //inputs and the controller mess continued

    //maybe you get like a powerup (or two idc!)
    //but you also get passives? who freaking knows
    //
    //I think for the next power up I want it to be like 1 or 2 seconds where there are just blocks placed under
    //you if you were about to fall.

    //big potential break through in design
    // so powerups cost points, lets call them pp
    // for example speedup 1pp
    // invincibility 2pp
    // build 3pp
    // you start out the game with zero pp
    // you collect randomly generated pp balls (lol not intentional)
    // ^these balls are spawned similarly to the enemies.
    // powerups may be obtained at power up kiosks that appear (maybe in the range of (40-60 rows there is the first kiosk))
    // IDK I was thinking about making the kiosks completely random. but im thinking maybe they have a chance to show up,
    // and they only show up in a certain spot. I dont want duplicate kiosks.

    void Update()
    {
        UpdateUI();
        //print(pp);
        if (Input.GetButtonDown("restart")){
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown("o")){
            StartCoroutine(SwitchInput());
        }
        if (Input.GetButtonDown("usepower") && !powerup&& pp >= powerupCost&&powerChanger != -1){
            pp -=powerupCost;
            if (powerChanger == 0){
                StartCoroutine(Invincibility());
            } else if (powerChanger == 1){
                StartCoroutine(SpeedUp());
            } else if (powerChanger == 2){
            //} else if (powerChanger == 2){
                StartCoroutine(Build());
            } else {
                StartCoroutine(Build());
            }
        }
        /*
        if (Input.GetButtonDown("cycle")){
            powerChanger +=1;
            if (powerChanger >2){
                powerChanger = 0;
            }

            //THIS IS TEMPORARY

            
            
        }
         */
        
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        if (alive){
            //this is using wasd
            CheckFaces();
            if (!tracker.usingController){
                if (!waiting){
                    if (Input.GetKey(KeyCode.W)){
                        StartCoroutine(Move("up"));
                    }
                    else if (Input.GetKey(KeyCode.S)){
                        StartCoroutine(Move("down"));
                    }
                    else if (Input.GetKey(KeyCode.A)){
                        StartCoroutine(Move("left"));
                    }
                    else if (Input.GetKey(KeyCode.D)){
                        StartCoroutine(Move("right"));
                    }
                }
            } else {
                if (!waiting){
                    //print(Input.GetAxis("Vertical"));
                    if(Input.GetAxis("Vertical")>sensitivity){
                        StartCoroutine(Move("up"));
                    }
                    else if(Input.GetAxis("Vertical")<-sensitivity){
                        StartCoroutine(Move("down"));
                    }
                    else if(Input.GetAxis("Horizontal")>sensitivity){
                        StartCoroutine(Move("right"));
                    }
                    else  if(Input.GetAxis("Horizontal")<-sensitivity){
                        StartCoroutine(Move("left"));
                    }
                }
           }
        }    
    }
    //powerups     
    IEnumerator Invincibility(){
        powerup = true;
        if (pmr != null){pmr.material = InvincMaterial;}
        enemyObjectPool.MakeEnemiesWireFrame();
        yield return new WaitForSeconds(3f);
        enemyObjectPool.MakeEnemiesNormal();
        if (pmr != null){
            pmr.material = PlayerMaterial;
        }
        powerup = false;
    }
    IEnumerator SpeedUp(){
        powerup = true;
        rollSpeed = 1.6f;
        if (pmr != null){pmr.material = SpeedMaterial;}
        yield return new WaitForSeconds(3f);
        if (pmr != null){
            pmr.material = PlayerMaterial;
        }
        rollSpeed = 1;
        powerup = false;
    }
    IEnumerator Build(){
        powerup = true;
        buildMode = true;
        if (pmr != null){pmr.material = BuildMaterial;}
        yield return new WaitForSeconds(0f);
        /*
        if (pmr != null){
            pmr.material = PlayerMaterial;
        }*/
        //buildMode = false;
        //powerup = false;
    }

    void UpdateUI(){
        //calculate score
        /* */
        if (transform.position.z > score){
            score = (int) transform.position.z;
            if (score > tracker.bestScore){
                tracker.bestScore = score;
            }
        }
        //update UI
        totalPP.text = "PP: "+pp.ToString();
        scoreText.text = "Score: "+score.ToString();
        bestScoreText.text = "Best: "+tracker.bestScore.ToString();
        if (powerChanger != -1){
            powerCostText.text = ""+ powerupCost.ToString();
        }
        if (powerChanger == 0){
                
            powerText.text = "i";
        } else if (powerChanger == 1){
            powerText.text = "z";
        } else if (powerChanger == 2){
            powerText.text = "m";
        }
    }

    //current raycast setup
    //thoughts is since raycasts just kind of kill you right now, maybe to like "get a power up"
    //id do a raycast that shoots like straight up? thatd be cool right? IDK
    void CheckFaces(){
        /* */
        Vector3 transformPos = new Vector3 (transform.position.x,transform.position.y+1,transform.position.z);
        Vector3 transformupandforward = new Vector3 (transform.forward.x,transform.forward.y+1,transform.forward.z);
        
        RaycastHit hit;
        //Debug.DrawRay(cubeTransform.position,transformupandforward,Color.green,.01f);
        //Debug.DrawRay(cubeTransform.position,transform.forward,Color.green,.01f);
        
        //Debug.DrawRay(cubeTransform.position,transform.right,Color.green,.01f);
        
        //Debug.DrawRay(cubeTransform.position,-transform.right,Color.green,.01f);
        //straight down from the cube. dont know if .52 is right.
        
        if (!Physics.Raycast(transformPos,-transform.up,out hit,.52f)){
            if (!buildMode){
                StartCoroutine(FallDeath());
            }
        }  
        if (hit.collider != null){
            if (hit.collider.transform.gameObject.tag == "fallTile"){
                if (!hit.collider.transform.gameObject.GetComponent<FallTile>().on){
                    hit.collider.transform.gameObject.GetComponent<FallTile>().on = true;
                }
            }
        }
        if (Physics.Raycast(cubeTransform.position,transformupandforward,out hit,.55f)){
            StartCoroutine(DeathBackward());
        }
        else if (Physics.Raycast(cubeTransform.position,transform.forward,out hit,.46f)){
            StartCoroutine(DeathBackward());
        }
        else if (Physics.Raycast(cubeTransform.position,-transform.forward,out hit,.46f)){
            StartCoroutine(DeathForward());
        }
        else if (Physics.Raycast(cubeTransform.position,transform.right,out hit,.46f)){
            StartCoroutine(DeathLeft());
        }
        else if (Physics.Raycast(cubeTransform.position,-transform.right,out hit,.46f)){
            StartCoroutine(DeathRight());
        }
        

    }
    //the magic that makes this game tick/ animation that is secretly teleportation but it isnt cause raycasts rule

    IEnumerator Move(string direction){
        //this speed up power is really putting this code to the test. still some teleporting happening
        //i will have to keep investigating this but im not gonna bang my head about it. like ill fix it with time
        // cause its a decent bug.
        waiting = true;
        animator.SetTrigger(direction);
        
        animator.speed = rollSpeed;
        yield return new WaitForSeconds(.3f/rollSpeed);
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
        if (buildMode){
            GameObject g = Instantiate(playerMadeTile,new Vector3(transform.position.x,transform.position.y,transform.position.z),Quaternion.identity);
            
            blocksBuilt +=1;
            //if you place 4 blocks exit build mode.
            if (blocksBuilt >= 4){
                if (pmr != null){
                    pmr.material = PlayerMaterial;
                }
                    buildMode = false;
                    powerup = false;
                    blocksBuilt = 0;
            }
        }
        //animator.speed = rollSpeed;
        yield return new WaitForSeconds(.05f);
        //CheckFaces();
        waiting = false;
    }

    //making death look cool/work
    IEnumerator FallDeath(){
        alive = false;
        PlayerRb.constraints = RigidbodyConstraints.None;
        PlayerRb.constraints = RigidbodyConstraints.FreezeRotation;
        PlayerRb.constraints = RigidbodyConstraints.FreezePositionX;
        PlayerRb.constraints = RigidbodyConstraints.FreezePositionZ;
        animator.enabled = false;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
    IEnumerator DeathRight(){
        
        GameObject g = Instantiate(cubeReplacement,cubeTransform.position,Quaternion.identity);
        Destroy(cubeTransform.gameObject);
        
        alive = false;
        Rigidbody rb;
        foreach (Transform child in g.transform)
        {
            rb = child.GetComponent<Rigidbody>();
            child.GetComponent<MeshRenderer>().material = pmr.material;

            rb.AddForce(transform.right *6f,ForceMode.Impulse);
            
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
    IEnumerator DeathLeft(){
        
        GameObject g = Instantiate(cubeReplacement,cubeTransform.position,Quaternion.identity);
        Destroy(cubeTransform.gameObject);
        
        alive = false;
        Rigidbody rb;
        foreach (Transform child in g.transform)
        {
            child.GetComponent<MeshRenderer>().material = pmr.material;
            rb = child.GetComponent<Rigidbody>();
            rb.AddForce(-transform.right *4f,ForceMode.Impulse);
            
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
    IEnumerator DeathForward(){
        
        GameObject g = Instantiate(cubeReplacement,cubeTransform.position,Quaternion.identity);
        Destroy(cubeTransform.gameObject);
        
        alive = false;
        Rigidbody rb;
        foreach (Transform child in g.transform)
        {
            child.GetComponent<MeshRenderer>().material = pmr.material;
            rb = child.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward *4f,ForceMode.Impulse);
            
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
    IEnumerator DeathBackward(){
        
        GameObject g = Instantiate(cubeReplacement,cubeTransform.position,Quaternion.identity);
        Destroy(cubeTransform.gameObject);
        
        alive = false;
        Rigidbody rb;
        foreach (Transform child in g.transform)
        {
            child.GetComponent<MeshRenderer>().material = pmr.material;
            rb = child.GetComponent<Rigidbody>();
            rb.AddForce(-transform.forward *4f,ForceMode.Impulse);
            
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
    
}
