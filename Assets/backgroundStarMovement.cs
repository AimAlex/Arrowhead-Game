using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundStarMovement : MonoBehaviour
{
    // [SerializeField] private float verticalY=1, horizontalX=100;
    // [SerializeField] private float speed = 0;
    [SerializeField] public float speedX = 0.35f;
    // [SerializeField] public float speedY = 0.0035f;
    [SerializeField] public float speedY = 0.0035f;
    private Vector2 startPosition=new Vector2(-100,10);
    private Vector2 endPosition=new Vector2(120,-30);
    private int verticalDir = 0;
    private int horizontalDir = 0;
    private float verticalDistance = 0;
    private float horizontalDistance = 0;

    // Start is called before the first frame update
    void Start()
    {
        verticalDir = -1;
        horizontalDir = 1;
        speedY = speedX/100;
        // verticalDistance = verticalY;
        // startPosition = transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(verticalDir * speedY * Time.deltaTime * Vector3.up);
        transform.Translate(horizontalDir * speedX * Time.deltaTime * Vector3.right);
        Debug.Log("y="+transform.position.y.ToString());
        Debug.Log("x="+transform.position.x.ToString());
        Debug.Log("starty="+startPosition.y.ToString());
        Debug.Log("startx="+startPosition.x.ToString());
        if (transform.position.x > endPosition.x) {
            Debug.Log("reach vertical distance goal");
            this.transform.position=startPosition;
        }
        // if (verticalDistance > 0 && startPosition.y-transform.position.y > verticalDistance) {
        //     Debug.Log("reach vertical distance goal");
        //     this.transform.position=startPosition;
        //     verticalY=200;
        //     horizontalX=
        // }
    }
}
