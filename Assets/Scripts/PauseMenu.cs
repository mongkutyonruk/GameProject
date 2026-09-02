using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private void Start()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.PausedStateChanged += HandlePausedStateChange;
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PausedStateChanged -= HandlePausedStateChange;
        }
    }

    private void HandlePausedStateChange(bool isPaused)
    {
        pausePanel.SetActive(isPaused);
    }

    public void Resume()
    {
        GameManager.Instance.ResumeGame();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
