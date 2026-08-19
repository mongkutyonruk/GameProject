using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("PLAYER HIT OBSTACLE!");
        }
    }
}
