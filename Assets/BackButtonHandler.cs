using UnityEngine;

public class BackButtonHandler : MonoBehaviour
{
    private float lastTapTime;
    public float doubleTapThreshold = 0.5f;

    void Update()
    {
        // Check for the device back button input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Calculate the time since the last tap
            float timeSinceLastTap = Time.time - lastTapTime;

            // Check if it's a double tap
            if (timeSinceLastTap < doubleTapThreshold)
            {
                // Call the function to handle the double tap
                HandleDoubleTap();
            }
            else
            {
                // Update the last tap time for future reference
                lastTapTime = Time.time;
            }
        }
    }

    void HandleDoubleTap()
    {
        // Implement your logic here for what should happen on a double tap
        // For example, you can close the game or show a confirmation dialog

        // Close the application (this works for standalone builds)
        // Note: This won't work in the Unity Editor, but it will work in the built application
        Application.Quit();
    }
}
