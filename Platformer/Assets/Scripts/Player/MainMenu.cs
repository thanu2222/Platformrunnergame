using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // calling when ever the play button is pressed    
    public void PlayGame()
    {
        // Loading the main game when the play button is pressed
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Calling when ever the quit button is pressed
    public void QuitGame ()
    {
        // This is used so when in unity engine when the quit button is pressed it tells the user the game has quit in the bottom

        Debug.Log("QUIT");
        // Making the game quit
        Application.Quit();
    }
}