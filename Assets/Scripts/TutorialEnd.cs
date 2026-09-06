using UnityEngine;

public class TutorialEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.EndTutorial();
        }
    }
}
