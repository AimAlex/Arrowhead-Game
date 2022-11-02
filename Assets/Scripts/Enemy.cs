using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float vertical, horizontal;
    // [SerializeField] private float speed = 0.5f;
    [SerializeField] public float speed = 0.5f;
    private Vector2 startPosition;
    private int verticalDir = 0;
    private int horizontalDir = 0;
    private float verticalDistance = 0;
    private float horizontalDistance = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (vertical > 0)
        {
            verticalDir = 1;
            verticalDistance = vertical;
        }
        else if (vertical < 0)
        {
            verticalDir = -1;
            verticalDistance = -vertical;
        }

        if (horizontal > 0)
        {
            horizontalDir = 1;
            horizontalDistance = horizontal;
        }
        else if (horizontal < 0)
        {
            horizontalDir = -1;
            horizontalDistance = -horizontal;
        }
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update ()
    {
        if (verticalDistance > 0 && transform.position.y - startPosition.y > verticalDistance) {
            verticalDir = -1;
        }
        else if (verticalDistance > 0 && startPosition.y - transform.position.y > verticalDistance) {
            verticalDir = 1;
        }

        if (horizontalDistance > 0 && transform.position.x - startPosition.x > horizontalDistance)
        {
            horizontalDir = -1;
        }
        else if (horizontalDistance > 0 && startPosition.x - transform.position.x > horizontalDistance)
        {
            horizontalDir = 1;
        }
        
        transform.Translate(verticalDir * speed * Time.deltaTime * Vector3.up);
        transform.Translate(horizontalDir * speed * Time.deltaTime * Vector3.right);
    }
}
