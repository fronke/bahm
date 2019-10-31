using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float dampTime = 0.2f;                 // Approximate time for the camera to refocus.
    public float screenEdgeBuffer = 4f;           // Space between the top/bottom most target and the screen edge.
    [HideInInspector]
    public Transform[] targets; // All the targets the camera needs to encompass.


    private Camera mainCamera;                        // Used for referencing the camera.
    private Vector3 moveVelocity;                 // Reference velocity for the smooth damping of the position.
    private Vector3 desiredPosition;              // The position the camera is moving towards.

    private float camMovmentX;
    private float camMovmentZ;

    private void Awake()
    {
        mainCamera = GetComponentInChildren<Camera>();

        Transform limitTop = GameObject.Find("LimitTop").GetComponent<Transform>();
        Transform limitBottom = GameObject.Find("LimitBottom").GetComponent<Transform>();
        Transform limitLeft = GameObject.Find("LimitLeft").GetComponent<Transform>();
        Transform limitRight = GameObject.Find("LimitRight").GetComponent<Transform>();

        float levelSizeX = Vector3.Distance(limitLeft.position, limitRight.position)/2;
        float levelSizeZ = Vector3.Distance(limitTop.position, limitBottom.position)/2;

        camMovmentX = levelSizeX - (mainCamera.aspect * mainCamera.orthographicSize);
        camMovmentZ = levelSizeZ - (mainCamera.orthographicSize);
    }


    private void FixedUpdate()
    {
        // Move the camera towards a desired position.
        Move();
    }


    private void Move()
    {
        // Find the average position of the targets.
        FindAveragePosition();

        // Smoothly transition to that position.
        // The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
        Vector3 target = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, dampTime);

        // Clamp camera position to block on map edges
        float targetX = Mathf.Clamp(target.x, -camMovmentX, camMovmentX);
        float targetZ = Mathf.Clamp(target.z, -camMovmentZ, camMovmentZ);

        transform.position = new Vector3(targetX, transform.position.y, targetZ);
    }


    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        // Go through all the targets and add their positions together.
        for (int i = 0; i < targets.Length; i++)
        {
            // If the target isn't active, go on to the next one.
            if (!targets[i].gameObject.activeSelf)
                continue;

            // Add to the average and increment the number of targets in the average.
            averagePos += targets[i].position;
            numTargets++;
        }

        // If there are targets divide the sum of the positions by the number of them to find the average.
        if (numTargets > 0)
            averagePos /= numTargets;

        // Keep the same y value.
        averagePos.y = transform.position.y;

        // The desired position is the average position;
        desiredPosition = averagePos;
    }

    public void SetStartPositionAndSize()
    {
        // Find the desired position.
        FindAveragePosition();

        // Set the camera's position to the desired position without damping.
        transform.position = desiredPosition;
    }
}
