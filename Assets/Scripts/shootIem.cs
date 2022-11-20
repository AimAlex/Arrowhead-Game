using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootIem : MonoBehaviour
{
    private float bulletSpeed=1f;
    private Vector3 direction=new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("bullet start func");
        var boss=GameObject.Find("Boss");
        this.transform.position=boss.transform.position;
        direction = new Vector3(FindObjectOfType<PlayerMovement>().rigidbody.position.x,FindObjectOfType<PlayerMovement>().rigidbody.position.y,0)-this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(bulletSpeed * Time.deltaTime * direction);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        // if(collision.tag=="Player"){
            // Destroy(gameObject);
        // }
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision){
        // Destroy(gameObject);
        gameObject.SetActive(false);
    }

    // private void bulletMoving(){
    //     transform.Translate
    // }
}
