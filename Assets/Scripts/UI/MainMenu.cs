using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // loads the main game scene when the player starts the game
    public void LoadGame()
    {
        // plays the button click sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayClick();
        }

        // loads the scene containing the main gameplay
        SceneManager.LoadScene(1);
    }

    // closes the application when the player chooses to quit
    public void QuitGame()
    {
        // plays the button click sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayClick();
        }

        // exits the application
        Application.Quit();
    }
}
