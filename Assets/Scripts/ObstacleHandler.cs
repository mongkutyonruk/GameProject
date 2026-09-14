using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    // checks whether the player has collided with an obstacle
    private void OnTriggerEnter(Collider other)
    {
        // checks whether the object entering the trigger is tagged as an obstacle
        if (other.CompareTag("Obstacle"))
        {
            // ignores the collision if the player is currently invincible
            if (GameManager.Instance.CurrentState.IsInvincible)
            {
                return;
            }

            // displays a message in the console when the player hits an obstacle
            Debug.Log("PLAYER HIT OBSTACLE!");

            // plays the crash sound before the game over screen appears
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayCrash();
            }

            // changes the game to the game over state after the player is hit
            GameManager.Instance.ChangeState(GameManager.Instance.GameOverState);
        }
    }
}
