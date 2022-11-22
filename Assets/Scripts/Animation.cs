using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;


public class Animation : MonoBehaviour
{
    public bool isDead=false;
    // private bool isIdle=false;
    // private bool isRunning=false;
    private bool deathPauseStarted=false;
    private float deathPauseDur=0.5f;
    private float timer_death,timer_hurt,timer_dash,timer_restart;
    private arcMovement[] arcs;
    private trapMovement[] traps;
    private Enemy[] enemies;
    public static Animator anim;
    public static Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private AudioClip dieAudio;
    public bool isHurt=false;
    private float hurtDur=0.8f;
    private bool hurtDurStarted=false;
    // public bool isRestart = false;
    // private float restartDur = 1.0f;
    // public bool restartDurStarted = false;

    // Start is called before the first frame update
    private void Start()
    {
        anim=GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        dieAudio = Resources.Load<AudioClip>("music/die");
    }

    void Update()
    {
        if(isHurt && !hurtDurStarted){
            hurtDurStarted=true;
            anim.SetBool("hurt",true);
            timer_hurt=Time.time;
            // this.GetComponent<Rigidbody2D>().velocity=new Vector3(0,0,0);
            // this.GetComponent<Rigidbody2D>().gravityScale=0;
        }
        
        if(hurtDurStarted){
            if(Time.time-timer_hurt>hurtDur){
                if (FindObjectOfType<PlayerMovement>().isRunning)
                {
                    anim.SetBool("running", true);
                    anim.SetBool("hurt", false);
                }
                else
                {
                    anim.SetBool("idle",true);
                    anim.SetBool("hurt",false);
                }
                isHurt=false;
                hurtDurStarted=false;
                FindObjectOfType<PlayerLife>().hurtStarted=false;
                if (FindObjectOfType<Enemy>() is not null)
                {
                    FindObjectOfType<Enemy>().hurtStarted=false;
                }
                // this.GetComponent<Rigidbody2D>().velocity=new Vector3(0,0,0);
                // this.GetComponent<Rigidbody2D>().gravityScale=1;
            }
        }
        /*
        if (isRestart && !restartDurStarted)
        {
            restartDurStarted = true;
            anim.SetBool("restart", true);
            timer_restart = Time.time;
        }

        if (restartDurStarted)
        {
            if (Time.time - timer_restart > restartDur)
            {
                anim.SetBool("idle", true);
                anim.SetBool("restart", false);
                isRestart = false;
                restartDurStarted = false;
            }
        }
        */
        if(isDead && !deathPauseStarted){
            anim.SetBool("dead",true);
            deathPauseStarted=true;
            timer_death=Time.time;
            playerMovement.PlayAudio(dieAudio);
            FindObjectOfType<PlayerMovement>().moveSpeed=0;
            FindObjectOfType<PlayerMovement>().jumpForce=0;
            this.GetComponent<Rigidbody2D>().velocity=new Vector3(0,0,0);
            this.GetComponent<Rigidbody2D>().gravityScale=0;

            arcs=Resources.FindObjectsOfTypeAll<arcMovement>();
            traps=Resources.FindObjectsOfTypeAll<trapMovement>();
            enemies=Resources.FindObjectsOfTypeAll<Enemy>();
            for(int i=0;i<arcs.Length;i++){
                arcs[i].speed=0;
            }
            for(int i=0;i<traps.Length;i++){
                traps[i].speed=0;
            }
            for(int i=0;i<enemies.Length;i++){
                //enemies[i].speed=0;
            }
        }

        if(deathPauseStarted){
            if(Time.time-timer_death>deathPauseDur){
                PlayerLife.Die();
            }
        }

    }
}
