using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Refernces the trap damage
    [SerializeField] protected float damage;

    // OnTriggerEnter2D is called when this collider2D has begun touching another rigidbody2D or Collider2D
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // Checking if there is collison with gameobeject with player tag
        if (collision.tag == "Player")
            // if collsion did happen then it gets health component and does damage to the player
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}