
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
    public float radius = 0.8f;
    private Collider2D Coll;
    private Rigidbody2D Rb;
    public GameObject bomb1, bomb2, bomb3;
    public LayerMask BoomMask;
    public Vector2 bomb_position;
    private Pickupbomb pickupbomb;
    private PlayerMovement playerMovement;
    private AudioClip bombExplodeAudio;
    private  Color32 bombcolor;

    // Start is called before the first frame update


private void Awake()
{
    bomb1 = GameObject.Find("Bomb1");
    bomb2 = GameObject.Find("Bomb2");
    bomb3 = GameObject.Find("Bomb3");
    Coll = GetComponent<Collider2D>();
    pickupbomb = GameObject.Find("Player").GetComponent<Pickupbomb>();
    playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    bombExplodeAudio = Resources.Load<AudioClip>("music/bomb_explosion");
    bombcolor=new Color32(255,255,255,255);
}
void Update()
{
    if (bomb1.GetComponent<SpriteRenderer>().color == bombcolor && pickupbomb.bomb_dict[bomb1.name])
    {
        StartCoroutine(Explotion(bomb1,bomb1.GetComponent<Animator>()));
    } else if (bomb2.GetComponent<SpriteRenderer>().color == bombcolor && pickupbomb.bomb_dict[bomb2.name])
    {
        StartCoroutine(Explotion(bomb2,bomb2.GetComponent<Animator>()));
    } else if (bomb3.GetComponent<SpriteRenderer>().color == bombcolor && pickupbomb.bomb_dict[bomb3.name])
    {
        StartCoroutine(Explotion(bomb3,bomb3.GetComponent<Animator>()));
    }
}

IEnumerator Explotion(GameObject bomb, Animator anim)
{
    pickupbomb.bomb_dict[bomb.name] = false;
    yield return new WaitForSeconds(1);
    anim.SetBool("bomb1", true);
    anim.SetBool("Idle", false);
	//FindObjectOfType<BombAnimation>().second1=true;
	yield return new WaitForSeconds(1);
	//bomb.GetComponent<SpriteRenderer>().color = Color.red;
	//FindObjectOfType<BombAnimation>().second2=true;
    anim.SetBool("bomb2", true);
    anim.SetBool("bomb1", false);
	yield return new WaitForSeconds(1);
    anim.SetBool("explode", true);
    // Debug.Log("anim="+anim.ToString());
    anim.SetBool("bomb2", false);
    yield return new WaitForSeconds(0.3f);

    playerMovement.PlayAudio2(bombExplodeAudio);

    bomb_position = bomb.transform.position;
    Collider2D[] CollCheck = Physics2D.OverlapCircleAll(bomb_position, radius);

    foreach (var item in CollCheck)
    {
        if (item.name != "4_1ground" 
                && item.name!="Player" 
                    && !item.CompareTag("Booster")
                    && !item.CompareTag("Boss")
                        && !item.CompareTag("Treasure")
                            &&!item.CompareTag("indestructible")
                                &&item.name!="Square"
                                    &&!item.CompareTag("Finish")
                                        &&!item.CompareTag("entry")
                                            &&!item.CompareTag("Deadzone")
            )
        {
            // Debug.Log(item);
            Destroy(item.gameObject);
        }
    }
	//FindObjectOfType<BombAnimation>().isExplode=true;
	// yield return new WaitForSeconds(0.5f);
    // Debug.Log("Explotion: " + pickupbomb.bomb_dict[bomb.name]);
    // Coll.enabled = false;
    
    
    // Debug.Log(bomb_position);
    // Debug.Log(radius);

    
    //Destroy(bomb);
    
    bomb.GetComponent<SpriteRenderer>().color = Color.clear;
    bomb_position = bomb.transform.position;
    bomb_position.x = -27.9f;
    bomb_position.y = 4.48f;
    bomb.GetComponent<Transform>().position = bomb_position;
    anim.SetBool("Idle", true);
    anim.SetBool("explode", false);
    //FindObjectOfType<BombAnimation>().idle=true;
    
}
    
    

}
