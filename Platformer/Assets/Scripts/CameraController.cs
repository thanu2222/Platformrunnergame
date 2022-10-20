using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room camera
    // Refernces speed and determines the speed of the camera and makes it possible to edit the speed using the unity engine
    [SerializeField] private float speed;
    // Tells the camera which posistion to go to 
    private float currentPosX;
    //
    private Vector3 velocity = Vector3.zero;

    //Follow player
    // Refernces the player
    [SerializeField] private Transform player;
    // References how far the camera looks ahead of the player
    [SerializeField] private float aheadDistance;
    // References the camrea speed
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    // Update is called once per frame
    private void Update()
    {
        //Room camera
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

        //Follows player
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

   // Method changes destination of the camera 
    public void MoveToNewRoom(Transform _newRoom)
    {   
        // Changes camrea posistion to the new room
        currentPosX = _newRoom.position.x;
    }
}