using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    // Refernces the trap damage
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    // References the delay it takes for the trap to activate
    [SerializeField] private float activationDelay;
    // References the time the trap is active for
    [SerializeField] private float activeTime;
    // References the animator component
    private Animator anim;
    // Refrences the Sprite Renderer component
    private SpriteRenderer spriteRend;

    //when the trap gets triggered
    private bool triggered; 
    
    // When trap can be active and hurt player
    private bool active; 
    
    // Calling Awake function(Awake function is called everytime the script is loaded)
    private void Awake()
    {
         //Checking for Component in Animator and storing the component inside the anim variable
        anim = GetComponent<Animator>();
        //Checking for Component for Sprite Renderer and storing the component inside the spriteRend variable
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // OnTriggerEnter2D is called when this collider2D has begun touching another rigidbody2D or Collider2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checking if there is collison with gameobeject with player tag
        if (collision.tag == "Player")
        {
            // checkis if the firetrap is not triggered 
            // Than Active fire trap
            if (!triggered)
                StartCoroutine(ActivateFiretrap());

            // If the fire trap is active
            // Than get health Component and take damage for the player
            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    private IEnumerator ActivateFiretrap()
    {
        //turn the sprite red to notify the player and trigger the trap
        triggered = true;
        spriteRend.color = Color.red; 

        //Wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; //turn the sprite back to its initial color
        active = true;
        anim.SetBool("activated", true);

        //Wait until X seconds, deactivate trap and reset all variables and animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}