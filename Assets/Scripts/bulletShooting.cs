using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletShooting : MonoBehaviour
{
    public GameObject prefab_shootItem_left;
    public GameObject prefab_shootItem_right;
    private bool playerClose=false;
    private float shootingRange=230f;
    private float timer;
    private float shotGap=3f;

    void Start(){
        timer=Time.time-shotGap;
        // prefab_shootItem=GameObject.Find("bullet");
        if(prefab_shootItem_left!=null && prefab_shootItem_right != null)
        {
            if(prefab_shootItem_left.GetComponent<PolygonCollider2D>()==null){
                prefab_shootItem_left.AddComponent<PolygonCollider2D>();
            }
            else if (prefab_shootItem_right.GetComponent<PolygonCollider2D>() == null)
            {
                prefab_shootItem_right.AddComponent<PolygonCollider2D>();
            }
        }
        // ShootItem();
    }

    void Update(){
       var dir= new Vector3(FindObjectOfType<PlayerMovement>().rigidbody.position.x,FindObjectOfType<PlayerMovement>().rigidbody.position.y,0)-this.transform.position;
    //    Debug.Log(dir.x*dir.x+dir.y*dir.y+dir.z*dir.z);
       if(dir.x*dir.x+dir.y*dir.y+dir.z*dir.z < shootingRange/2){
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
        if (FindObjectOfType<PlayerMovement>().rigidbody.position.x - this.transform.position.x <=0)
        {
            GameObject shotItem = Instantiate(prefab_shootItem_left);
            shotItem.transform.tag = "bullet";
        }
        else
        {
            GameObject shotItem = Instantiate(prefab_shootItem_right);
            shotItem.transform.tag = "bullet";
        }


    /*    prefab_shootItem.SetActive(true);*/
        // GameObject shotItem=Instantiate(prefab_shootItem,new Vector3(transform.position.x,transform.position.y,transform.position.z));
        // shotItem.GetComponent<ShootItem>().
    }
}
