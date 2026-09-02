using UnityEngine;

public class TokenHandler : MonoBehaviour
{
    private int tokenCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Present"))
        {
            tokenCount++;
            GameHUD.Instance.UpdateTokenCount(tokenCount);
            Destroy(other.gameObject);

            Debug.Log("Tokens collected: " + tokenCount);
        }
    }
}