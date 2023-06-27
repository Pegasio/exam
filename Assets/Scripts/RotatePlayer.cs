using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public GameObject player; // Drag and drop your player object into this field in the inspector

    public void RotateLeft()
    {
        // Rotate the player around the y-axis in the opposite direction (counterclockwise)
        player.transform.Rotate(Vector3.up, -90.0f * Time.deltaTime);
    }

    public void RotateRight()
    {
        // Rotate the player around the y-axis in the clockwise direction
        player.transform.Rotate(Vector3.up, 90.0f * Time.deltaTime);
    }
}
