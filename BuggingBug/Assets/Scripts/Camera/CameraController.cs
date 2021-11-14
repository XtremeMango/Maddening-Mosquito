using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Vector2 boundaryThickness;
    [SerializeField]
    float cameraFollowSpeed;
    [SerializeField]
    Vector3 mousePos;

    Vector3 screenCenter;
    enum axis { X,Y}
    private void Awake()
    {
        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2,0);
        boundaryThickness = new Vector3(Screen.width / 4, Screen.height / 4);
    }

    private void Update()
    {
        mousePos = Input.mousePosition;
        if (Input.mousePosition.x < boundaryThickness.x || Input.mousePosition.x > Screen.width - boundaryThickness.x || Input.mousePosition.y < boundaryThickness.y || Input.mousePosition.y > Screen.height - boundaryThickness.y)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 dir = (Input.mousePosition - screenCenter).normalized;
        transform.position = Vector3.Slerp(transform.position,transform.position+dir,cameraFollowSpeed * Time.deltaTime);
    }
}
