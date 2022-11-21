using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour

{
    private Transform destination;
    public bool isdoor1;
    public float distance = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        if (isdoor1 == false)
        {
            destination = GameObject.FindGameObjectWithTag("door1").GetComponent<Transform>();
        }
        else
        {
            destination = GameObject.FindGameObjectWithTag("door2").GetComponent<Transform>();
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


