using UnityEngine;

public class TutorialUIEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialUI tutorialUI = FindFirstObjectByType<TutorialUI>();

            if (tutorialUI != null)
            {
                tutorialUI.HideTutorial();
            }

            GameHUD.Instance.StartScore();
        }
    }
}
