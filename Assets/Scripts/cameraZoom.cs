using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraZoom : MonoBehaviour
{
    // [SerializeField] float zoomInSpeed = 1f;
    // [SerializeField] float zoomOutSpeed = 1f;
    // [SerializeField] float targetZoomIn = .3f;

    // float initalZoom = 0f;
    // float initalPosX;
    // float initalPosY;

    // public static bool IsZoomedIn = false;
    // public static bool IsZoomedOut = false;

    // Transform target;
    // Camera cam;

    // Void Start()
    // {
    //     cam = Camera.main;
    //     intialZoom = cam.orthographicSize;
    //     initalPosX = cam.transform.position.x;
    //     initalPosY = cam.transform.position.y;
    // }

    // public void ZoomInCamera(Transform target)
    // {
    //     if(!IsZoomedIn)
    //     {
    //         cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoomIn, Time.deltaTime * zoomInSpeed);
    //         cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, target.postion.x, Time.deltaTime * zoomInSpeed),
    //             Mathf.Lerp(cam.transform.position.y, target.position.y, Time.deltaTime * zoomInSpeed),
    //             cam.transform.position.z);
            
    //         if(cam.orthographicSize - targetZoomIn <= .001f)
    //         {
    //             IsZoomedIn = true;
    //         }
            
    //     }
        
    // }

    // public void ZoomOutCamera(Transform target)
    // {
    //     if(!IsZoomedIn)
    //     {
    //         cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, initalZoom, Time.deltaTime * zoomOutSpeed);
    //         cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, initalPosX, Time.deltaTime * zoomOutSpeed),
    //             Mathf.Lerp(cam.transform.position.y, initalPosY, Time.deltaTime * zoomOutSpeed),
    //             cam.transform.position.z);
            
    //         if(initalZoom - cam.orthographicSize <= .05f)
    //         {
    //             IsZoomedIn = false;
    //             IsZoomedOut = true;
    //             cam.orthographicSize = initalZoom;
    //             cam.transform.position = new Vector3(intialPosX, initialPosY, cam.transform.position.z);
    //         }
    //     }
    // }
    //-----------------------------------------------------------------------------------------------
    // [SerializeField]public float Speed;
    // public bool ZoomActive;
    // public Vector3[] Target;
    // public Camera Cam;
    
    // // Start is called before the first frame update
    // void Start()
    // {
    //     Cam = Camera.main;
    // }

    // // Update is called once per frame
    // public void LateUpdate()
    // {
    //     if(ZoomActive){
    //         Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, 3, Speed);
    //         Cam.transform.position = Vector3.Lerp(Cam.transform.position, Target[1], Speed);
    //     }
    //     else{
    //         Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, 5, Speed);
    //         Cam.transform.position = Vector3.Lerp(Cam.transform.position, Target[0], Speed);
    //     }
    // }
}
