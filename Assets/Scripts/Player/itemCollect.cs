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
    [SerializeField] private GameObject tool1Obj, tool2Obj, tool3Obj, tool4Obj, tool5Obj, tool6Obj;
    
    public static int itemNumber = 0;
    private SpriteRenderer _renderer;

    // bag varialbes
    bool CanBePick = false;
    public static GameObject onPickObject;
    public static Stack<GameObject> bagStack = new Stack<GameObject> ();
    public static Image tool1;
    public static Image tool2;
    public static Image tool3;
	public static Image tool4;
    public static Image tool5;
    public static Image tool6;

	private Color white = new Color(100,100,100);
	private Color green = new Color(0,255,0);
	// private Color black = new Color(0,0,0);
    
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
            FindObjectOfType<AnalyticsScript>().WrongCollection();
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
        }else{
            FindObjectOfType<AnalyticsScript>().WrongCollection();
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
						if (tool1.sprite == tool4.sprite) {
							tool4.sprite = null;
                    		tool4.color = Color.clear;
						} else if (tool1.sprite == tool5.sprite) {
							tool5.sprite = null;
                    		tool5.color = Color.clear;
						} else if (tool1.sprite == tool6.sprite) {
							tool6.sprite = null;
                    		tool6.color = Color.clear;
						}
                    }
                    else if (tool2.sprite == null)
                    {
                        tool2.sprite = onPickObject.GetComponent<SpriteRenderer>().sprite;
                        tool2.color = onPickObject.GetComponent<SpriteRenderer>().color;
						if (tool2.sprite == tool4.sprite) {
							tool4.sprite = null;
                    		tool4.color = Color.clear;
						} else if (tool2.sprite == tool5.sprite) {
							tool5.sprite = null;
                    		tool5.color = Color.clear;
						} else if (tool2.sprite == tool6.sprite) {
							tool6.sprite = null;
                    		tool6.color = Color.clear;
						}
                    }
                    else
                    {
                        tool3.sprite = onPickObject.GetComponent<SpriteRenderer>().sprite;
                        tool3.color = onPickObject.GetComponent<SpriteRenderer>().color;
						if (tool3.sprite == tool4.sprite) {
							tool4.sprite = null;
                    		tool4.color = Color.clear;
						} else if (tool3.sprite == tool5.sprite) {
							tool5.sprite = null;
                    		tool5.color = Color.clear;
						} else if (tool3.sprite == tool6.sprite) {
							tool6.sprite = null;
                    		tool6.color = Color.clear;
						}
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
					if (tool1.sprite == collItemList[0].GetComponent<SpriteRenderer>().sprite) {
						tool4.sprite = tool1.sprite;
        				tool4.color = tool1.color;
					} else if (tool1.sprite == collItemList[1].GetComponent<SpriteRenderer>().sprite) {
						tool5.sprite = tool1.sprite;
        				tool5.color = tool1.color;
					} else if (tool1.sprite == collItemList[2].GetComponent<SpriteRenderer>().sprite) {
						tool6.sprite = tool1.sprite;
        				tool6.color = tool1.color;
					}
                    tool1.sprite = null;
                    tool1.color = Color.clear;
                }
                else if (item.GetComponent<SpriteRenderer>().sprite == tool2.sprite)
                {
					if (tool2.sprite == collItemList[0].GetComponent<SpriteRenderer>().sprite) {
						tool4.sprite = tool2.sprite;
        				tool4.color = tool2.color;
					} else if (tool2.sprite == collItemList[1].GetComponent<SpriteRenderer>().sprite) {
						tool5.sprite = tool2.sprite;
        				tool5.color = tool2.color;
					} else if (tool2.sprite == collItemList[2].GetComponent<SpriteRenderer>().sprite) {
						tool6.sprite = tool2.sprite;
        				tool6.color = tool2.color;
					}
                    tool2.sprite = null;
                    tool2.color = Color.clear;
                }
                else
                {
					if (tool3.sprite == collItemList[0].GetComponent<SpriteRenderer>().sprite) {
						tool4.sprite = tool3.sprite;
        				tool4.color = tool3.color;
					} else if (tool3.sprite == collItemList[1].GetComponent<SpriteRenderer>().sprite) {
						tool5.sprite = tool3.sprite;
        				tool5.color = tool3.color;
					} else if (tool3.sprite == collItemList[2].GetComponent<SpriteRenderer>().sprite) {
						tool6.sprite = tool3.sprite;
        				tool6.color = tool3.color;
					}
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
		bagStack = new Stack<GameObject> ();
		if (collItemList.Count == 3) {
			tool4.sprite = collItemList[0].GetComponent<SpriteRenderer>().sprite;
        	tool4.color = collItemList[0].GetComponent<SpriteRenderer>().color;
        	tool5.sprite = collItemList[1].GetComponent<SpriteRenderer>().sprite;
        	tool5.color = collItemList[1].GetComponent<SpriteRenderer>().color;
        	tool6.sprite = collItemList[2].GetComponent<SpriteRenderer>().sprite;
			tool6.color = collItemList[2].GetComponent<SpriteRenderer>().color;
		}
        // tool3.color = collItemList[2].GetComponent<SpriteRenderer>().color;
    }

    void Awake()
    {
        tool1 = tool1Obj.GetComponent<Image>();
        tool2 = tool2Obj.GetComponent<Image>();
        tool3 = tool3Obj.GetComponent<Image>();
		tool4 = tool4Obj.GetComponent<Image>();
        tool5 = tool5Obj.GetComponent<Image>();
        tool6 = tool6Obj.GetComponent<Image>();
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
