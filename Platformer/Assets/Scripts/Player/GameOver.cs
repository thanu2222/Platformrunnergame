using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
 

    public void Restart()
    {
        // Function to be called when the restart button is pressed
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void Home(int SceneID)
    {
        // The game unfreezes when the home button is pressed
        Time.timeScale = 1f;
        // loads the main menu scene when the home button is pressed
        SceneManager.LoadScene(SceneID);
    }
}