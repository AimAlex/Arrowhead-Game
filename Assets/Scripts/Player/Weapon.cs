using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public itemCollect items;
    public Transform firePoint;
    // public GameObject bulletPrefab;
    public LineRenderer lineRenderer;
    public bool laserGunPickUp;
    public Vector2 direction;
    // public GameObject ground;

    private void Awake()
    {
        // ground = GameObject.Find("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)firePoint.position;
        if(Input.GetKeyDown(KeyCode.F) && laserGunPickUp == true)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, direction);
        if(hitInfo && hitInfo.transform.tag != "Finish")
        {
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
                    Debug.Log("Items Are Full");
                }
            }
            else if(hitInfo.transform.tag == "Enemy")
            {
                Debug.Log("Hit Enemy");
                Destroy(item);
            }
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);           
        }

        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.02f);
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
