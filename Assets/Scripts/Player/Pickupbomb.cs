using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;
public class Pickupbomb : MonoBehaviour
{
    public static bool collectBomb;
    public GameObject player;
    public GameObject bomb1;
	public GameObject bomb2;
	public GameObject bomb3;
    public int bomb_count;
    public Vector2 player_position;
    public Vector2 bomb_position;
    public float bomb_x;
    public float bomb_y;
    public float pos_x;
    public float pos_y;
    public Dictionary<int,string> bomb_dict;
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
        collectBomb = false;
        Debug.Log("Awake");
        player = GameObject.Find("Player");
        bomb1 = GameObject.Find("Bomb1");
        bomb2 = GameObject.Find("Bomb2");
        bomb3 = GameObject.Find("Bomb3");
		bomb_count=0;
     
    }

    //current setting: only place three bombs in a row at most in one frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)&&collectBomb==true)
        {
            DropBomb();
			bomb_count+=1;
			
        }
		if (bomb_count==3){
			bomb_count=0;
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
		if(bomb_count==0){
        bomb_position = bomb1.transform.position;
        bomb_position.x = pos_x + 1;
        bomb_position.y = pos_y;
        bomb1.GetComponent<Transform>().position = bomb_position;
        bomb1.GetComponent<SpriteRenderer>().color = Color.green;
        bomb_count+=1;
		}
       else if(bomb_count==1){
        bomb_position = bomb2.transform.position;
        bomb_position.x = pos_x + 1;
        bomb_position.y = pos_y;
        bomb2.GetComponent<Transform>().position = bomb_position;
        bomb2.GetComponent<SpriteRenderer>().color = Color.green;
        bomb_count+=1;
		}
		else if(bomb_count==2)
       {
        bomb_position = bomb3.transform.position;
        bomb_position.x = pos_x + 1;
        bomb_position.y = pos_y;
        bomb3.GetComponent<Transform>().position = bomb_position;
        bomb3.GetComponent<SpriteRenderer>().color = Color.green;
        bomb_count+=1;
		}   
    }
}
