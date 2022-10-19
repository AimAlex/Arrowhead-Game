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
    public bool camaraMove = false;
    private float originalSize = 9.306593f;
    private Camera mainCamera;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        startPosition = transform.position - player.position;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        // if (tourCamera == null) tourCamera = gameObject.transform;
    }
    private void Start(){
        List<Vector3> tourpoints = new List<Vector3>();
        Debug.Log("start: " + transform.position);
        tourpoints.Add(new Vector3(transform.position.x + 200, transform.position.y + 200, transform.position.z));
        tourpoints.Add(new Vector3(transform.position.x + 400, transform.position.y + 400, transform.position.z));
        tourpoints.Add(new Vector3(transform.position.x + 800, transform.position.y + 800, transform.position.z));
        tourpoints.Add(new Vector3(transform.position.x + 1000, transform.position.y + 1000, transform.position.z));
        tourpoints.Add(new Vector3(transform.position.x + 2000, transform.position.y + 2000, transform.position.z));
        tourpoints.Add(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        CameraTour(tourpoints);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            mainCamera.orthographicSize = 23f;
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            mainCamera.orthographicSize = originalSize;
        }
        // GetDirection();
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
    private void GetDirection(Vector3 direction)
    {
        
        // speedForward = Vector3.zero;
        // speedBack = Vector3.zero;
        // speedLeft = Vector3.zero;
        // speedRight = Vector3.zero;
        // if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.B) || Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)){
        //     camaraMove = false;
        // }
        // if (Input.GetKey(KeyCode.I)){
        //     speedForward = transform.up;
        //     camaraMove = true;
        // }
        // if (Input.GetKey(KeyCode.K)){
        //     speedBack = -transform.up;
        //     camaraMove = true;
        // }
        // if (Input.GetKey(KeyCode.J)){
        //     speedLeft = -transform.right;
        //     camaraMove = true;
        // }
        // if (Input.GetKey(KeyCode.L)){
        //     speedRight = transform.right;
        //     camaraMove = true;
        // }
        // direction = speedForward + speedBack + speedLeft + speedRight;
        direction.x *= 2;
        direction.y *= 2;
        transform.Translate(direction * Time.deltaTime, Space.World);

    }
    public void CameraTour(List<Vector3> tourPoints)
    {
        camaraMove = true;
        for (int i = 0; i < tourPoints.Count; i++)
        {
            transform.position = Vector3.Lerp(transform.position, tourPoints[i], Time.deltaTime);
            Debug.Log(transform.position);
        }
        camaraMove = false;
    }
}
