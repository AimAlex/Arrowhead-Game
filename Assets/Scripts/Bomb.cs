
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
    public float radius = 0.01F;
    private Collider2D Coll;
    private Rigidbody2D Rb;
    public GameObject bomb;
    public LayerMask BoomMask;
    public Vector2 bomb_position;
 


private void Awake()
{
    Debug.Log("Bomb");
    bomb = GameObject.Find("Bomb");
    Coll = GetComponent<Collider2D>();

}
void Update()
{
    if (bomb.GetComponent<SpriteRenderer>().color == Color.green)
    {
        
        StartCoroutine(Explotion());
    }
}

IEnumerator Explotion()
{
    yield return new WaitForSeconds(3);
    Debug.Log("Explotion");
    Coll.enabled = false;
    bomb_position = bomb.transform.position;
    Debug.Log(bomb_position);
    Debug.Log(radius);
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

            Debug.Log(item);
            Destroy(item.gameObject);
        }
    }
    
    //Destroy(bomb);
    bomb.GetComponent<SpriteRenderer>().color = Color.clear;
    bomb_position = bomb.transform.position;
    bomb_position.x = -27.9f;
    bomb_position.y = 4.48f;
    bomb.GetComponent<Transform>().position = bomb_position;
}
    
    

}
