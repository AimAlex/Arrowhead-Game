using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletShooting : MonoBehaviour
{
    public GameObject prefab_shootItem;
    private bool playerClose=false;
    private float shootingRange=230f;
    private float timer;
    private float shotGap=1.5f;

    void Start(){
        timer=Time.time-shotGap;
        // prefab_shootItem=GameObject.Find("bullet");
        if(prefab_shootItem!=null){
            if(prefab_shootItem.GetComponent<PolygonCollider2D>()==null){
                prefab_shootItem.AddComponent<PolygonCollider2D>();
            }
        }
        // ShootItem();
    }

    void Update(){
       var dir= new Vector3(FindObjectOfType<PlayerMovement>().rigidbody.position.x,FindObjectOfType<PlayerMovement>().rigidbody.position.y,0)-this.transform.position;
    //    Debug.Log(dir.x*dir.x+dir.y*dir.y+dir.z*dir.z);
       if(dir.x*dir.x+dir.y*dir.y+dir.z*dir.z < shootingRange){
            playerClose=true;
       }else{
            playerClose=false;
       }
       if(Time.time-timer>shotGap && playerClose){
            ShootItem();
            timer=Time.time;
       }
    }

    private void ShootItem(){
        // Debug.Log("shoot item");
        // prefab_shootItem=GameObject.Find("bullet");
        GameObject shotItem=Instantiate(prefab_shootItem);
        shotItem.transform.tag = "bullet";

    /*    prefab_shootItem.SetActive(true);*/
        // GameObject shotItem=Instantiate(prefab_shootItem,new Vector3(transform.position.x,transform.position.y,transform.position.z));
        // shotItem.GetComponent<ShootItem>().
    }
}
