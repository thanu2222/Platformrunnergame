using UnityEngine;

public class Door : MonoBehaviour
{   // Refernces the Pervious room
    [SerializeField] private Transform previousRoom;
    // Refernces the New room
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    // OnTriggerEnter2D is called when this collider2D has begun touching another rigidbody2D or Collider2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checking if there is collison with gameobeject with player tag
        
        if (collision.tag == "Player")
        {
            // Checks if the player is coming from the left by seeing if the players x posistion is samller than the doors x posistion
            // If the player is coming from the left then moves the camera posistion to new room
            if (collision.transform.position.x < transform.position.x)
                cam.MoveToNewRoom(nextRoom);
            // If the player is coming from the right then moves camera posistion to the preivous room
            else
                cam.MoveToNewRoom(previousRoom);
        }
    }
}