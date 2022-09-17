using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class itemCollect : MonoBehaviour
{
    private int itemNumber = 0;
    private SpriteRenderer _renderer;

    // Level 3 variables;
    public GameObject item1Prefab;
    public GameObject item2Prefab;
    public GameObject item3Prefab;
    public GameObject item4Prefab;
    private bool hasPower=false;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Treasure"))
        {
            ++itemNumber;
            Destroy(col.gameObject);
        }

        if (col.CompareTag("Finish"))
        {
            if (itemNumber == 4)
            {
                _renderer.color = Color.black; 
                Destroy(col.gameObject);

                // Analytics codes
                FindObjectOfType<AnalyticsScript>().Success();

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Level 3 codes
     private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag=="Treasure")
        {
            ++itemNumber;

            if(hasPower){
                spaw_cloned_items();
            }

            Destroy(col.gameObject);

        }

        if (col.collider.tag=="treasure(cloned)")
        {
            ++itemNumber;
            Destroy(col.gameObject);
        }


        if (col.collider.tag=="Finish")
        {
            if (itemNumber == 4)
            {
                _renderer.color = Color.black; 
                Destroy(col.gameObject);
            }
        }

        if(col.collider.tag=="spring(copy)"){
            _renderer.color =col.gameObject.GetComponent<SpriteRenderer>().color;
            hasPower=true;
            Destroy(col.gameObject);
        }

    }

     void spaw_cloned_items(){
		// Debug.Log(this.transform.localScale.x);
        GameObject obj1= Instantiate(item1Prefab, new Vector3(this.transform.position.x-this.transform.localScale.x+1,this.transform.position.y+this.transform.localScale.y+1,this.transform.position.z), Quaternion.identity);
		obj1.tag="treasure(cloned)";
        obj1.GetComponent<Rigidbody2D>().velocity=new Vector3(-3,3,0);

        GameObject obj2= Instantiate(item2Prefab, new Vector3(this.transform.position.x+this.transform.localScale.x+1,this.transform.position.y+this.transform.localScale.x+1,this.transform.position.z), Quaternion.identity);
        obj2.tag="treasure(cloned)";
		obj2.GetComponent<Rigidbody2D>().velocity=new Vector3(3,3,0);
    }

}
