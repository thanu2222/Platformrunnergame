using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    // Refernces speed and determines the speed of the projectile and makes it possible to edit the speed using the unity engine
    [SerializeField] private float speed;
    // Refernces the rest time between the enemy projectile 
    [SerializeField] private float resetTime;
    // Represents how many seconds teh projectile has been active
    private float lifetime;
    // Referncing Animator
    private Animator anim;
     // Referncing BoxCollider 2D
    private BoxCollider2D coll;

    private bool hit;

    // Calling Awake function(Awake function is called everytime the script is loaded)
    private void Awake()
    {   
        //Checking for Component for Animator and storing the component inside the anim variable
        anim = GetComponent<Animator>();
        //Checking for Component for BoxCollider2D and storing the component inside the coll variable
        coll = GetComponent<BoxCollider2D>();
    }

    // Method to activate the enemy projectile
    public void ActivateProjectile()
    {   
        // Reset the state of the projectile
        hit = false;
        // Resetting the projectie lifetime to 0
        lifetime = 0;
        // Making the projectile game object active
        gameObject.SetActive(true);
         // Enables box collider
        coll.enabled = true;
    }
    // Update is called once per frame
    private void Update()
    {   
        // Checking if the projectile hit something such as a wall if its true than the code doesn't execute
        if (hit) return;
        // Making the projectile move
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        // Calculating how many seconds the projectile has been active
        lifetime += Time.deltaTime;
         // If the projectile has been active more than the rest time than the enemy projectile will be deactivated
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

     // OnTriggerEnter2D is called when this collider2D has begun touching another rigidbody2D or Collider2D
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        // Checking if the projectile hit any other objects
        hit = true;
        base.OnTriggerEnter2D(collision); //Execute logic from parent script first
        // Disabling the box collider
        coll.enabled = false;

        if (anim != null)
            anim.SetTrigger("explode"); //When the object is a fireball explode it
        else
            gameObject.SetActive(false); //When this hits any object deactivate arrow
    }
    // Method deactives the projectile
    private void Deactivate()
    {
        // Deactivates the projectile after the expolsion has occured
        gameObject.SetActive(false);
    }
}