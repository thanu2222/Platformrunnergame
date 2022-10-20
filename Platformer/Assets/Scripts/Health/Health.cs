using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    // Determinds how much health starts with
    [SerializeField] private float startingHealth;
    // Contains the current health of the player
    public float currentHealth { get; private set; }
    
    // Referncing Animator
    private Animator anim;

    private bool dead;
    public GameObject gameOver;

    [Header("iFrames")]
    // Referncing IFrames duration
    [SerializeField] private float iFramesDuration;
    // Referncing IFrames number of flshes
    [SerializeField] private int numberOfFlashes;
    // Refrencing Sprite Renderer
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    
    // Start is called before the first frame update
    void Start()
    {
        // Game over is false because its the start of the game
        gameOver.SetActive(false);
    }

    // Calling Awake function(Awake function is called everytime the script is loaded)
    private void Awake()
    {
        // Current health is the same as starting health at the start
        currentHealth = startingHealth;
        //Checking for Component in Animator and storing the component inside the anim variable
        anim = GetComponent<Animator>();
        //Checking for Component for Sprite Renderer and storing the component inside the spriteRend variable
        spriteRend = GetComponent<SpriteRenderer>();
    }
    // Method to take damage
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        // To make sure the health minimum value is 0 and maximum value is starting health
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        // Checks if the current health is above 0
        if (currentHealth > 0)
        {
            
            StartCoroutine(Invunerability());
        }
        else
        {
            // if the dead variable is false
            if (!dead)
            {
                // Plays the die animation 
                anim.SetTrigger("die");
                //Activate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;
                // Checks if the player is dead so the die animation isnt repeadted
                dead = true;
                // Gameover is true because the player has died
                gameOver.SetActive(true);
            }
        }
    }
    // Method to add health
    public void AddHealth(float _value)
    {
        // Addding health
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        // Flashing purple when the player has taken damage to show the user that the have player is taking damage
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {   
            // Colour of the flashing
            spriteRend.color = new Color(0.3f, 0, 0.3f, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    
     private void Deactivate()
    {
        gameObject.SetActive(false);
    }


}
      

