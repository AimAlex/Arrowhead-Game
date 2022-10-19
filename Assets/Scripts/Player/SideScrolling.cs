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
    public bool camaraMove, inFrame1, inFrame2;
    private float originalSize = 9.306593f;
    private Camera mainCamera;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        startPosition = transform.position - player.position;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        camaraMove = true;
        inFrame1 = true;
        inFrame2 = false;
        smoothing = 1.5f;
    }
    private void Start(){
        initPosition = transform.position;
    }
    private void turotialFrame(){
        if (inFrame1){
            // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(initPosition.x + 5, initPosition.y, transform.position.z), ref direction, smoothing);
            transform.position = new Vector3(initPosition.x + 5, initPosition.y, transform.position.z);
            // infobox1 show
            if (Input.GetKeyDown(KeyCode.Return)){
                inFrame1 = false;
                inFrame2 = true;
            }
        }
        else if (inFrame2){
            transform.position = new Vector3(initPosition.x + 10, initPosition.y, transform.position.z);
            // infobox2 show
            if (Input.GetKeyDown(KeyCode.Return)){
                inFrame2 = false;
                inFrame1 = false;
                camaraMove = false;
                transform.position = initPosition;
            }
        }
    }
    private void Update()
    {
        turotialFrame();
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
    // private void GetDirection(Vector3 direction)
    // {
        
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
    //     direction.x *= 2;
    //     direction.y *= 2;
    //     transform.Translate(direction * Time.deltaTime, Space.World);

    // }
    // public void CameraTour(List<Vector3> tourPoints)
    // {
    //     camaraMove = true;
    //     for (int i = 0; i < tourPoints.Count; i++)
    //     {
    //         transform.position = Vector3.Lerp(transform.position, tourPoints[i], Time.deltaTime);
    //         Debug.Log(transform.position);
    //     }
    //     camaraMove = false;
    // }
}
