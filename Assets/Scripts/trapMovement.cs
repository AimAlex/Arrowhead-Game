using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapMovement : MonoBehaviour
{


    [SerializeField]private float upLimit;
    [SerializeField] private float downLimit;
    private float speed = 2f;
    private int direction = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
 
    void Update () {
        if (transform.position.y > upLimit) {
            direction = -1;
        }
        else if (transform.position.y < downLimit) {
            direction = 1;
        }
        // movement = 
        transform.Translate(Vector3.up * direction * speed * Time.deltaTime); 
  }
}
