using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Refernicing Speed and makes it possible to edit speed using the unity engine and determines the speed of the player
    [SerializeField] private float speed;
    // Referncing Jump Power and makes it possible to edit jump power using the Unity engine and determines the jump power of the player
    [SerializeField] private float jumpPower;
    // Referncing Ground Layer
    [SerializeField] private LayerMask groundLayer;
    // Referncing Wall layer
    [SerializeField] private LayerMask wallLayer;
    // Referncing Rigidbody2D
    private Rigidbody2D body;
    // Referncing Box Collider2D
    private BoxCollider2D boxCollider;
    // Variable will create delays between wall jumps
    private float wallJumpCooldown;
    private float horizontalInput;

    // Calling Awake function(Awake function is called everytime the script is loaded)
    private void Awake()
    {
        //Checking for Component for Rigidbody2D and storing the component inside the body variable
        body = GetComponent<Rigidbody2D>();
        //Checking for Component for BoxCollider2D and storing the component inside the boxCollider variable
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Stores the Input.GetAxis("Horizontal") value
        horizontalInput = Input.GetAxis("Horizontal");
        //Flipping player when moving left or right
        // Checking if the player is moving right
        if(horizontalInput > 0.01f)
            // Changes the player x Scale to 0.3 which makes the player face right
            transform.localScale = new Vector3(0.3f,0.3f,0.3f);
            // Checking if the player is moving left
        else if(horizontalInput < -0.01f)
            // Changes the player x scale to -0.3 which makes the player face left
            transform.localScale = new Vector3(-0.3f,0.3f,0.3f);

        // Wall Jumping
        // Checking if the wall jump cooldown is smaller than 0.2
        if (wallJumpCooldown > 0.2f)
        {  

            // Directly Changing how fast the player body is moving and which direction the player body is moving
            //Moving the player left or right depending on the user input
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            // Checks if the player is on the wall and not grounded
            if (onWall() && !isGrounded())
            {   
                // Makes player gravity zero
                // Makes the player stuck to the wall and not fall down
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            // Makes the player fall from the wall to the ground when going away from the wall
            else 
                body.gravityScale = 3;   
            
            // Checking if Input "Space" has been pressed and makes the player jump
            if (Input.GetKey(KeyCode.Space))
                Jump();
    
        }
        // Wall jump cooldown
        else 
            wallJumpCooldown += Time.deltaTime;
    
        

    }
    // Calling Jump method
    private void Jump()
    {
        if(isGrounded())
        {
        // Makes the player jump
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        
        }
        // Check if the player is on the wall not on the ground
        else if(onWall() && !isGrounded())
        {
            // Checks if the horizontal input is equal to 0
            if(horizontalInput == 0)
            {
                // Pushes the player away from the wall
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                // Flips the player when jumping from the wall
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            // Pushes the player upwards and away from the wall
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
            
        }
    }
   // Method tells if the player is grounded or not
    private bool isGrounded()
    {
        // Creats a ray from point orgin into a certain direction if the line intercepts with a object that has a collide than it will return true other wise it will return false in this case its the ground
        // Makes the player stay grounded on the ground and not fall through
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    // Method tells if the player is on the wall or not
     private bool onWall()
    {   
        // Creats a ray from point orgin into a certain direction if the line intercepts with a object that has a collide than it will return true other wise it will return false in this case its the wall
        // Makes the player not go throught the walls
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    // Method when the player can Attack
    public bool canAttack()
    {   
        //Checks and makes sure the player Can only attack when the player is not moving left to right and the player is grounded and not on a wall
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
