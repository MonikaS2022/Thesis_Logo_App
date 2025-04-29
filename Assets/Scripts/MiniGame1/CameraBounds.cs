using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Camera cameraToDisplay; // Reference to the camera

    void OnDrawGizmos()
    {
        if (cameraToDisplay == null) return;

        // Get the world space coordinates of the camera's corners
        Vector3 bottomLeft = cameraToDisplay.ViewportToWorldPoint(new Vector3(0, 0, cameraToDisplay.nearClipPlane));
        Vector3 topLeft = cameraToDisplay.ViewportToWorldPoint(new Vector3(0, 1, cameraToDisplay.nearClipPlane));
        Vector3 topRight = cameraToDisplay.ViewportToWorldPoint(new Vector3(1, 1, cameraToDisplay.nearClipPlane));
        Vector3 bottomRight = cameraToDisplay.ViewportToWorldPoint(new Vector3(1, 0, cameraToDisplay.nearClipPlane));

        Vector3 farBottomLeft = cameraToDisplay.ViewportToWorldPoint(new Vector3(0, 0, cameraToDisplay.farClipPlane));
        Vector3 farTopLeft = cameraToDisplay.ViewportToWorldPoint(new Vector3(0, 1, cameraToDisplay.farClipPlane));
        Vector3 farTopRight = cameraToDisplay.ViewportToWorldPoint(new Vector3(1, 1, cameraToDisplay.farClipPlane));
        Vector3 farBottomRight = cameraToDisplay.ViewportToWorldPoint(new Vector3(1, 0, cameraToDisplay.farClipPlane));

        // Visualize the camera's frustum bounds (camera's screen)
        Gizmos.color = Color.green;

        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);

        Gizmos.color = Color.red; // Far plane
        Gizmos.DrawLine(farBottomLeft, farTopLeft);
        Gizmos.DrawLine(farTopLeft, farTopRight);
        Gizmos.DrawLine(farTopRight, farBottomRight);
        Gizmos.DrawLine(farBottomRight, farBottomLeft);
    }
}