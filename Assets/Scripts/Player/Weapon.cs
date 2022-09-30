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
    public GameObject ground;

    private void Awake()
    {
        ground = GameObject.Find("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)firePoint.position;
        if(Input.GetButtonDown("Fire1") && laserGunPickUp == true)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, direction);
        if(hitInfo)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
            Collider2D col = hitInfo.collider;
            GameObject item = col.gameObject;
            if(hitInfo.transform.tag == "Treasure")
            {
                item.transform.position = new Vector3(item.transform.position.x, ground.transform.position.y + 1.0f, item.transform.position.z);
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
