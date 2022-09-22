using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Object = UnityEngine.Object;

public class itemCollect : MonoBehaviour
{
    [SerializeField] private List<GameObject> collItemList;
    [SerializeField] private string nextSceneName;
    
    private int itemNumber = 0;
    private SpriteRenderer _renderer;

    // bag varialbes
    bool CanBePick = false;
    private GameObject onPickObject;
    Queue<GameObject> bagQueue=new Queue<GameObject> ();
    public Image tool1;
    public Image tool2;
    public Image tool3;
    

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
            CanBePick = true;
            onPickObject = col.gameObject;
        }

        if (col.CompareTag("Finish"))
        {

            checkFinish();

            //level1_1 move to level1_2
            if(col.name == "level1_1_des")
            {
                // transform.position = new Vector3(34f,-6f,0f);
                SceneManager.LoadScene("level1_2");
            }
            // level1_2 move to level1_3
            else if(col.name == "level1_2_des")
            {
                // transform.position = new Vector3(128.7f,-5f,0f);
                SceneManager.LoadScene("level1_3");
            }
            // final destination
            else if(col.name == "level1_3_des")
            {
                SceneManager.LoadScene("level2");
            }
        }

        // if(col.CompareTag("trap")){
        //     FindObjectOfType<AnalyticsScript>().KillByTrap();
        // }
    }
    
    private void checkFinish()
    {
        List<GameObject> collList = new List<GameObject> ();
        foreach (var obj in bagQueue)
        {
            collList.Add(obj);
        }

        if (collList.Count != collItemList.Count)
        {
            return;
        }

        foreach (var item in collItemList) 
        {
            if (collList.Exists(t => t == item))
            {
                collList.Remove(item);
            }
            else
            {
                break;
            }
        }

        if (collList.Count == 0)
        {
            // success, go to next level
            FindObjectOfType<AnalyticsScript>().Success();
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Treasure"))
        {
            CanBePick = false;
        }
    }

    private void Update()
    {
        if (CanBePick)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ++itemNumber;
                onPickObject.SetActive(false);
                bagQueue.Enqueue(onPickObject);
                if (bagQueue.Count == 1)
                {
                    tool1.sprite = onPickObject.GetComponent<SpriteRenderer>().sprite;
                    tool1.color = onPickObject.GetComponent<SpriteRenderer>().color;
                }
                else if (bagQueue.Count == 2)
                {
                    tool2.sprite = onPickObject.GetComponent<SpriteRenderer>().sprite;
                    tool2.color = onPickObject.GetComponent<SpriteRenderer>().color;

                }
                else if (bagQueue.Count == 3)
                {
                    tool3.sprite = onPickObject.GetComponent<SpriteRenderer>().sprite;
                    tool3.color = onPickObject.GetComponent<SpriteRenderer>().color;

                }
                //Analytics codes
                // if(itemNumber==1){
                //     FindObjectOfType<AnalyticsScript>().Collect1();
                // }else if(itemNumber==2){
                //     FindObjectOfType<AnalyticsScript>().Collect2();
                // }else if(itemNumber==3){
                //     FindObjectOfType<AnalyticsScript>().Collect3();
                // }else if(itemNumber==4){
                //     FindObjectOfType<AnalyticsScript>().Collect4();
                // }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemNumber > 0)
            {
                GameObject item = bagQueue.Dequeue();
                item.SetActive(true);
                --itemNumber;
                if (bagQueue.Count == 0)
                {
                    tool1.color = Color.clear;
                }
                else if (bagQueue.Count == 1)
                {
                    tool2.color = Color.clear;
                }
                else if (bagQueue.Count == 2)
                {
                    tool3.color = Color.clear;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Awake()
    {
        // tool1 = GameObject.Find("tool1").GetComponent<Image>();
        // tool2 = GameObject.Find("tool2").GetComponent<Image>();
        // tool3 = GameObject.Find("tool3").GetComponent<Image>();
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
