using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOverStateChanged += HandleGameOverStateChange;
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOverStateChanged -= HandleGameOverStateChange;
        }
    }

    private void HandleGameOverStateChange(bool isGameOver)
    {
        gameOverPanel.SetActive(isGameOver);
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
