using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_shootItem : MonoBehaviour
{

    private float bulletSpeed = 1f;
    private Vector3 direction;
    private bool ISshoot = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ISshoot)
        {
            transform.Translate(bulletSpeed * Time.deltaTime * direction);
        }

        if (transform.tag == "enemy_bullet" && !ISshoot)
        {
            shoot();
            ISshoot = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

    }

    public void shoot()
    {

        // Debug.Log("bullet start func");
        var enemy = GameObject.Find("enemy_shoot");
        this.transform.position = enemy.transform.position;
        direction = new Vector3(FindObjectOfType<PlayerMovement>().rigidbody.position.x, FindObjectOfType<PlayerMovement>().rigidbody.position.y, 0) - this.transform.position;

    }
}
