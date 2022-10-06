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
    
    public static int itemNumber = 0;
    private SpriteRenderer _renderer;

    // bag varialbes
    bool CanBePick = false;
    public static GameObject onPickObject;
    public static Stack<GameObject> bagStack=new Stack<GameObject> ();
    public static Image tool1;
    public static Image tool2;
    public static Image tool3;
    
    // // Level 3 variables; For copy power
    // public GameObject cloned1;
    // public GameObject cloned2;
    // public GameObject cloned3;
    // public GameObject cloned2_2;
    // public GameObject cloned3_2;
    // public GameObject cloned3_3;
    // public GameObject item14;
    // // public GameObject item4Prefab;


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
            // if(col.name == "level1_1_des")
            // {
            //     // transform.position = new Vector3(34f,-6f,0f);
            //     SceneManager.LoadScene("level1_2");
            // }
            // // level1_2 move to level1_3
            // else if(col.name == "level1_2_des")
            // {
            //     // transform.position = new Vector3(128.7f,-5f,0f);
            //     SceneManager.LoadScene("level1_3");
            // }
            // // final destination
            // else if(col.name == "level1_3_des")
            // {
            //     SceneManager.LoadScene("level2-1");
            // }
        }

    }
    
    private void checkFinish()
    {
        List<GameObject> collList = new List<GameObject> ();
        foreach (var obj in bagStack)
        {
            collList.Add(obj);
        }

        if (collList.Count != collItemList.Count)
        {
            return;
        }

        foreach (var item in collItemList) 
        {
            Debug.Log(item.name);
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
                if (bagStack.Count < 3)
                {

                    ++itemNumber;
                    onPickObject.SetActive(false);
                    bagStack.Push(onPickObject);
                    if (tool1.sprite == null)
                    {
                        tool1.sprite = onPickObject.GetComponent<SpriteRenderer>().sprite;
                        tool1.color = onPickObject.GetComponent<SpriteRenderer>().color;
                    }
                    else if (tool2.sprite == null)
                    {
                        tool2.sprite = onPickObject.GetComponent<SpriteRenderer>().sprite;
                        tool2.color = onPickObject.GetComponent<SpriteRenderer>().color;

                    }
                    else
                    {
                        tool3.sprite = onPickObject.GetComponent<SpriteRenderer>().sprite;
                        tool3.color = onPickObject.GetComponent<SpriteRenderer>().color;
                    }


                    // //level 3. For copy power
                    // if(FindObjectOfType<PlayerMovement>().collectCopy){
                    //     if(onPickObject.name=="item1"){
                    //         spawn_cloned_items1();
                    //     }else if(onPickObject.name=="item2"){
                    //         spawn_cloned_items2();
                    //     }else if(onPickObject.name=="item3"){
                    //         spawn_cloned_items3();
                    //     }else if(onPickObject.name=="item11"){
                    //         spawn_cloned_items11();
                    //     }
                    // }

                }
                // else
                // {
                //     // GameObject.Find("info").GetComponent<InfoShow>().showInfo("You Bag is full!");
                // }
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
                GameObject item = bagStack.Pop();
                item.SetActive(true);
                --itemNumber;
                if (item.GetComponent<SpriteRenderer>().sprite == tool1.sprite)
                {
                    tool1.sprite = null;
                    tool1.color = Color.clear;
                }
                else if (item.GetComponent<SpriteRenderer>().sprite == tool2.sprite)
                {
                    tool2.sprite = null;
                    tool2.color = Color.clear;
                }
                else
                {
                    tool3.sprite = null;
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
        tool1 = GameObject.Find("tool1").GetComponent<Image>();
        tool2 = GameObject.Find("tool2").GetComponent<Image>();
        tool3 = GameObject.Find("tool3").GetComponent<Image>();
    }

    // // Level 3 codes; For copy power 
    //  private void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col.collider.tag=="Treasure")
    //     {
    //         ++itemNumber;

    //         // if(hasPower){
    //         //     spaw_cloned_items();
    //         // }

    //         Destroy(col.gameObject);

    //     }

    //     if (col.collider.tag=="treasure(cloned)")
    //     {
    //         ++itemNumber;
    //         Destroy(col.gameObject);
    //     }


    //     if (col.collider.tag=="Finish")
    //     {
    //         if (itemNumber == 4)
    //         {
    //             _renderer.color = Color.black; 
    //             Destroy(col.gameObject);
    //         }
    //     }

    //     // if(col.collider.tag=="spring(copy)"){
    //     //     _renderer.color =col.gameObject.GetComponent<SpriteRenderer>().color;
    //     //     // hasPower=true;
    //     //     Destroy(col.gameObject);
    //     // }

    // }

    //  void spawn_cloned_items1(){
    //     cloned3.transform.position=new Vector3(this.transform.position.x-(this.transform.localScale.x+3),this.transform.position.y+this.transform.localScale.y+1,this.transform.position.z);
    //     cloned3.AddComponent<Rigidbody2D>();
    //     cloned3.GetComponent<Rigidbody2D>().velocity=new Vector3(-1,0,0);

    //     cloned1.transform.position=new Vector3(this.transform.position.x+this.transform.localScale.x+1,this.transform.position.y+this.transform.localScale.y+1,this.transform.position.z);
    //     cloned1.AddComponent<Rigidbody2D>();
    //     cloned1.GetComponent<Rigidbody2D>().velocity=new Vector3(4,2,0);
    // }

    // void spawn_cloned_items2(){
    //     cloned2.transform.position=new Vector3(this.transform.position.x-(this.transform.localScale.x+3),this.transform.position.y+this.transform.localScale.y+1,this.transform.position.z);
    //     cloned2.AddComponent<Rigidbody2D>();
    //     cloned2.GetComponent<Rigidbody2D>().velocity=new Vector3(-2,0,0);

    //     cloned3_3.transform.position=new Vector3(this.transform.position.x+this.transform.localScale.x+1,this.transform.position.y+this.transform.localScale.y+1,this.transform.position.z);
    //     cloned3_3.AddComponent<Rigidbody2D>();
    //     cloned3_3.GetComponent<Rigidbody2D>().velocity=new Vector3(3,2,0);
    // }


    // void spawn_cloned_items3(){
    //     cloned2_2.transform.position=new Vector3(this.transform.position.x-(this.transform.localScale.x+3),this.transform.position.y+this.transform.localScale.y+1,this.transform.position.z);
    //     cloned2_2.AddComponent<Rigidbody2D>();
    //     cloned2_2.GetComponent<Rigidbody2D>().velocity=new Vector3(-3,0,0);

    //     cloned3_2.transform.position=new Vector3(this.transform.position.x+this.transform.localScale.x+1,this.transform.position.y+this.transform.localScale.y+1,this.transform.position.z);
    //     cloned3_2.AddComponent<Rigidbody2D>();
    //     cloned3_2.GetComponent<Rigidbody2D>().velocity=new Vector3(2,2,0);
    // }

    // void spawn_cloned_items11(){
    //     item14.transform.position=new Vector3(this.transform.position.x-(this.transform.localScale.x+3),this.transform.position.y+this.transform.localScale.y+1,this.transform.position.z);
    //     item14.AddComponent<Rigidbody2D>();
    //     item14.GetComponent<Rigidbody2D>().velocity=new Vector3(-3,0,0);
    // }



}
