
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;
public class Bomb: MonoBehaviour
{

    public float waitTime;
    public float radius = 2;
    private Collider2D Coll;
    private Rigidbody2D Rb;
    public GameObject bomb1, bomb2, bomb3;
    public LayerMask BoomMask;
    public Vector2 bomb_position;
    private Pickupbomb pickupbomb;
    private PlayerMovement playerMovement;
    private AudioClip bombExplodeAudio;
    


private void Awake()
{
    bomb1 = GameObject.Find("Bomb1");
    bomb2 = GameObject.Find("Bomb2");
    bomb3 = GameObject.Find("Bomb3");
    Coll = GetComponent<Collider2D>();
    pickupbomb = GameObject.Find("Player").GetComponent<Pickupbomb>();
    playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    bombExplodeAudio = Resources.Load<AudioClip>("music/bomb_explosion");
}
void Update()
{
    if (bomb1.GetComponent<SpriteRenderer>().color == Color.green && pickupbomb.bomb_dict[bomb1.name])
    {
        StartCoroutine(Explotion(bomb1));
    } else if (bomb2.GetComponent<SpriteRenderer>().color == Color.green && pickupbomb.bomb_dict[bomb2.name])
    {
        StartCoroutine(Explotion(bomb2));
    } else if (bomb3.GetComponent<SpriteRenderer>().color == Color.green && pickupbomb.bomb_dict[bomb3.name])
    {
        StartCoroutine(Explotion(bomb3));
    }
}

IEnumerator Explotion(GameObject bomb)
{
    pickupbomb.bomb_dict[bomb.name] = false;
    yield return new WaitForSeconds(1);
	FindObjectOfType<BombAnimation>().second1=true;
	yield return new WaitForSeconds(1);
	FindObjectOfType<BombAnimation>().second2=true;
	yield return new WaitForSeconds(1);
	FindObjectOfType<BombAnimation>().isExplode=true;
    // Debug.Log("Explotion: " + pickupbomb.bomb_dict[bomb.name]);
    // Coll.enabled = false;
    bomb_position = bomb.transform.position;
    playerMovement.PlayAudio2(bombExplodeAudio);
    // Debug.Log(bomb_position);
    // Debug.Log(radius);
    Collider2D[] CollCheck = Physics2D.OverlapCircleAll(bomb_position, radius);

    foreach (var item in CollCheck)
    {
        if (item.name != "4_1ground" 
                && item.name!="Player" 
                    && !item.CompareTag("Booster") 
                        &&!item.CompareTag("Treasure")
                            &&!item.CompareTag("indestructible")
                            &&item.name!="Square"
                                &&!item.CompareTag("Finish")
            )
        {
            // Debug.Log(item);
            Destroy(item.gameObject);
        }
    }
    
    //Destroy(bomb);
    bomb.GetComponent<SpriteRenderer>().color = Color.clear;
    bomb_position = bomb.transform.position;
    bomb_position.x = -27.9f;
    bomb_position.y = 4.48f;
    bomb.GetComponent<Transform>().position = bomb_position;
    FindObjectOfType<BombAnimation>().idle=true;
}
    
    

}
