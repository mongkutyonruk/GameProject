using UnityEngine;

public class TutorialRoad : MonoBehaviour
{
    [TextArea]
    public string tutorialMessage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialUI tutorialUI = FindFirstObjectByType<TutorialUI>();

            if (tutorialUI != null)
            {
                tutorialUI.ShowTutorial(tutorialMessage);
            }
        }
    }
}
