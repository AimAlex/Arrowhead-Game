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
    private int player_face = 0; // 0: right, 1: left

    private HashSet<GameObject> checkPopList = new HashSet<GameObject>();
    private HashSet<GameObject> needDeleteList = new HashSet<GameObject>();

	private Color white = new Color(100,100,100);
	private Color green = new Color(0,255,0);
    private PlayerMovement playerMovement;
    private AudioClip pickAudio, levelSuccessAudio;
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
            playerMovement.isPreview = true;
            playerMovement.audioSource2.volume = 0.5f;
            playerMovement.PlayAudio2(levelSuccessAudio);
            playerMovement.audioSource2.volume = 0.25f;
            FindObjectOfType<AnalyticsScript>().Success();
            StartCoroutine(enterNextLevel());
            // SceneManager.LoadScene(nextSceneName);
        }else{
            if(tool4.color == Color.clear && tool5.color == Color.clear && tool6.color == Color.clear){
                // success, go to next level
                playerMovement.isPreview = true;
                playerMovement.audioSource2.volume = 0.5f;
                playerMovement.PlayAudio2(levelSuccessAudio);
                playerMovement.audioSource2.volume = 0.25f;
                FindObjectOfType<AnalyticsScript>().Success();
                StartCoroutine(enterNextLevel());
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
        yield return new WaitForSeconds(0.3f);
        playerMovement.isPreview = false;
        SceneManager.LoadScene(nextSceneName);
    }
    private void Update()
    {   
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
                GameObject item = bagStack.Pop();
                item.transform.position = transform.position + new Vector3(0, 0.8f, 0);
                item.GetComponent<BoxCollider2D>().isTrigger = false;
                item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                item.SetActive(true);
                if (player_face == 0){
                    item.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x + 3, gameObject.GetComponent<Rigidbody2D>().velocity.y + 2);
                } else if (player_face == 1){
                    item.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x - 3, gameObject.GetComponent<Rigidbody2D>().velocity.y + 2);
                }

                checkPopList.Add(item);
           
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

    private void CheckTouchGround(GameObject toolItem)
    {
        Debug.Log(Physics2D.OverlapCircle(toolItem.transform.GetChild(0).gameObject.transform.position, 0.7f, playerMovement.ground));
        if (Physics2D.OverlapCircle(toolItem.transform.GetChild(0).gameObject.transform.position, 0.7f, playerMovement.ground))
        {
            toolItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            toolItem.GetComponent<BoxCollider2D>().isTrigger = true;
            needDeleteList.Add(toolItem);
            Debug.Log("touch ground");
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
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        pickAudio = Resources.Load<AudioClip>("music/pickuptreasure");
        levelSuccessAudio = Resources.Load<AudioClip>("music/small_passlevel");
    }

}
