using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.Tilemaps;


public class PlayerLife : MonoBehaviour
{
    public static Rigidbody2D rb;
    float timer, timer1,timer_collision;
    float restartHoldDur = 3f;
    float restartTimeAfterDie = 3f;
    public static string curScene;
    private PlayerMovement playerMovement;
    private AudioClip dieAudio;
    private float collisionDur=3f;
    private bool collisionStarted=false;
    private string collisionItem="";
    public bool hurtStarted=false;
    AudioSource m_MyAudioSource;
    // private float m_MySliderValue=0.1f;

    private float timeRestart;

    private bool restartStart = false;
    // private bool isFirstTimePressS = true;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer1 = float.PositiveInfinity;

        // //change audioSource volume
        // curScene = SceneManager.GetActiveScene().name;
        // m_MyAudioSource = GetComponent<AudioSource>();
        // Debug.Log("curScene="+curScene.ToString());
        // if(curScene=="level3-0" || curScene=="level3-1" || curScene=="level3-2" || curScene=="level3-3"){
        //     m_MyAudioSource.volume = 0.01f;
        // }else{
        //     m_MyAudioSource.volume = m_MySliderValue;
        // }


        //setup destination tag and collider
        var checkObj=GameObject.Find("Destination");
        if(checkObj!=null){
            if(checkObj.transform.childCount>0){
                foreach (Transform child in checkObj.transform)
                {
                    child.gameObject.tag="Finish";
                    if(child.gameObject.GetComponent<BoxCollider2D>()==null){
                        child.gameObject.AddComponent<BoxCollider2D>();
                    }
                    child.gameObject.GetComponent<BoxCollider2D>().isTrigger=true;
                }
            }else{
                checkObj.gameObject.tag="Finish";
                if(checkObj.gameObject.GetComponent<BoxCollider2D>()==null){
                    checkObj.gameObject.AddComponent<BoxCollider2D>();
                }
                checkObj.gameObject.GetComponent<BoxCollider2D>().isTrigger=true;
            }
        }

    }
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        dieAudio = Resources.Load<AudioClip>("music/die");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("trap"))
        {
                if(!collisionStarted){
                    timer_collision=Time.time;
                    collisionStarted=true;
                    collisionItem="trap";
                }
            if(!hurtStarted){
                hurtStarted=true;
                bool isStillAlive=FindObjectOfType<healthPoint>().UpdateHurt();
                if(!isStillAlive){
                    FindObjectOfType<AnalyticsScript>().KilledByTrap();
                    FindObjectOfType<Animation>().isDead=true;
                }else{
                    FindObjectOfType<Animation>().isHurt=true;
                }
            }
        }else if(col.gameObject.CompareTag("Deadzone")){
            FindObjectOfType<AnalyticsScript>().KilledByDeadzone();
            FindObjectOfType<Animation>().isDead=true;
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
                if(!collisionStarted){
                    timer_collision=Time.time;
                    collisionStarted=true;
                    collisionItem="Enemy";
                }
            if(!hurtStarted){
                hurtStarted=true;
                bool isStillAlive=FindObjectOfType<healthPoint>().UpdateHurt();
                if(!isStillAlive){
                    FindObjectOfType<AnalyticsScript>().KilledByEnemy();
                    FindObjectOfType<Animation>().isDead=true;
                }else{
                    FindObjectOfType<Animation>().isHurt=true;
                }
            }
        }
        else if (col.gameObject.CompareTag("bullet")|| col.gameObject.CompareTag("enemy_bullet"))

        {
                bool isStillAlive = FindObjectOfType<healthPoint>().UpdateHurt();
                if (!isStillAlive)
                {
                    FindObjectOfType<AnalyticsScript>().KilledByEnemy();
                    FindObjectOfType<Animation>().isDead = true;
                }
                else
                {
                    FindObjectOfType<Animation>().isHurt = true;
                }
            
        }
    }

    private void OnCollisionExit2D(Collision2D col){
        if (col.gameObject.CompareTag("trap"))
        {
            collisionStarted=false;
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            collisionStarted=false;
        }
        else if (col.gameObject.CompareTag("bullet") || col.gameObject.CompareTag("enemy_bullet"))
        {
            collisionStarted = false;
        }

    }

    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.CompareTag("Enemy"))
    //     {
    //             if(!collisionStarted){
    //                 timer_collision=Time.time;
    //                 collisionStarted=true;
    //                 collisionItem="Enemy";
    //             }
    //         if(!hurtStarted){
    //             hurtStarted=true;
    //             bool isStillAlive=FindObjectOfType<healthPoint>().UpdateHurt();
    //             if(!isStillAlive){
    //                 FindObjectOfType<AnalyticsScript>().KilledByEnemy();
    //                 FindObjectOfType<Animation>().isDead=true;
    //             }else{
    //                 FindObjectOfType<Animation>().isHurt=true;
    //             }
    //         }
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D col)
    // {
    //     if (col.CompareTag("Enemy"))
    //     {
    //         collisionStarted=false;
    //     }

    // }

    
    void Update()
    {

        if(collisionStarted){
            if(Time.time-timer_collision>collisionDur){
                if(!FindObjectOfType<healthPoint>().UpdateHurt()){
                    if(collisionItem=="trap"){
                        FindObjectOfType<AnalyticsScript>().KilledByTrap();
                    }else if(collisionItem=="Enemy"){
                        FindObjectOfType<AnalyticsScript>().KilledByEnemy();
                    }
                    FindObjectOfType<Animation>().isDead=true;
                }else{
                    FindObjectOfType<Animation>().isHurt=true;
                }
                timer_collision=timer_collision+collisionDur;
            }
        }
        
        if (Input.GetKeyDown("s"))
        {
            timer = Time.time;
            
            Debug.Log("restart begin");
        }
        else if (Input.GetKey("s"))
        {
            if (Time.time - timer > restartHoldDur)
            {
                timer = float.PositiveInfinity;
                RestartLevel();
            }
        }
        /*
        else if (Input.GetKey("s"))
        {
            // Debug.Log("restart in process");
            if (Time.time - timer > restartHoldDur)
            {
                Animation.anim.SetBool("restart", true);
                if (!restartStart)
                {
                    restartStart = true;
                    timeRestart = Time.time;
                }
                
                if (restartStart && Time.time - timeRestart > 2f)
                {
                    timer = float.PositiveInfinity;
                    restartStart = false;
                    Animation.anim.SetBool("restart", false);
                    Animation.anim.SetBool("idle", true);
                    // Debug.Log("restart end");
                    RestartLevel();
                }

            }
        }
        */
        /*
        if (Input.GetKey("s"))
        {
            StartCoroutine(Restart());
        }
        */
        
        else if (SceneManager.GetActiveScene().name == "DieScene")
        {
            if (timer1 == float.PositiveInfinity)
            {
                timer1 = Time.time;
            }
            if (Time.time - timer1 > restartTimeAfterDie)
            {
                SceneManager.LoadScene(curScene);
                timer1 = float.PositiveInfinity;
            }
        }
        else
        {
            timer = float.PositiveInfinity;
            timer1 = float.PositiveInfinity;
        }
    }
    /*
    public IEnumerator Restart()
    {
        // Animation.anim.SetBool("restart", true);
        yield return new WaitForSeconds(2f);
        // Animation.anim.SetBool("restart", false);
        // Animation.anim.SetBool("idle", true);
        RestartLevel();
    }
    */
    
    public void Retry()
    {
        SceneManager.LoadScene(curScene);
    }

    public static void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        curScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("DieScene");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
