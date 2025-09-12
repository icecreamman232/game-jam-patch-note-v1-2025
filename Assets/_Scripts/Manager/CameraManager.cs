using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Movement Settings")]
    public float mouseSensitivity = 2.0f;
    public bool invertY = true; // Set to true for inverted Y movement
    
    [Header("World Bounds (Area that camera can see)")]
    public Vector2 worldMinBounds = new Vector2(-10f, -10f);
    public Vector2 worldMaxBounds = new Vector2(10f, 10f);
    
    [Header("Pixel Perfect Settings")]
    public int pixelsPerUnit = 64;
    public Vector2 referenceResolution = new Vector2(960f, 540f);
    
    [Header("Gizmo Settings")]
    public Color boundsColor = Color.yellow;
    public Color cameraBoundsColor = Color.cyan;
    public bool showBounds = true;
    
    private Vector3 lastMousePosition;
    private bool isDragging = false;
    private Camera cam;
    private PixelPerfectCamera pixelPerfectCamera;
    
    private void Start()
    {
        cam = GetComponent<Camera>();
        pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
    }
    
    void Update()
    {
        HandleMouseDrag();
    }
    
    private void HandleMouseDrag()
    {
        // Check if mouse button is pressed down
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }
        
        // Check if mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        
        // Handle dragging
        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - lastMousePosition;
            
            // Convert mouse movement to world movement
            float moveX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
            float moveY = mouseDelta.y * mouseSensitivity * Time.deltaTime;
            
            // Invert Y movement (when mouse moves up, camera moves down)
            if (invertY)
            {
                moveY = -moveY;
            }
            
            // Calculate new position
            Vector3 newPosition = transform.position + new Vector3(-moveX, -moveY, 0);
            
            // Apply camera view bounds constraints
            Vector2 cameraBounds = GetCameraBounds();
            newPosition.x = Mathf.Clamp(newPosition.x, cameraBounds.x, cameraBounds.y);
            
            Vector2 cameraVerticalBounds = GetCameraVerticalBounds();
            newPosition.y = Mathf.Clamp(newPosition.y, cameraVerticalBounds.x, cameraVerticalBounds.y);
            
            // Apply the constrained position
            transform.position = newPosition;
            
            lastMousePosition = currentMousePosition;
        }
    }
    
    private Vector2 GetCameraBounds()
    {
        float cameraHalfWidth = GetCameraWorldWidth() / 2f;
        float minX = worldMinBounds.x + cameraHalfWidth;
        float maxX = worldMaxBounds.x - cameraHalfWidth;
        return new Vector2(minX, maxX);
    }
    
    private Vector2 GetCameraVerticalBounds()
    {
        float cameraHalfHeight = GetCameraWorldHeight() / 2f;
        float minY = worldMinBounds.y + cameraHalfHeight;
        float maxY = worldMaxBounds.y - cameraHalfHeight;
        return new Vector2(minY, maxY);
    }
    
    private float GetCameraWorldWidth()
    {
        if (pixelPerfectCamera != null)
        {
            // Calculate based on pixel perfect camera settings
            return referenceResolution.x / pixelsPerUnit;
        }
        else if (cam != null)
        {
            // Fallback to regular camera calculation
            return cam.orthographicSize * 2f * cam.aspect;
        }
        
        // Default fallback
        return referenceResolution.x / pixelsPerUnit;
    }
    
    private float GetCameraWorldHeight()
    {
        if (pixelPerfectCamera != null)
        {
            // Calculate based on pixel perfect camera settings
            return referenceResolution.y / pixelsPerUnit;
        }
        else if (cam != null)
        {
            // Fallback to regular camera calculation
            return cam.orthographicSize * 2f;
        }
        
        // Default fallback
        return referenceResolution.y / pixelsPerUnit;
    }
    
    // Draw gizmos in scene view
    private void OnDrawGizmos()
    {
        if (!showBounds) return;
        
        // Draw world bounds (yellow)
        Gizmos.color = boundsColor;
        Vector3 worldCenter = new Vector3(
            (worldMinBounds.x + worldMaxBounds.x) / 2f,
            (worldMinBounds.y + worldMaxBounds.y) / 2f,
            transform.position.z
        );
        
        Vector3 worldSize = new Vector3(
            worldMaxBounds.x - worldMinBounds.x,
            worldMaxBounds.y - worldMinBounds.y,
            0.1f
        );
        
        Gizmos.DrawWireCube(worldCenter, worldSize);
        
        // Draw camera movement bounds (cyan)
        Gizmos.color = cameraBoundsColor;
        Vector2 cameraBoundsX = GetCameraBounds();
        Vector2 cameraBoundsY = GetCameraVerticalBounds();
        
        Vector3 cameraBoundsCenter = new Vector3(
            (cameraBoundsX.x + cameraBoundsX.y) / 2f,
            (cameraBoundsY.x + cameraBoundsY.y) / 2f,
            transform.position.z
        );
        
        Vector3 cameraBoundsSize = new Vector3(
            cameraBoundsX.y - cameraBoundsX.x,
            cameraBoundsY.y - cameraBoundsY.x,
            0.1f
        );
        
        Gizmos.DrawWireCube(cameraBoundsCenter, cameraBoundsSize);
        
        // Draw current camera view area
        Gizmos.color = Color.green;
        float cameraWidth = GetCameraWorldWidth();
        float cameraHeight = GetCameraWorldHeight();
        Vector3 currentViewSize = new Vector3(cameraWidth, cameraHeight, 0.1f);
        Gizmos.DrawWireCube(transform.position, currentViewSize);
        
        // Draw corner markers for world bounds
        float markerSize = 0.5f;
        Gizmos.color = boundsColor;
        Vector3[] worldCorners = new Vector3[]
        {
            new Vector3(worldMinBounds.x, worldMinBounds.y, transform.position.z),
            new Vector3(worldMaxBounds.x, worldMinBounds.y, transform.position.z),
            new Vector3(worldMaxBounds.x, worldMaxBounds.y, transform.position.z),
            new Vector3(worldMinBounds.x, worldMaxBounds.y, transform.position.z)
        };
        
        foreach (Vector3 corner in worldCorners)
        {
            Gizmos.DrawWireCube(corner, Vector3.one * markerSize);
        }
    }
    
    // Draw gizmos when selected (more opaque)
    private void OnDrawGizmosSelected()
    {
        if (!showBounds) return;
        
        // Set a more opaque color when selected for world bounds
        Color selectedWorldColor = boundsColor;
        selectedWorldColor.a = 0.2f;
        Gizmos.color = selectedWorldColor;
        
        Vector3 worldCenter = new Vector3(
            (worldMinBounds.x + worldMaxBounds.x) / 2f,
            (worldMinBounds.y + worldMaxBounds.y) / 2f,
            transform.position.z
        );
        
        Vector3 worldSize = new Vector3(
            worldMaxBounds.x - worldMinBounds.x,
            worldMaxBounds.y - worldMinBounds.y,
            0.1f
        );
        
        Gizmos.DrawCube(worldCenter, worldSize);
        
        // Draw camera bounds with different color
        Color selectedCameraColor = cameraBoundsColor;
        selectedCameraColor.a = 0.3f;
        Gizmos.color = selectedCameraColor;
        
        Vector2 cameraBoundsX = GetCameraBounds();
        Vector2 cameraBoundsY = GetCameraVerticalBounds();
        
        Vector3 cameraBoundsCenter = new Vector3(
            (cameraBoundsX.x + cameraBoundsX.y) / 2f,
            (cameraBoundsY.x + cameraBoundsY.y) / 2f,
            transform.position.z
        );
        
        Vector3 cameraBoundsSize = new Vector3(
            cameraBoundsX.y - cameraBoundsX.x,
            cameraBoundsY.y - cameraBoundsY.x,
            0.1f
        );
        
        Gizmos.DrawCube(cameraBoundsCenter, cameraBoundsSize);
    }
}
