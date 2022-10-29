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
    private bool deathPauseStarted=false;
    private float deathPauseDur=1.5f;
    private float timer;
    private arcMovement[] arcs;
    private trapMovement[] traps;
    private Enemy[] enemies;
    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        anim=GetComponent<Animator>();
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
                FindObjectOfType<PlayerLifeWithAnim>().Die();
            }
        }

    }
}
