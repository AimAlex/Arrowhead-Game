using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootIem : MonoBehaviour
{
    private float bulletSpeed=1f;
    private Vector3 direction;
    private bool ISshoot=false;
    /*private bool shoot2;*/
    // Start is called before the first frame update
    void Start()
    {
        /*   gameObject.SetActive(false);*/

        // Debug.Log(transform.tag);

    }

    // Update is called once per frame
    void Update()
    {
        if (ISshoot)
        {
            transform.Translate(bulletSpeed * Time.deltaTime * direction);
        }

        if (transform.tag== "bullet"&& !ISshoot)
        {
            shoot();
            ISshoot = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        Destroy(gameObject);

    }

    public void shoot()
    {

        // Debug.Log("bullet start func");
        var boss = GameObject.Find("Boss");
        this.transform.position = boss.transform.position;
        direction = new Vector3(FindObjectOfType<PlayerMovement>().rigidbody.position.x, FindObjectOfType<PlayerMovement>().rigidbody.position.y, 0) - this.transform.position;

    }

    // private void bulletMoving(){
    //     transform.Translate
    // }
}
