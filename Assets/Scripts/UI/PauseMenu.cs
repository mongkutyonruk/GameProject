using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // stores the pause menu panel that is shown when the game is paused
    [SerializeField] private GameObject pausePanel;

    // subscribes the pause menu to pause state changes when the object starts
    private void Start()
    {
        // checks that the game manager exists before trying to subscribe to its event
        if (GameManager.Instance != null)
        {
            // listens for change to the paused state so the pause panel can be updated
            GameManager.Instance.PausedStateChanged += HandlePausedStateChange;
        }
    }

    // removes the pause menu from the pause state event when the object is destroyed
    private void OnDestroy()
    {
        // checks that the game manager still exists before removing the event subscription
        if (GameManager.Instance != null)
        {
            // stops listening for pause state changes to prevent references to this object
            GameManager.Instance.PausedStateChanged -= HandlePausedStateChange;
        }
    }

    // updates the visibility of the pause panel based on whether the game is paused
    private void HandlePausedStateChange(bool isPaused)
    {
        // shows the pause panel when paused and hides it when the game resumes
        pausePanel.SetActive(isPaused);
    }

    // resumes the game when the player presses the resume button
    public void Resume()
    {
        // tells the game manager to resume the game and restore normal gameplay
        GameManager.Instance.ResumeGame();
    }

    // restarts the current scene from the beginning
    public void Restart()
    {
        // makes sure the scene is not reloaded while time is still paused
        Time.timeScale = 1f;
        // reloads the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // returns the player to the main menu
    public void Quit()
    {
        // makes sure the main menu is not loaded while time is still paused
        Time.timeScale = 1f;
        // loads the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
