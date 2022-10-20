using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class PlayerLifeWithAnim : MonoBehaviour
{
    private Rigidbody2D rb;
    float timer, timer1;
    float restartHoldDur = 2f;
    float restartTimeAfterDie = 3f;
    public static string curScene;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer1 = float.PositiveInfinity;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("trap"))
        {
            FindObjectOfType<AnalyticsScript>().KilledByTrap();
            FindObjectOfType<Animation>().isDead=true;
            // Die();
        }else if(col.gameObject.CompareTag("Enemy")){
            FindObjectOfType<AnalyticsScript>().KilledByEnemy();
            FindObjectOfType<Animation>().isDead=true;
            // Die();
        }else if(col.gameObject.CompareTag("Deadzone")){
            FindObjectOfType<AnalyticsScript>().KilledByDeadzone();
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            FindObjectOfType<AnalyticsScript>().KilledByEnemy();
            FindObjectOfType<Animation>().isDead=true;
            // Die();
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

    public void Die()
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
