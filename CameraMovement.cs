using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform cameraTransform; // The position, rotation and scale of the Main Camera
    public float cameraSpeed; // The speed of the camera's movement

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraTransform.Translate(new Vector3(cameraSpeed, 0.0f)); // Move the cameras position to the right at the speed of the float variable "cameraSpeed"
    }
}
