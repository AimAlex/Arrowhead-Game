using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform firePoint;

    public LineRenderer lineRenderer;

    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        var random = Random.Range(0f, 260f);
        Vector2 randomVector = Random.insideUnitCircle;
        direction = randomVector;
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        RaycastHit2D[] hitInfos = Physics2D.RaycastAll(firePoint.position, direction);
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, direction*100);
        foreach(var hitInfo in hitInfos)
        {
            if (hitInfo.transform.tag == "Player")
            {
                PlayerLife.Die();
            }
        }
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(5);
        lineRenderer.enabled = false;
    }
}
