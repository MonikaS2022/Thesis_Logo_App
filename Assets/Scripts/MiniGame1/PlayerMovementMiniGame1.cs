using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMiniGame1 : MonoBehaviour
{
    public Camera mainCamera;  // Reference to the main camera
    public float moveSpeed = 10f;  // Speed of basket movement
    private float horizontalInput;
    private float cameraWidth;  // Width of the camera's view
    private float cameraHeight; // Height of the camera's view
    public float basketSize = 1f;  // The size of the basket (cube)

    void Start()
    {
        // Calculate the width and height of the camera's view
        float aspectRatio = mainCamera.aspect;  // Aspect ratio of the camera (width/height)
        cameraHeight = mainCamera.orthographicSize * 2;  // Height of the camera's view
        cameraWidth = cameraHeight * aspectRatio;  // Width of the camera's view
    }
    void Update()
    {
        // Get horizontal input (e.g., left and right arrow keys or A/D keys)
        horizontalInput = Input.GetAxis("Horizontal");

        // Move the player (basket) based on the input
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

        // Clamp the basket's position within the camera's boundaries, 
        // taking half the size of the basket into account
        float halfBasketWidth = basketSize / 2f;

        // Clamp X position (basket should stay within camera's boundaries, excluding half its size)
        float clampedX = Mathf.Clamp(transform.position.x, -cameraWidth / 2f + halfBasketWidth, cameraWidth / 2f - halfBasketWidth);

        // Optionally, clamp Y position if needed (for 2D-style movement)
        float clampedY = Mathf.Clamp(transform.position.y, -cameraHeight / 2f + halfBasketWidth, cameraHeight / 2f - halfBasketWidth);

        // Apply the clamped position to the basket
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
