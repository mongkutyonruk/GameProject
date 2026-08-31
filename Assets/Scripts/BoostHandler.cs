using UnityEngine;

public class BoostHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boost"))
        {
            Debug.Log("Player collected a boost token!");
            GameManager.Instance.ChangeState(GameManager.Instance.BoostedState);
        }
    }
}
