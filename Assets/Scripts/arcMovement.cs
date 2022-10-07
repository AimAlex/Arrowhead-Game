using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arcMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private float radius = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(new Vector3(0,0,0), new Vector3(0, 0, radius),speed*Time.deltaTime);
    }
}
