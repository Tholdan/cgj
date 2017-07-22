using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //PUBLIC
    [Header("Game Objects")]
    public GameObject mainCamera;
    public GameObject focus;

    [Header("Camera Configuration")]
    public float cameraDistance;
    public float cameraHeight;
    public Vector3 LookAtHeight;

    [Header("Camera Speed")]
    public float movementSpeed;
    public float rotationSpeed;


    //PRIVATE
    private RaycastHit rayCastHitInfo;
    private Vector3 normalCameraPosition;
    private float time;

	// Use this for initialization
	void Start () {
        time = 1;
        transform.position = focus.transform.position;
        transform.rotation = focus.transform.rotation;
        normalCameraPosition = new Vector3(0.0f, cameraHeight, -cameraDistance);
        mainCamera.transform.localPosition = normalCameraPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (AntiWallCollision())
        {
            transform.position = focus.transform.position;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, focus.transform.position, movementSpeed * Time.deltaTime);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, focus.transform.rotation, rotationSpeed * Time.deltaTime);
        mainCamera.transform.LookAt(transform.position + LookAtHeight);
	}

    /// <summary>
    /// Moves the camera forward if it's gonna collide with a wall.
    /// </summary>
    /// <returns></returns>
    bool AntiWallCollision()
    {
        bool collisionDetected = false;
        Ray antiCollisionRay = new Ray(transform.position, transform.rotation * normalCameraPosition);

        if (Physics.Raycast(antiCollisionRay, out rayCastHitInfo, normalCameraPosition.magnitude) && !rayCastHitInfo.transform.CompareTag("Player"))
        {
            time = 0;
            collisionDetected = true;
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, normalCameraPosition.normalized * (Vector3.Distance(transform.position, rayCastHitInfo.point) - 1f), 10*Time.deltaTime);
        }
        else
        {
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, normalCameraPosition, 0.25f * time);
            time += Time.deltaTime;
        }

        return collisionDetected;
    }
}
