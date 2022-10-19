using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // This variable will be for us to assign the Game Over Panel
    public GameObject gameOverPanel;
    // Update is called once per frame
    void Update()
    {
        // if the gameobect tag player is equal to zero which identifys that the player is dead
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            // if the the gameobeject player tag is zero that means the player is dead so the the game over panel comes up
            gameOverPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        // Function to be called when the restart button is pressed
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}