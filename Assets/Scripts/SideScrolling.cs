using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;
    private Vector3 startPosition;
    [SerializeField] private float smoothing;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        startPosition = transform.position - player.position;
    }

    private void LateUpdate()
    {
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

}
