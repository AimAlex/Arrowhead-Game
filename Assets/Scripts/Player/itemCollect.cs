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
    // [SerializeField] private GameObject tool1Obj, tool2Obj, tool3Obj, tool4Obj, tool5Obj, tool6Obj;
    // private List<GameObject> collItemList=new List<GameObject>();
    private GameObject tool1Obj, tool2Obj, tool3Obj, tool4Obj, tool5Obj, tool6Obj, passLevel, grayMask;
    
    public static int itemNumber = 0;
    private SpriteRenderer _renderer;

    // bag varialbes
    bool CanBePick = false;
    private float timer;
    public static GameObject onPickObject;
    public static Stack<GameObject> bagStack = new Stack<GameObject> ();
    public static Image tool1;
    public static Image tool2;
    public static Image tool3;
	public static Image tool4;
    public static Image tool5;
    public static Image tool6;
    private int player_face = 0; // 0: right, 1: left
    private GameObject popItem;
    private HashSet<GameObject> checkPopList = new HashSet<GameObject>();
    private HashSet<GameObject> needDeleteList = new HashSet<GameObject>();
    private HashSet<GameObject> alreadyTouchWall = new HashSet<GameObject>();
	private Color white = new Color(100,100,100);
	private Color green = new Color(0,255,0);
    private PlayerMovement playerMovement;
    private AudioClip pickAudio, levelSuccessAudio;


    // Start is called before the first frame update
    void Start()
    {

        //Acquire item1-item3, tool1-tool6
        // var item1=GameObject.Find("item1");
        // var item2=GameObject.Find("item2");
        // var item3=GameObject.Find("item3");
        // Debug.Log("item1="+item1.ToString());
        // collItemList.Add(item1);
        // collItemList.Add(item2);
        // collItemList.Add(item3);

        tool1Obj=GameObject.Find("tool1");
        tool2Obj=GameObject.Find("tool2");
        tool3Obj=GameObject.Find("tool3");
        tool4Obj=GameObject.Find("tool4");
        tool5Obj=GameObject.Find("tool5");
        tool6Obj=GameObject.Find("tool6");
        passLevel=GameObject.Find("passLevel");
        grayMask=GameObject.Find("GrayMask");
        
        passLevel.SetActive(false);
        grayMask.SetActive(false);

        timer = float.PositiveInfinity;

        if(tool1Obj!=null){
            tool1 = tool1Obj.GetComponent<Image>();
        }
        if(tool2Obj!=null){
            tool2 = tool2Obj.GetComponent<Image>();
        }
        if(tool3Obj!=null){
            tool3 = tool3Obj.GetComponent<Image>();
        }
        if(tool4Obj!=null){
		    tool4 = tool4Obj.GetComponent<Image>();
        }
        if(tool5Obj!=null){
            tool5 = tool5Obj.GetComponent<Image>();
        }
        if(tool6Obj!=null){
            tool6 = tool6Obj.GetComponent<Image>();
        }

        // Debug.Log("tool1Obj="+tool1Obj.ToString());


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


    private void OnTriggerStay2D(Collider2D col)
    {
        // if (onPickObject != null){
        //     Debug.Log("cur pick item: " + onPickObject.name + " " + CanBePick);
        // }
        if (col.CompareTag("Treasure") && !checkPopList.Contains(col.gameObject))
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
            // Debug.Log(item.name);
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
            playerMovement.isPreview = true;
            playerMovement.audioSource2.volume = 0.5f;
            playerMovement.PlayAudio2(levelSuccessAudio);
            playerMovement.audioSource2.volume = 0.25f;
            FindObjectOfType<AnalyticsScript>().Success();
            
            passLevel.SetActive(true);
            grayMask.SetActive(true);

            GameObject tmp = GameObject.Find("popup");
            if (tmp)
            {
                tmp.SetActive(false);
            }
            
            Time.timeScale = 0f;
            timer = Time.realtimeSinceStartup;
            // StartCoroutine(enterNextLevel());
            // SceneManager.LoadScene(nextSceneName);
        }else{
            if(tool4.color == Color.clear && tool5.color == Color.clear && tool6.color == Color.clear){
                // success, go to next level
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                playerMovement.audioSource2.volume = 0.5f;
                playerMovement.PlayAudio2(levelSuccessAudio);
                playerMovement.audioSource2.volume = 0.25f;
                FindObjectOfType<AnalyticsScript>().Success();
                
                passLevel.SetActive(true);
                grayMask.SetActive(true);
                
                
                
                Time.timeScale = 0f;
                timer = Time.realtimeSinceStartup;
                // StartCoroutine(enterNextLevel());
                // SceneManager.LoadScene(nextSceneName);
            }else{
                FindObjectOfType<AnalyticsScript>().WrongCollection();
                return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Treasure"))
        {
            CanBePick = false;
        }
    }
    private IEnumerator enterNextLevel()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        SceneManager.LoadScene(nextSceneName);
    }
    private void Update()
    {
        // Debug.Log("timer = " + timer);
        // Debug.Log("time = " + Time.realtimeSinceStartup);
        if (Time.realtimeSinceStartup - timer > 5f)
        {
            timer = float.PositiveInfinity;
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextSceneName);
            // Debug.Log("timer = " + timer);
            // Debug.Log("scene name = " + nextSceneName);
        }
        
        if (Input.GetKeyDown(KeyCode.D)){
            player_face = 0;
        } else if (Input.GetKeyDown(KeyCode.A)){
            player_face = 1;
        }
        foreach (var popItem in checkPopList){
            CheckTouchGround(popItem);
        }
        foreach (var deleteItem in needDeleteList){
            checkPopList.Remove(deleteItem);
        }
        needDeleteList.Clear();
        if (CanBePick)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                if (bagStack.Count < 3)
                {
                    playerMovement.PlayAudio(pickAudio);
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
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (itemNumber > 0)
            {
                popItem = bagStack.Pop();
                popItem.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                popItem.transform.position = transform.position + new Vector3(0, 0.4f, 0);
                popItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                popItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                popItem.SetActive(true);
                if (player_face == 0){
                    popItem.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x/3 + 3, gameObject.GetComponent<Rigidbody2D>().velocity.y/3 + 2.5f);
                } else if (player_face == 1){
                    popItem.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x/3 - 3, gameObject.GetComponent<Rigidbody2D>().velocity.y/3 + 2.5f);
                }

                checkPopList.Add(popItem);
           
                --itemNumber;
                if (popItem.GetComponent<SpriteRenderer>().sprite == tool1.sprite)
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
                else if (popItem.GetComponent<SpriteRenderer>().sprite == tool2.sprite)
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

    private void CheckTouchGround(GameObject toolItem)
    {
        
        // if (Physics2D.OverlapCircle(toolItem.transform.GetChild(0).gameObject.transform.position, 0.7f, playerMovement.ground))
        Vector2 checkPos = toolItem.transform.GetChild(0).gameObject.transform.position;
        Vector2 velocity = toolItem.GetComponent<Rigidbody2D>().velocity;
        if (!alreadyTouchWall.Contains(toolItem) && Physics2D.OverlapArea(checkPos, checkPos + new Vector2(0.01f, 1.5f), playerMovement.ground)){  // close to upper ground
            // Debug.Log("touch up wall");
            toolItem.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            alreadyTouchWall.Add(toolItem);
        } else if (!alreadyTouchWall.Contains(toolItem) && Physics2D.OverlapArea(checkPos, checkPos + new Vector2(1f, 0.01f), playerMovement.ground) || Physics2D.OverlapArea(checkPos, checkPos + new Vector2(-1f, 0.01f), playerMovement.ground)){
            // Debug.Log("touch left or right wall");
            if (toolItem.GetComponent<Rigidbody2D>().velocity.y > 0) {
                toolItem.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            } else{
                toolItem.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocity.y);
            }
            alreadyTouchWall.Add(toolItem);
        }
        if (Physics2D.OverlapArea(checkPos, checkPos + new Vector2(0.001f, -0.4f), playerMovement.ground))
        {
            toolItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            needDeleteList.Add(toolItem);
            alreadyTouchWall.Remove(toolItem);
            // Debug.Log("touch ground");
        }
    }



    void Awake()
    {
        // tool1 = tool1Obj.GetComponent<Image>();
        // tool2 = tool2Obj.GetComponent<Image>();
        // tool3 = tool3Obj.GetComponent<Image>();
		// tool4 = tool4Obj.GetComponent<Image>();
        // tool5 = tool5Obj.GetComponent<Image>();
        // tool6 = tool6Obj.GetComponent<Image>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        pickAudio = Resources.Load<AudioClip>("music/pickuptreasure");
        levelSuccessAudio = Resources.Load<AudioClip>("music/small_passlevel");
    }

}
