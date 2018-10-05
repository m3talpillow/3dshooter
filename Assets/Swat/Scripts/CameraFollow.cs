using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject cameraFollowObject;

    // Adjustable values 
    public float clampAngleHigh = 70f;
    public float clampAngleLow = -50f;
    public float cameraMoveSpeed = 120f;
    public float inputSensitivity = 150f;

    // Used for keeping track of camera movements
    private float mouseUpDown;
    private float mouseLeftRight;
    private float cameraRotationUpDown;
    private float cameraRotationLeftRight;
    
    void Start ()
    {
        // Find starting rotation of camera
        Vector3 rotation = transform.localRotation.eulerAngles;
        cameraRotationUpDown = rotation.x;
        cameraRotationLeftRight = rotation.y;

        // Hide and disable cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
    // Modifying Euler angles of camera object before assigning back
	void Update ()
    {
        // Pick up mouse movement
        mouseUpDown = Input.GetAxis("Mouse Y");
        mouseLeftRight = Input.GetAxis("Mouse X");

        // Use sensitivity to adjust how quick camera moves
        cameraRotationUpDown += mouseUpDown * inputSensitivity * Time.deltaTime;
        cameraRotationLeftRight += mouseLeftRight * inputSensitivity * Time.deltaTime;

        // Restrict camera rotation and assign the new rotation to camera object
        cameraRotationUpDown = Mathf.Clamp(cameraRotationUpDown, clampAngleLow, clampAngleHigh);
        transform.rotation = Quaternion.Euler(cameraRotationUpDown, cameraRotationLeftRight, 0.0f);
    }

    // CameraBase will always move towards CameraFollowObject and sit on top of it (swivel point for camera)
    private void LateUpdate()
    {
        Transform target = cameraFollowObject.transform;
        float step = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
