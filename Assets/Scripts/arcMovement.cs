using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arcMovement : MonoBehaviour
{
    // [SerializeField] private float speed = 0;
    [SerializeField] public float speed = 8;
    public float radiusX = 0;
    public float radiusY = 0;
    public float radiusZ = 1;
    public float rotateCenterX=0;
    public float rotateCenterY=0;
    public float rotateCenterZ=0;
    // Start is called before the first frame update
    void Start()
    {
    //  rotateCenterX=this.transform.position.x;   
    //  rotateCenterY=this.transform.position.y;   
    //  rotateCenterZ=this.transform.position.z;   
    }
    // Update is called once per frame
    void Update()
    {
        // transform.RotateAround(this.transform.position, new Vector3(0, 0, radius),speed*Time.deltaTime);
        transform.RotateAround(new Vector3(rotateCenterX,rotateCenterY,rotateCenterZ), new Vector3(radiusX, radiusY, radiusZ),speed*Time.deltaTime);
    }
}
