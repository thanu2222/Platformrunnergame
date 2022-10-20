using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Represents how much time will be needed till the player can shoot the next shot
    [SerializeField] private float attackCooldown;
    // Referncing the posistion the fireball will be fired
    [SerializeField] private Transform firePoint;
    // Referncing Game obejcts to place the fireballs
    [SerializeField] private GameObject[] fireballs;
    // References Player Movement 
    private PlayerMovement playerMovement;
    // Cooldown timer
    private float cooldownTimer = Mathf.Infinity;

    // Calling Awake function(Awake function is called everytime the script is loaded)
    private void Awake()
    {
        // Getting Player Movement Componenet
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Checking if the left mouse button is clicked
        // Checking if the cooldown timer is bigger than than the attack cooldown and checks if the player is in a state able to attack
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }
    // Calling Attack Method
    private void Attack()
    {
        // When the player attacks the cool down timer is reset to 0
        cooldownTimer = 0;

        //Resetting the posistion of the fire balls to the fire point
        fireballs[FindFireball()].transform.position = firePoint.position;
        // Getting the projectile component form the Fireball and sending the fireball towards where the player is facing
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    // Method for all fireballs
    private int FindFireball()
    {
        // loop through all the fireballs
        for (int i = 0; i < fireballs.Length; i++)
        {   
            // Checking if the fireball in the Hierachy is active and if not active the fireball can be used again
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}