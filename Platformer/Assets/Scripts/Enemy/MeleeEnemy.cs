using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    // Refernces arrow trap attack cool down 
    [SerializeField] private float attackCooldown;
    // References the range of the enemy
    [SerializeField] private float range;
    // Refernces the damage the enemy does
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    // Referncing the distance the player needs to be within for the enemy to start slashing from
    [SerializeField] private float colliderDistance;
     // Referncing Box Collider2D
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    // Referncing Player Layer
    [SerializeField] private LayerMask playerLayer;
    // Referncing Cooldownt timer
    private float cooldownTimer = Mathf.Infinity;

    //References
    // Referncing BoxCollider 2D
    private Animator anim;
    // Referncing player health
    private Health playerHealth;
    // Refenrcinng Enemy Patrol
    private EnemyPatrol enemyPatrol;
    
    // Calling Awake function(Awake function is called everytime the script is loaded)
    private void Awake()
    {   
        //Checking for Component for Animator and storing the component inside the anim variable
        anim = GetComponent<Animator>();
        //Checking for Component for Enemypatrol  and storing the component inside the enemyPatrol variable
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Update is called once per frame
    private void Update()
    {   
        // Cool downt timer
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight
        // Checks if player is in sight
        if (PlayerInSight())
        {
            // checks if player is in sight and the cool down timer is greater than attack cool down
            if (cooldownTimer >= attackCooldown)
            {  
                 // reset the cooldown timer to 0
                cooldownTimer = 0;
                // Enemy attack animation
                anim.SetTrigger("meleeAttack");
            }
        }
        
        // Patroling of the enemy
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    // Player in sight method
    private bool PlayerInSight()
    {   
        // Creats a ray from point orgin into a certain direction if the line intercepts with a object that has a collide than it will return true other wise it will return false 
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        // Enemy hitting player
        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }
    // Drawing Gizomos method
    private void OnDrawGizmos()
    {
        // Drawing a box which shows how far the enemy will see and where the player will have to be to be hit by the enemy
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // Damaging player method
    private void DamagePlayer()
    {   // Damaging player health
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
}