using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // loads the main game scene when the player starts the game
    public void LoadGame()
    {
        // loads the scene containing the main gameplay
        SceneManager.LoadScene("SampleScene");
    }

    // closes the application when the player chooses to quit
    public void QuitGame()
    {
        // exits the application
        Application.Quit();
    }
}
