using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    float timer;
    float restartHoldDur = 2f;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("trap") || col.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }
    
    void Update()
    {
        if(Input.GetKeyDown("s"))
        {
            timer = Time.time;
        }
        else if(Input.GetKey("s"))
        {
            if(Time.time - timer > restartHoldDur)
            {
                timer = float.PositiveInfinity;
                RestartLevel();
            }
        }
        else
        {
            timer = float.PositiveInfinity;
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        FindObjectOfType<AnalyticsScript>().KilledByTrap();
        RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
