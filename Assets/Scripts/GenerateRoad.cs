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
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (road == null)
        {
            return;
        }

        GameObject nextRoad = null;

        if (GameManager.Instance.IsTutorialActive)
        {
            nextRoad = GameManager.Instance.GetNextTutorialRoad();
        }
        else
        {
            GameObject[] roadPool = GameManager.Instance.roadSegments;

            if (roadPool.Length > 0)
            {
                int random = Random.Range(0, roadPool.Length);
                nextRoad = roadPool[random];
            }
        }

        if (nextRoad != null)
        {
            Instantiate(nextRoad, road.spawnPoint.position, road.spawnPoint.rotation);
        }
    }
}
