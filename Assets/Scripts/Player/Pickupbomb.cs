using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;
public class Pickupbomb : MonoBehaviour
{
    public bool collectBomb;
    public GameObject player;
    public GameObject bomb;
    
    public Vector2 player_position;
    public Vector2 bomb_position;
    public float bomb_x;
    public float bomb_y;
    public float pos_x;
    public float pos_y;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Booster"))
        {
            if(col.name == "bombItem")
            {
                collectBomb = true;
                Destroy(col.gameObject);
            } 
        }
    }

    private void Awake()
    {
        Debug.Log("Awake");
        player = GameObject.Find("Player");
        bomb = GameObject.Find("Bomb");
    }

    //current setting: only use bomb for one time in one frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)&&collectBomb==true)
        {
            DropBomb();
        }
    }
    
    private void DropBomb()
    {
        Debug.Log("dropbomb");
        // get the player object location, get the bomb object,   make it visible, change the location
        //GameObject new_bomb = GameObject.Instantiate(bomb) as GameObject;
        player_position = player.transform.position;
        pos_x = player_position.x;
        pos_y = player_position.y;
        bomb_position = bomb.transform.position;
        bomb_position.x = pos_x + 1;
        bomb_position.y = pos_y;
        bomb.GetComponent<Transform>().position = bomb_position;
        bomb.GetComponent<SpriteRenderer>().color = Color.green;
        
    }
}