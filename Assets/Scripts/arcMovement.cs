using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arcMovement : MonoBehaviour
{
    // [SerializeField] private float speed = 0;
    [SerializeField] public float speed = 0;
    private float radius = 1;
    public float rotateCenterX=0;
    public float rotateCenterY=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        // transform.RotateAround(this.transform.position, new Vector3(0, 0, radius),speed*Time.deltaTime);
        transform.RotateAround(new Vector3(rotateCenterX,rotateCenterY,0), new Vector3(0, 0, radius),speed*Time.deltaTime);
    }
}
