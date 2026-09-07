using UnityEngine;

public class TokenHandler : MonoBehaviour
{
    private int tokenCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Present"))
        {
            return;
        }

        if (!GameHUD.Instance.ScoreStarted)
        {
            Destroy(other.gameObject);
            return;
        }

        tokenCount++;

        GameHUD.Instance.UpdateTokenCount(tokenCount);

        Destroy(other.gameObject);

        Debug.Log("Tokens collected: " + tokenCount);
    }
}