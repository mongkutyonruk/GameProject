using UnityEngine;

public class TokenHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Present"))
        {
            Debug.Log("Player collected a present token!");
            //GameManager.Instance.ChangeState(GameManager.Instance.BoostedState);
        }
    }
}

           /* if (GameManager.Instance.CurrentState.IsInvincible)
            {
                return;
            }

            Debug.Log("PLAYER HIT OBSTACLE!");
            GameManager.Instance.ChangeState(GameManager.Instance.GameOverState);
        }*/