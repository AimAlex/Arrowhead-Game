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

        if (name=="door1")
        {
            GameObject.Find("door1").tag = "entry";
            destination = GameObject.Find("door2").transform;
        }
        else if(name == "door2")
        {
            GameObject.Find("door2").tag = "entry";
            destination = GameObject.Find("door1").transform;
        }
        else if (name == "door3")
        {
            GameObject.Find("door3").tag = "entry";
            destination = GameObject.Find("door4").transform;
        }
        else if (name == "door4")
        {
            GameObject.Find("door4").tag = "entry";
            destination = GameObject.Find("door3").transform;
        }

        else if (name == "door5")
        {
            GameObject.Find("door5").tag = "entry";
            destination = GameObject.Find("door6").transform;
        }
        else if (name == "door6")
        {
            GameObject.Find("door6").tag = "entry";
            destination = GameObject.Find("door5").transform;
        }
        else if (name == "door7")
        {
            GameObject.Find("door7").tag = "entry";
            destination = GameObject.Find("door8").transform;
        }

        else if (name == "door8")
        {
            GameObject.Find("door8").tag = "entry";
            destination = GameObject.Find("door7").transform;
        }
        else if (name == "door9")
        {
            GameObject.Find("door9").tag = "entry";
            destination = GameObject.Find("door10").transform;
        }
        else if (name == "door10")
        {
            GameObject.Find("door10").tag = "entry";
            destination = GameObject.Find("door9").transform;
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
            if (Vector2.Distance(transform.position, other.transform.position) > distance && transform.tag=="entry")
            {
                destination.tag = "exit";
                other.transform.position = new Vector2(destination.position.x, destination.position.y);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (transform.tag == "exit")
            {
                transform.tag = "entry";
            }
        }
    }





}


