using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;


public class PlayerLifeWithHP : MonoBehaviour
{
    private Rigidbody2D rb;
    float timer, timer1;
    float restartHoldDur = 2f;
    float restartTimeAfterDie = 3f;
    private string curScene="HealthPoint";
    public Image blood0;
    public Image blood1;
    public Image blood2;
    public Image blood3;
    public Image blood4;
    private int hurt=0;
    private PlayerMovement playerMovement;
    private AudioClip dieAudio;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer1 = float.PositiveInfinity;
        blood1.enabled=false;
        blood2.enabled=false;
        blood3.enabled=false;
        blood4.enabled=false;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        dieAudio = Resources.Load<AudioClip>("music/die");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("trap"))
        {
            hurt++;
            if(hurt==1){
                blood0.enabled=false;
                blood1.enabled=true;
            }else if(hurt==2){
                blood1.enabled=false;
                blood2.enabled=true;
            }else if(hurt==3){
                blood2.enabled=false;
                blood3.enabled=true;
            }else if(hurt==4){
                blood3.enabled=false;
                blood4.enabled=true;
                FindObjectOfType<AnalyticsScript>().KilledByEnemy();
                Die();
            }

        }else if(col.gameObject.CompareTag("Enemy")){
            FindObjectOfType<AnalyticsScript>().KilledByEnemy();
            Die();
        }else if(col.gameObject.CompareTag("Deadzone")){
            FindObjectOfType<AnalyticsScript>().KilledByDeadzone();
            Die();
        }
    }

        private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            hurt++;
            if(hurt==1){
                blood0.enabled=false;
                blood1.enabled=true;
            }else if(hurt==2){
                blood1.enabled=false;
                blood2.enabled=true;
            }else if(hurt==3){
                blood2.enabled=false;
                blood3.enabled=true;
            }else if(hurt==4){
                blood3.enabled=false;
                blood4.enabled=true;
                FindObjectOfType<AnalyticsScript>().KilledByEnemy();
                Die();
            }
        }

    }
    
    void Update()
    {
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
                // Debug.Log("curScene=");
                // Debug.Log(curScene);
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

    private void Die()
    {
        playerMovement.PlayAudio(dieAudio);
        rb.bodyType = RigidbodyType2D.Static;
        // curScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("DieScene");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
