using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDistanceAway = 1f;
    public float maxDistanceAway = 3.5f;
    public float smooth = 20f;
    private float distance;
    private Vector3 dollyDirection;

    // Get camera's position relative to CameraBase and the vector's length
    void Awake ()
    {
        dollyDirection = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
	}
	
	// Find where camera should be, if a collision is detected then move camera forward
	void Update ()
    {
        Vector3 desiredCameraPosition = transform.parent.TransformPoint(dollyDirection * maxDistanceAway);
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredCameraPosition, out hit))
            distance = Mathf.Clamp(hit.distance * 0.9f, minDistanceAway, maxDistanceAway);
        else
            distance = maxDistanceAway;

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDirection * distance, Time.deltaTime * smooth);
	}
}
