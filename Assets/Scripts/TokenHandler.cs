using UnityEngine;

public class TokenHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Present"))
        {
            Debug.Log("Player collected a present token!");
        }
    }
}