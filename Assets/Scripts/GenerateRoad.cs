using UnityEngine;

public class GenerateRoad : MonoBehaviour
{
    public GameObject roadSegment;
    private RoadSegment road;

    private void Awake()
    {
        road = GetComponentInParent<RoadSegment>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (road != null)
            {
                Instantiate(roadSegment, road.spawnPoint.position, road.spawnPoint.rotation);
            }
        }
    }
}
