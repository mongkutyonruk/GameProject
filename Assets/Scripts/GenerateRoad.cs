using UnityEngine;

public class GenerateRoad : MonoBehaviour
{
    public GameObject[] roadSegments;
    private RoadSegment road;

    private void Awake()
    {
        road = GetComponentInParent<RoadSegment>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (road != null && roadSegments.Length > 0)
            {
                int random = Random.Range(0, roadSegments.Length);
                Instantiate(roadSegments[random], road.spawnPoint.position, road.spawnPoint.rotation);
            }
        }
    }
}
