using UnityEngine;

public class GenerateRoad : MonoBehaviour
{
    private RoadSegment road;

    private void Awake()
    {
        road = GetComponentInParent<RoadSegment>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (road != null && GameManager.Instance.roadSegments.Length > 0)
            {
                int random = Random.Range(0, GameManager.Instance.roadSegments.Length);
                Instantiate(GameManager.Instance.roadSegments[random], road.spawnPoint.position, road.spawnPoint.rotation);
            }
        }
    }
}
