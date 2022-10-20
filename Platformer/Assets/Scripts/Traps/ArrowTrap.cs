using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour

{   // Refernces arrow trap attack cool down 
    [SerializeField] private float attackCooldown;
    // Referncing the posistion the arrows will be fired
    [SerializeField] private Transform firePoint;
    // Referncing Game obejcts to place the arrows
    [SerializeField] private GameObject[] arrows;
    // Cooldown timer
    private float cooldownTimer;

    // Calling Attack Method
    private void Attack()
    {
        // When the arrow attacks the cool down timer is reset to 0
        cooldownTimer = 0;

        //Resetting the posistion of the arrows to the fire point
        arrows[FindArrow()].transform.position = firePoint.position;
         // Getting the enemy projectile component form the arrowtrap and sending the arrows towards where the arrowtrap is facing
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    // Method for all arrows
    private int FindArrow()
    {   
        // loop through all the arrows
        for (int i = 0; i < arrows.Length; i++)
        {
             // Checking if the arrows in the Hierachy is active and if not active then the arrows can be used again
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
     // Update is called once per frame
    private void Update()
    {
        // Cooldown timer
        cooldownTimer += Time.deltaTime;

        // If the cool down timer is greater or equal to attack cool down than the arrowtrap will attack
        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}