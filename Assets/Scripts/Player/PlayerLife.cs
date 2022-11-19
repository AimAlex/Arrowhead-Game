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
    float restartHoldDur = 2f;
    float restartTimeAfterDie = 3f;
    public static string curScene;
    private PlayerMovement playerMovement;
    private AudioClip dieAudio;
    private float collisionDur=3f;
    private bool collisionStarted=false;
    private string collisionItem="";
    public bool hurtStarted=false;
    AudioSource m_MyAudioSource;
    private float m_MySliderValue=0.1f;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer1 = float.PositiveInfinity;

        //change audioSource volume
        m_MyAudioSource = GetComponent<AudioSource>();
        m_MyAudioSource.volume = m_MySliderValue;

        // Change instruction image
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            // if (objs[i].hideFlags == HideFlags.None)
            // {
                if (objs[i].name == "Picture")
                {
                    objs[i].gameObject.GetComponent<RawImage>().texture=Resources.Load<Texture2D>("instruction_new");
                    objs[i].gameObject.GetComponent<RectTransform>().sizeDelta=new Vector2(1000,600);
                    objs[i].gameObject.GetComponent<RectTransform>().rotation = Quaternion.EulerAngles(0f, 0f, 0f);
                }
            // }
        }

        //setup destination tag and collider
        var checkObj=GameObject.Find("Destination");
        if(checkObj.transform.childCount>0){
            foreach (Transform child in checkObj.transform)
            {
                child.gameObject.tag="Finish";
                if(child.gameObject.GetComponent<PolygonCollider2D>()==null){
                    child.gameObject.AddComponent<PolygonCollider2D>();
                }
                child.gameObject.GetComponent<PolygonCollider2D>().isTrigger=true;
            }
        }else{
            checkObj.gameObject.tag="Finish";
            if(checkObj.gameObject.GetComponent<PolygonCollider2D>()==null){
                checkObj.gameObject.AddComponent<PolygonCollider2D>();
            }
            checkObj.gameObject.GetComponent<PolygonCollider2D>().isTrigger=true;
        }

        // check treasures tag and collider
        checkObj=GameObject.Find("Treasure");
        if(checkObj.transform.childCount>0){
            foreach (Transform child in checkObj.transform)
            {
                child.gameObject.tag="Treasure";
                if(child.gameObject.GetComponent<PolygonCollider2D>()==null){
                    child.gameObject.AddComponent<PolygonCollider2D>();
                }
                child.gameObject.GetComponent<PolygonCollider2D>().isTrigger=true;
            }
        }else{
            checkObj.gameObject.tag="Treasure";
            if(checkObj.gameObject.GetComponent<PolygonCollider2D>()==null){
                checkObj.gameObject.AddComponent<PolygonCollider2D>();
            }
            checkObj.gameObject.GetComponent<PolygonCollider2D>().isTrigger=true;
        }

        // reset tilemap collider
        checkObj=GameObject.Find("Grid");
        if(checkObj.transform.childCount>0){
            foreach (Transform child in checkObj.transform)
            {
                if(child.transform.childCount>0){
                    foreach (Transform child2 in checkObj.transform)
                    {
                        if(child2.gameObject.GetComponent<TilemapCollider2D>()!=null){
                           Destroy(child2.gameObject.AddComponent<TilemapCollider2D>());
                        }
                           child2.gameObject.AddComponent<TilemapCollider2D>();
                    }
                }else{
                    if(child.gameObject.GetComponent<TilemapCollider2D>()!=null){
                        Destroy(child.gameObject.AddComponent<TilemapCollider2D>());
                    }
                        child.gameObject.AddComponent<TilemapCollider2D>();
                }
            }
        }else{
            if(checkObj.gameObject.GetComponent<TilemapCollider2D>()!=null){
                Destroy(checkObj.gameObject.AddComponent<TilemapCollider2D>());
            }
                checkObj.gameObject.AddComponent<TilemapCollider2D>();
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
