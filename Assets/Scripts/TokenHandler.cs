using UnityEngine;

public class TokenHandler : MonoBehaviour
{
    private int tokenCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Present"))
        {
            tokenCount++;
            Debug.Log("Tokens collected: " + tokenCount);
        }
        Destroy(other.gameObject);
    }
}