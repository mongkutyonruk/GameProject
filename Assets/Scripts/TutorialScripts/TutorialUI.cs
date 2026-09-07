using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialPanel;

    [SerializeField]
    private TMP_Text tutorialText;

    private void Start()
    {
        tutorialPanel.SetActive(GameManager.Instance.tutorialRoadsEnabled);
    }

    public void ShowTutorial(string message)
    {
        tutorialText.text = message;
        tutorialPanel.SetActive(true);
    }

    public void HideTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}
