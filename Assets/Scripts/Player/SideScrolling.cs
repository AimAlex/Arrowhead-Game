using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SideScrolling : MonoBehaviour
{
    public Transform tourCamera;
    private Transform player;
    private Vector3 startPosition, initPosition;
    // [SerializeField] private float smoothing;
    private Vector3 direction = Vector3.zero;
    private Vector3 speedForward;
    private Vector3 speedBack;
    private Vector3 speedLeft;
    private Vector3 speedRight;
    private Vector3 playerOriginalPosition;
    public bool camaraMove, inPreviewMode, inExitPreviewMode;
    public GameObject previewObject;
    private float originalSize = 9.306593f;
    private Camera mainCamera;
    public int pathIndex;
    private bool[] pathPiontCheck;
    public float cameraPreviewSize = 18f;
    public List<GameObject> pathPoints;
    public List<float> pathZoom;
    public List<float> pathSpeed;
    private List<Vector3> tourList;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        startPosition = transform.position - player.position;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        inExitPreviewMode = false;
        // tourPosition variable, when use the tour camera, must uncomment next line
        // camaraMove = true;
        // smoothing = 1.5f;
        pathIndex = 0;
    }
    private void Start(){
        InitCamera();
        initPosition = transform.position;
        // InitCameraTour();
        // demo camera tour code
        // pathPoints = new List<Vector3>();
        // pathPoints.Add(new Vector3(initPosition.x + 15, initPosition.y, transform.position.z));
        // pathPoints.Add(new Vector3(initPosition.x + 15, initPosition.y + 10, transform.position.z));
        // pathPoints.Add(new Vector3(initPosition.x + 30, initPosition.y + 5, transform.position.z));
        // pathPoints.Add(initPosition);
        // pathZoom = new List<float>();
        // pathZoom.Add(23.0f);
        // pathZoom.Add(3.0f);
        // pathZoom.Add(1.0f);
        // pathZoom.Add(9.306593f);
        // pathSpeed = new List<float>();
        // pathSpeed.Add(0.5f);
        // pathSpeed.Add(0.6f);
        // pathSpeed.Add(0.8f);
        // pathSpeed.Add(1f);
    }

    private void InitCameraTour()
    {
        tourList = new List<Vector3>();
        for (int i = 0; i < pathPoints.Count; ++i)
        {
            tourList.Add(pathPoints[i].transform.position);
            if (i >= pathZoom.Count)
            {
                pathZoom.Add(20f);
            }

            if (i >= pathSpeed.Count)
            {
                pathSpeed.Add(1);
            }
        }

        tourList.Add(initPosition);
        pathZoom.Add(originalSize);
        pathSpeed.Add(3);
    }

    private void startCameraTour()
    {
        if (pathPoints.Count == 0)
        {
            return;
        }
        camaraMove = true;

        if (!tourPosition(tourList, pathZoom, pathSpeed))
        {
            pathPoints.Clear();
        }
    }

    public bool tourPosition(List<Vector3> path, List<float> zoom, List<float> speed){
        // if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)){
        //     pathIndex = path.Count;
        // }
        if (pathIndex < path.Count){
            transform.position = Vector3.Lerp(transform.position, new Vector3(path[pathIndex].x, path[pathIndex].y, transform.position.z), speed[pathIndex] * Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, zoom[pathIndex], speed[pathIndex] * Time.deltaTime);
            // If need different waitting time for each point, please chage the float value, such as 0.5f
            if (Mathf.Abs(transform.position.x - path[pathIndex].x) <= 0.5f && Mathf.Abs(transform.position.y - path[pathIndex].y) <= 0.5f){
                pathIndex++;
            }
            return true;
        }
        if (pathIndex == path.Count){
            camaraMove = false;
            return false;
        }

        return false;
    }

    private void Update()
    {
        if (!inExitPreviewMode && !inPreviewMode && Mathf.Abs(mainCamera.orthographicSize - originalSize) > 0.05f)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, originalSize, Time.deltaTime);
        }
        // startCameraTour();
        // tourPosition(pathPoints, pathZoom, pathSpeed);
        if (Input.GetKeyDown(KeyCode.O))
        {
            inPreviewMode = true;
            inExitPreviewMode = false;
        }
        else if (Input.GetKeyUp(KeyCode.O))
        {
            inExitPreviewMode = true;
        }
        if (!inExitPreviewMode && inPreviewMode){
            // Debug.Log(transform.position + " " + previewObject.transform.position);
            transform.position = Vector3.Lerp(transform.position, previewObject.transform.position, Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, cameraPreviewSize, Time.deltaTime);
        }
        else if (inExitPreviewMode && inPreviewMode){
            // Debug.Log(inExitPreviewMode + " " + inPreviewMode);
            transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, 0, -10), 5*Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, originalSize, 5*Time.deltaTime);
            if ((Mathf.Abs(transform.position.x - player.position.x) <= 0.05f && Mathf.Abs(transform.position.y - player.position.y) <= 0.05f) || Mathf.Abs(mainCamera.orthographicSize - originalSize) <= 0.05f){
                inPreviewMode = false;
                inExitPreviewMode = false;
            }      
        }
    }
    private void LateUpdate()
    {
        if (!inPreviewMode){
            InitCamera();
        }
        // if (!camaraMove)
        // {
        //     InitCamera();
        // }
    }
    private void InitCamera(){
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = player.position.x;
        cameraPosition.y = player.position.y;
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
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)){
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
