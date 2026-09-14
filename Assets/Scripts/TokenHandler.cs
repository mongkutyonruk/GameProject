using UnityEngine;

public class TokenHandler : MonoBehaviour
{
    // keeps track of the number of tokens collected by the player
    private int tokenCount = 0;

    // checks whether the player has collected a token
    private void OnTriggerEnter(Collider other)
    {
        // ignores objects that are not tagged as presents
        if (!other.CompareTag("Present"))
        {
            return;
        }

        // removes the present without counting it if the score system has not started
        if (!GameHUD.Instance.ScoreStarted)
        {
            Destroy(other.gameObject);
            return;
        }

        // increases the token count when a valid token is collected
        tokenCount++;

        // updates the token count displayed on the game hud
        GameHUD.Instance.UpdateTokenCount(tokenCount);

        // plays the present collection sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayPresent();
        }

        // removes the collected present from the scene
        Destroy(other.gameObject);

        // displays the current number of collected tokens in the console
        Debug.Log("Tokens collected: " + tokenCount);
    }
}