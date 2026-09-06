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

        GameObject[] roadPool;

        if (GameManager.Instance.IsTutorialActive)
        {
            roadPool = GameManager.Instance.tutorialRoadSegments;
        }
        else
        {
            roadPool = GameManager.Instance.roadSegments;
        }

        if (roadPool.Length > 0)
        {
            int random = Random.Range(0, roadPool.Length);

            Instantiate(roadPool[random], road.spawnPoint.position, road.spawnPoint.rotation);
        }
    }
}
