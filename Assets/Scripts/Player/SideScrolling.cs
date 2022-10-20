using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SideScrolling : MonoBehaviour
{
    public Transform tourCamera;
    private Transform player;
    private Vector3 startPosition, initPosition;
    [SerializeField] private float smoothing;
    private Vector3 direction = Vector3.zero;
    private Vector3 speedForward;
    private Vector3 speedBack;
    private Vector3 speedLeft;
    private Vector3 speedRight;
    public bool camaraMove, inFrame1, inFrame2, inPreviewMode, inCameraResume;
    private float originalSize = 9.306593f;
    private Camera mainCamera;
    private int pathIndex;
    private bool[] pathPiontCheck;
    private List<Vector3> pathPoints;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        startPosition = transform.position - player.position;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        // tourPosition variable, when use the tour camera, must uncomment next line
        // camaraMove = true;
        smoothing = 1.5f;
        pathIndex = 0;
    }
    private void Start(){
        InitCamera();
        initPosition = transform.position;
        pathPoints = new List<Vector3>();
        // demo camera tour code
        pathPoints.Add(new Vector3(initPosition.x + 15, initPosition.y, transform.position.z));
        pathPoints.Add(new Vector3(initPosition.x + 15, initPosition.y + 10, transform.position.z));
        pathPoints.Add(new Vector3(initPosition.x + 30, initPosition.y + 5, transform.position.z));
        pathPoints.Add(initPosition);
    }

    private void tourPosition(List<Vector3> path){
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)){
            pathIndex = path.Count;
        }
        if (pathIndex < path.Count){
            transform.position = Vector3.Lerp(transform.position, path[pathIndex], Time.deltaTime);
            // If need different waitting time for each point, please chage the float value, such as 0.5f
            if (Mathf.Abs(transform.position.x - path[pathIndex].x) <= 0.5f && Mathf.Abs(transform.position.y - path[pathIndex].y) <= 0.5f){
                pathIndex++;
            }
        }
        else if (pathIndex == path.Count){
            camaraMove = false;
        }
    }

    private void Update()
    {
        // tourPosition(pathPoints);
        if (Input.GetKeyDown(KeyCode.P))
        {
            inPreviewMode = true;
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            inPreviewMode = false;
        }
        if (inPreviewMode){
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 23f, Time.deltaTime);
        }
        else if (!inPreviewMode){
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, originalSize, Time.deltaTime);
        }
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
        speedForward = Vector3.zero;
        speedBack = Vector3.zero;
        speedLeft = Vector3.zero;
        speedRight = Vector3.zero;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.B) || Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)){
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
        direction.x *= 2;
        direction.y *= 2;
        transform.Translate(direction * Time.deltaTime, Space.World);
    }
}
