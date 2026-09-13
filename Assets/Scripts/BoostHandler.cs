using UnityEngine;

public class BoostHandler : MonoBehaviour
{
    // checks whether the player has collected a boost token
    private void OnTriggerEnter(Collider other)
    {
        // checks whether the object entering the trigger is tagged as a boost
        if (other.CompareTag("Boost"))
        {
            // displays a message in the console when the player collects a boost token
            Debug.Log("Player collected a boost token!");

            // changes the game to the boosted state
            GameManager.Instance.ChangeState(GameManager.Instance.BoostedState);

            // removes the collected boost token from the scene
            Destroy(other.gameObject);
        }
    }
}
