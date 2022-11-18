using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class PlayerLife : MonoBehaviour
{
    public static Rigidbody2D rb;
    float timer, timer1,timer_collision;
    float restartHoldDur = 2f;
    float restartTimeAfterDie = 3f;
    public static string curScene;
    private PlayerMovement playerMovement;
    private AudioClip dieAudio;
    private float collisionDur=3f;
    private bool collisionStarted=false;
    private string collisionItem="";
    public bool hurtStarted=false;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer1 = float.PositiveInfinity;
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
        }
        else if (Input.GetKey("s"))
        {
            if (Time.time - timer > restartHoldDur)
            {
                timer = float.PositiveInfinity;
                RestartLevel();
            }
        }
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
