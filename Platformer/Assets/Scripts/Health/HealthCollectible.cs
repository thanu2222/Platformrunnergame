using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    // Refernces the health value
    [SerializeField] private float healthValue;

     // OnTriggerEnter2D is called when this collider2D has begun touching another rigidbody2D or Collider2D
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        // Checking if there is collison with gameobeject with player tag
        if (collision.tag == "Player")
        {
            // If there is collsion than gets component from health and adds health
            collision.GetComponent<Health>().AddHealth(healthValue);
            // Deactivates the health colleactable after the health is full
            gameObject.SetActive(false);
        }
    }
}