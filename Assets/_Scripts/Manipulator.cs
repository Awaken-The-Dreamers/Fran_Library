using UnityEngine;
using UnityEngine.InputSystem;

public class Manipulator : MonoBehaviour
{
    // A variable to define the distance from the camera for the object in 3D space
    private float distanceFromCamera = 5f; 

    void Update()
    {
        // 1. Read the current mouse position from the Input System
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();

        // 2. Convert the screen position to a world position
        // The Z value is crucial for ScreenToWorldPoint; it determines how far into the scene the point is.
        mouseScreenPosition.z = distanceFromCamera; 
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        // For 2D, you might want to force the Z position to 0 to stay on the 2D plane
        //worldPoint.z = 0; // Uncomment this line for a purely 2D game

        // 3. Set the GameObject's position to the new world position
        transform.position = worldPoint;

        
    }
}
