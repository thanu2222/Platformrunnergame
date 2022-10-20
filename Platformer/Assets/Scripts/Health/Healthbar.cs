using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    // Refernces Player health
    [SerializeField] private Health playerHealth;
    // Refernces the total health bar image
    [SerializeField] private Image totalhealthBar;
    // Refernces the current health bar imaage
    [SerializeField] private Image currenthealthBar;

    // Start is called before the first frame update
    private void Start()
    {
        // Total health bar heart image
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
    private void Update()
    {
        // Current healthbar heart image
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}