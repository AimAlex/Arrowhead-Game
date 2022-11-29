using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemy_bullet;

    public Vector2 direction;
    private RaycastHit2D hitInfo;
    private float time;
    private float timeDelay;
    public bool hurtStarted = false;
    private Rigidbody2D playerRigidbody;
    private Vector3 enemyPos;
    private Vector3 playerPos;

    private bool playerClose = false;
    private float shootingRange = 100f;
    private float timer;
    private float shotGap = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time - shotGap;
        if (enemy_bullet != null)
        {
            if (enemy_bullet.GetComponent<PolygonCollider2D>() == null)
            {
                enemy_bullet.AddComponent<PolygonCollider2D>();
            }
        }
        time = 0f;
        timeDelay = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        time = time + 1f * Time.deltaTime;
        playerRigidbody = FindObjectOfType<PlayerMovement>().rigidbody;
        playerPos = playerRigidbody.position;
        enemyPos = transform.position;


        var dir = new Vector3(FindObjectOfType<PlayerMovement>().rigidbody.position.x, FindObjectOfType<PlayerMovement>().rigidbody.position.y, 0) - this.transform.position;
        //    Debug.Log(dir.x*dir.x+dir.y*dir.y+dir.z*dir.z);
        if (dir.x * dir.x + dir.y * dir.y + dir.z * dir.z < shootingRange)
        {
            playerClose = true;
        }
        else
        {
            playerClose = false;
        }
        if (Time.time - timer > shotGap && playerClose&&(FindObjectOfType<PlayerMovement>().rigidbody.position.y- this.transform.position.y>=-1))
        {
            ShootItem();
            timer = Time.time;
        }

    }

    private void ShootItem()
    {
        // Debug.Log("shoot item");
        // prefab_shootItem=GameObject.Find("bullet");
        GameObject shotItem = Instantiate(enemy_bullet);
        shotItem.transform.tag = "enemy_bullet";

    }
}
