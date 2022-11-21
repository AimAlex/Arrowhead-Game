using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour

{
    private Transform destination;
    public float distance = 0.2f;
    // Start is called before the first frame update
    void Start()

    {
        var name = transform.name;
        if (name=="door2")
        {
            destination = GameObject.Find("door1").transform;
        }
        else
        {
            destination = GameObject.Find("door2").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Vector2.Distance(transform.position, other.transform.position) > distance)
            {
                other.transform.position = new Vector2(destination.position.x, destination.position.y);
            }
        }
    }


     

    
}


