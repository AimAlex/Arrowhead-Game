// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class level2ParallaxController : MonoBehaviour
{
    Transform cam; // Main Camera
    Vector3 camStartPos;
    Vector2 distance; // distance between the camera start position and its current position

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;

    float farthestBack;

    [Range(0f,0.05f)]
    public float parallaxSpeed;

    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;

        }
        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++) // find the farthest background
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }

        }

        for (int i = 0; i < backCount; i++) // set the speed of backgrounds
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

   

    private void LateUpdate()
    {
        distance = cam.position - camStartPos;
        // transform.position = new Vector3(cam.position.x, transform.position.y, 0);


        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speedX = backSpeed[i] * parallaxSpeed;
            float speedY = speedX / 2;  // if you close Y movement , set to 0
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance.x*speedX, distance.y*speedY));
        }
    }
}
