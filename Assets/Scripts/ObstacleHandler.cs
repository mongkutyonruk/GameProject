using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (GameManager.Instance.CurrentState.IsInvincible)
            {
                return;
            }

            Debug.Log("PLAYER HIT OBSTACLE!");
            GameManager.Instance.ChangeState(GameManager.Instance.GameOverState);
        }
    }
}
