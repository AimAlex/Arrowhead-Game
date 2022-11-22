using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public itemCollect items;
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public static bool laserGunPickUp;
    public Vector2 direction;
    RaycastHit2D hitInfo;
	AudioClip shootAudio;
    private PlayerMovement playerMovement;
    public Vector2 playerVelocity;
    private void Awake()
    {
        laserGunPickUp = false;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
		shootAudio = Resources.Load<AudioClip>("music/laser-shoot1");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        playerVelocity = FindObjectOfType<PlayerMovement>().rigidbody.velocity;
        if (playerVelocity.x > 0)
        {
            direction = firePoint.right;
        }
        else if(playerVelocity.x < 0)
        {
            direction = -firePoint.right;
        }
        else
        {
            direction = playerVelocity;
            Debug.Log(playerVelocity.x);
            Debug.Log(firePoint.position.y);
        }
        */
        direction = firePoint.right;
        if(Input.GetKeyDown(KeyCode.J) && laserGunPickUp == true)
        {
            StartCoroutine(Shoot());
            playerMovement.PlayAudio(shootAudio);
        }
    }

    IEnumerator Shoot()
    {
        // modified codes
        RaycastHit2D[] hitInfos = Physics2D.RaycastAll(firePoint.position, direction);
        if(hitInfos.Length==0 || (hitInfos.Length==1 && hitInfos[0].transform.tag=="Player")){
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + (Vector3)direction*100); 
        }else{

            if(hitInfos[0].transform.tag=="Player"){
                hitInfo=hitInfos[1];
            }else{
                hitInfo=hitInfos[0];
            }
        


            if(hitInfo)
            {
            // if(hitInfo && hitInfo.transform.tag != "Finish")
            // {
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point);
                Collider2D col = hitInfo.collider;
                GameObject item = col.gameObject;
                if(hitInfo.transform.tag == "Treasure")
                {
                    // item.transform.position = new Vector3(item.transform.position.x, ground.transform.position.y + 1.0f, item.transform.position.z);
                    if(itemCollect.bagStack.Count < 3)
                    {
                        itemCollect.itemNumber = itemCollect.itemNumber + 1;
                        itemCollect.onPickObject = item;
                        itemCollect.onPickObject.SetActive(false);
                        itemCollect.bagStack.Push(itemCollect.onPickObject);
                        if (itemCollect.tool1.sprite == null)
                        {
                            itemCollect.tool1.sprite = itemCollect.onPickObject.GetComponent<SpriteRenderer>().sprite;
                            itemCollect.tool1.color = itemCollect.onPickObject.GetComponent<SpriteRenderer>().color;
                            if (itemCollect.tool1.sprite == itemCollect.tool4.sprite) {
                                itemCollect.tool4.sprite = null;
                                itemCollect.tool4.color = Color.clear;
                            } else if (itemCollect.tool1.sprite == itemCollect.tool5.sprite) {
                                itemCollect.tool5.sprite = null;
                                itemCollect.tool5.color = Color.clear;
                            } else if (itemCollect.tool1.sprite == itemCollect.tool6.sprite) {
                                itemCollect.tool6.sprite = null;
                                itemCollect.tool6.color = Color.clear;
                            }
                        }
                        else if (itemCollect.tool2.sprite == null)
                        {
                            itemCollect.tool2.sprite = itemCollect.onPickObject.GetComponent<SpriteRenderer>().sprite;
                            itemCollect.tool2.color = itemCollect.onPickObject.GetComponent<SpriteRenderer>().color;
                            if (itemCollect.tool2.sprite == itemCollect.tool4.sprite) {
                                itemCollect.tool4.sprite = null;
                                itemCollect.tool4.color = Color.clear;
                            } else if (itemCollect.tool2.sprite == itemCollect.tool5.sprite) {
                                itemCollect.tool5.sprite = null;
                                itemCollect.tool5.color = Color.clear;
                            } else if (itemCollect.tool2.sprite == itemCollect.tool6.sprite) {
                                itemCollect.tool6.sprite = null;
                                itemCollect.tool6.color = Color.clear;
                            }
                        }
                        else
                        {
                            itemCollect.tool3.sprite = itemCollect.onPickObject.GetComponent<SpriteRenderer>().sprite;
                            itemCollect.tool3.color = itemCollect.onPickObject.GetComponent<SpriteRenderer>().color;
                            if (itemCollect.tool3.sprite == itemCollect.tool4.sprite) {
                                itemCollect.tool4.sprite = null;
                                itemCollect.tool4.color = Color.clear;
                            } else if (itemCollect.tool3.sprite == itemCollect.tool5.sprite) {
                                itemCollect.tool5.sprite = null;
                                itemCollect.tool5.color = Color.clear;
                            } else if (itemCollect.tool3.sprite == itemCollect.tool6.sprite) {
                                itemCollect.tool6.sprite = null;
                                itemCollect.tool6.color = Color.clear;
                            }
                        }
                    }
                    else
                    {
                        // Debug.Log("Items Are Full");
                    }
                }
                else if(hitInfo.transform.tag == "Enemy")
                {
                    // Debug.Log("Hit Enemy");
                    Destroy(item);
                }
                else if(hitInfo.transform.tag == "Boss")
                {
                    // Debug.Log("Hit Boss");
                    bool isStillAlive = FindObjectOfType<bossBar>().UpdateHurt();
                    if (!isStillAlive)
                    {
                        Destroy(item);
                    }
                        
                }
            }else{
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, firePoint.position + (Vector3)direction*100); 
            }

        }

        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.05f);
        lineRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Booster"))
        {
            if(col.name == "laserGunItem")
            {
                laserGunPickUp = true;
                Destroy(col.gameObject);
            } 
        }
    }
}
