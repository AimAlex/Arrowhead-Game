using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float horizontal;
    [SerializeField] public float speed = 0;
    private Vector2 startPosition;
    private int horizontalDir = 0;
    private float horizontalDistance = 0;
    void Start()
    {
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
    void Update()
    {
        if (horizontalDistance > 0 && transform.position.x - startPosition.x > horizontalDistance)
        {
            horizontalDir = -1;
        }
        else if (horizontalDistance > 0 && startPosition.x - transform.position.x > horizontalDistance)
        {
            horizontalDir = 1;
        }
        
        transform.Translate(horizontalDir * speed * Time.deltaTime * Vector3.right);
    }
}
