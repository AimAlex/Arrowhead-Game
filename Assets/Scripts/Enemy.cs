using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Search;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform firePoint;

    public LineRenderer lineRenderer;

    public Vector2 direction;

    private RaycastHit2D hitInfo;

    private float time;

    private float timeDelay;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        timeDelay = 2f;
    }

    // Update is called once per frame
    void Update ()
    {
        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay)
        {
            time = 0f;
            var random = Random.Range(0f, 260f);
            Vector2 randomVector = Random.insideUnitCircle;
            direction = randomVector;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        RaycastHit2D[] hitInfos = Physics2D.RaycastAll(firePoint.position, direction);
        if (hitInfos.Length == 0 || (hitInfos.Length==1 && hitInfos[0].transform.tag == "Enemy"))
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, direction*100); 
        }
        else
        {
            if (hitInfos[0].transform.tag == "Enemy")
            {
                hitInfo = hitInfos[1];
            }
            else
            {
                hitInfo = hitInfos[0];
            }

            if (hitInfo)
            {
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point);
                if(!FindObjectOfType<healthPoint>().UpdateHurt()){
                    PlayerLife.Die();
                }
            }
            else
            {
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, direction*100);
            }
        }

        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.05f);
        lineRenderer.enabled = false;
    }
}
