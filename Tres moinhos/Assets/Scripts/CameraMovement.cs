using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject terrain;
    [SerializeField] float rotateSpeed = 2;
    [SerializeField] float zoomSpeed = 2;
    [SerializeField] float maxZoom = 20;
    [SerializeField] float minZoom = 60;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.RotateAround(terrain.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.RotateAround(terrain.transform.position, Vector3.up, -rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow) && Camera.main.fieldOfView >= maxZoom)
        {
            Camera.main.fieldOfView -= zoomSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Camera.main.fieldOfView <= minZoom)
        {
            Camera.main.fieldOfView += zoomSpeed * Time.deltaTime;
        }
    }
}
