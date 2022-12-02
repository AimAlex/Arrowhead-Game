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
    private int player_face = 0; // 0: right, 1: left
    public Dictionary<string,bool> bomb_dict;
    private PlayerMovement playerMovement;
    private AudioClip bombDropAudio;
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
        // Debug.Log("Awake");
        player = GameObject.Find("Player");
        bomb1 = GameObject.Find("Bomb1");
        bomb2 = GameObject.Find("Bomb2");
        bomb3 = GameObject.Find("Bomb3");
        bomb1.GetComponent<SpriteRenderer>().color = Color.clear;
        bomb2.GetComponent<SpriteRenderer>().color = Color.clear;
        bomb3.GetComponent<SpriteRenderer>().color = Color.clear;
		bomb_count=0;
        bomb_dict = new Dictionary<string, bool>();
        bomb_dict[bomb1.name] = false;
        bomb_dict[bomb2.name] = false;
        bomb_dict[bomb3.name] = false; 
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        bombDropAudio = Resources.Load<AudioClip>("music/PutdownBomb");
    }

    //current setting: only place three bombs in a row at most in one frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)&&collectBomb==true)
        {
            if (bomb_dict[bomb1.name] == false && bomb1.GetComponent<SpriteRenderer>().color == Color.clear){
                DropBomb(bomb1);
            } else if (bomb_dict[bomb2.name] == false && bomb2.GetComponent<SpriteRenderer>().color == Color.clear){
                DropBomb(bomb2);
            } else if (bomb_dict[bomb3.name] == false && bomb3.GetComponent<SpriteRenderer>().color == Color.clear){
                DropBomb(bomb3);
            }
        }
        if (Input.GetKeyDown(KeyCode.D)){
            player_face = 0;
        } else if (Input.GetKeyDown(KeyCode.A)){
            player_face = 1;
        }
    }
    
    private void DropBomb(GameObject bomb)
    {
        bomb.GetComponent<Rigidbody2D>().drag = 1f;
        playerMovement.PlayAudio2(bombDropAudio);
        // Debug.Log(bomb.name);
        bomb_dict[bomb.name] = true;
        //bomb.GetComponent<SpriteRenderer>().color = Color.white;
        bomb.GetComponent<SpriteRenderer>().color=new Color32(255,255,255,255);
        // get the player object location, get the bomb object,   make it visible, change the location
        //GameObject new_bomb = GameObject.Instantiate(bomb) as GameObject;
        player_position = player.transform.position;
        pos_x = player_position.x;
        pos_y = player_position.y;
	
        bomb_position = bomb.transform.position;
        bomb_position.x = pos_x;
        bomb_position.y = pos_y + 0.3f;
        bomb.GetComponent<Transform>().position = bomb_position;
        // Debug.Log(player_face);
        if (player_face == 0){
            bomb.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x/2.5f + 4, player.GetComponent<Rigidbody2D>().velocity.y/2.5f + 3);
        } else if (player_face == 1){
            bomb.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x/2.5f - 4, player.GetComponent<Rigidbody2D>().velocity.y/2.5f + 3);
        }
        bomb_count+=1;
		
       /*else if(bomb_count==1){
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
		}   */
    }
}
