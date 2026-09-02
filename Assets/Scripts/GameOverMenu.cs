using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalDistanceText;
    [SerializeField] private TMP_Text finalTokenCount;

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

        if (isGameOver)
        {
            finalDistanceText.text = Mathf.FloorToInt(GameHUD.Instance.Distance).ToString();
            finalTokenCount.text = GameHUD.Instance.TokenCount.ToString();
        }
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
