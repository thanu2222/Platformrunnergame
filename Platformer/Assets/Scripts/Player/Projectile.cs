using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Refernces speed and determines the speed of the projectile and makes it possible to edit the speed using the unity engine
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    // Represents how many seconds teh projectile has been active
    private float lifetime;
    // Referncing Animator
    private Animator anim;
    // Referncing BoxCollider 2D
    private BoxCollider2D boxCollider;
    // Calling Awake function(Awake function is called everytime the script is loaded)
    private void Awake()
    {
        //Checking for Component for Animator and storing the component inside the anim variable
        anim = GetComponent<Animator>();
        //Checking for Component for BoxCollider2D and storing the component inside the boxCollider variable
        boxCollider = GetComponent<BoxCollider2D>();
    }

     // Update is called once per frame
    private void Update()
    {
        // Checking if the projectile hit something such as a wall if its true than the code doesn't execute
        if (hit) return;
        // Making the projectile move
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
        // Calculating how many seconds the projectile has been active
        lifetime += Time.deltaTime;
        // If the projectile has been over 5 seconds than the projectile will be deactivated
        if (lifetime > 5) gameObject.SetActive(false);
    }
    // OnTriggerEnter2D is called when this collider2D has begun touching another rigidbody2D or Collider2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checking if the projectile hit any other objects
        hit = true;
        // Disabling the box collider
        boxCollider.enabled = false;
        // Playing the explode animation
        anim.SetTrigger("explode");

        // Checking if there is a collison with a game object with tag called enemy
            // If so then the it gets the health component and takes away one health of the enemy
        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1);
    }

    // Method tells the projectile to shoot left or right
    public void SetDirection(float _direction)
    {
        // Resetting the projectie lifetime to 0
        lifetime = 0;
        direction = _direction;
        // Making the projectile game object active
        gameObject.SetActive(true);
        // Reset the state of the projectile
        hit = false;
        // Enables box collider
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        // Checking if the projectile facing the right way
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;
        // Flipping the projectile towards the way the player is facing
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    // Method deactives the projectile
    private void Deactivate()
    {
        // Deactivates the projectile after the expolsion has occured
        gameObject.SetActive(false);
    }
}