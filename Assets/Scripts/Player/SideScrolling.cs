using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    public Transform tourCamera;
    private Transform player;
    private Vector3 startPosition;
    [SerializeField] private float smoothing;
    private Vector3 direction = Vector3.zero;
    private Vector3 speedForward;
    private Vector3 speedBack;
    private Vector3 speedLeft;
    private Vector3 speedRight;
    private bool camaraMove = false;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        startPosition = transform.position - player.position;
        // if (tourCamera == null) tourCamera = gameObject.transform;
    }
    private void Update()
    {
        GetDirection();
        direction.x *= 2;
        direction.y *= 2;
        transform.Translate(direction * Time.deltaTime, Space.World);
    }
    private void LateUpdate()
    {
        if (!camaraMove)
        {
            InitCamera();
        }
    }
    private void InitCamera(){
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = player.position.x;
        //cameraPosition.y = player.position.y + startPosition.y;
        //cameraPosition.y = player.position.y;
        cameraPosition.y = player.position.y + 3;
        if (cameraPosition != transform.position)
        {
            transform.position = cameraPosition;
        }
    }
    private void GetDirection()
    {
        speedForward = Vector3.zero;
        speedBack = Vector3.zero;
        speedLeft = Vector3.zero;
        speedRight = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
            camaraMove = false;
        }
        if (Input.GetKey(KeyCode.I)){
            speedForward = transform.up;
            camaraMove = true;
        }
        if (Input.GetKey(KeyCode.K)){
            speedBack = -transform.up;
            camaraMove = true;
        }
        if (Input.GetKey(KeyCode.J)){
            speedLeft = -transform.right;
            camaraMove = true;
        }
        if (Input.GetKey(KeyCode.L)){
            speedRight = transform.right;
            camaraMove = true;
        }
        direction = speedForward + speedBack + speedLeft + speedRight;
    }
}
