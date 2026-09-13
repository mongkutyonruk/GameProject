using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    // stores the game over panel that is shown when the game ends
    [SerializeField] private GameObject gameOverPanel;
    // displays the final distance reached by the player
    [SerializeField] private TMP_Text finalDistanceText;
    // displays the final number of tokens collected by the player
    [SerializeField] private TMP_Text finalTokenCount;

    // subscribes the game over menu to game over state changes when the object starts
    private void Start()
    {
        // checks that the game manager exists before trying to subscribe to its event
        if (GameManager.Instance != null)
        {
            // listens for changes to the game over state so the panel and final score can be updated
            GameManager.Instance.GameOverStateChanged += HandleGameOverStateChange;
        }
    }

    // removes the game over menu from the game over event when the object is destroyed
    private void OnDestroy()
    {
        // checks that the game manager still exists before removing the event subscription
        if (GameManager.Instance != null)
        {
            // stops listening for game over state changes to prevent references to this object
            GameManager.Instance.GameOverStateChanged -= HandleGameOverStateChange;
        }
    }

    // updates the game over panel and final score information when the game ends
    private void HandleGameOverStateChange(bool isGameOver)
    {
        // shows the game over panel when the game is over and hides it otherwise
        gameOverPanel.SetActive(isGameOver);

        // only updates the final score information when the game has actually ended
        if (isGameOver)
        {
            // displays the final distance as a whole number
            finalDistanceText.text = Mathf.FloorToInt(GameHUD.Instance.Distance).ToString();
            // displays the final number of tokens collected
            finalTokenCount.text = GameHUD.Instance.TokenCount.ToString();
        }
    }

    // restarts the current game scene from the beginning
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
