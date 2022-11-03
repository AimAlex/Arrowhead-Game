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
    private float deathPauseDur=1.5f;
    private float timer;
    private arcMovement[] arcs;
    private trapMovement[] traps;
    private Enemy[] enemies;
    public static Animator anim;
    public static Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private AudioClip dieAudio;

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

        if(isDead && !deathPauseStarted){
            anim.SetBool("dead",true);
            deathPauseStarted=true;
            timer=Time.time;

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
            if(Time.time-timer>deathPauseDur){
                playerMovement.PlayAudio(dieAudio);
                PlayerLife.Die();
            }
        }

    }
}
