using UnityEngine;

public class GenerateRoad : MonoBehaviour
{
    // stores the road segment that this trigger belongs to
    private RoadSegment road;

    // finds the road segment that contains this trigger when the object is initialized
    private void Awake()
    {
        // gets the RoadSegment component from a parent object
        road = GetComponentInParent<RoadSegment>();
    }

    // checks whether the player has entered the road generation trigger
    private void OnTriggerEnter(Collider other)
    {
        // ignores objects that are not tagged as the player
        if (!other.CompareTag("Player"))
        {
            return;
        }

        // stops the road from being generated if the trigger is not connected to a road segment
        if (road == null)
        {
            return;
        }

        // stores the next road segment that will be generated
        GameObject nextRoad = null;

        // checks whether the tutorial is currently active
        if (GameManager.Instance.IsTutorialActive)
        {
            // gets the next road segment from the tutorial road sequence
            nextRoad = GameManager.Instance.GetNextTutorialRoad();
        }
        else
        {
            // gets the collection of regular road segments available for generation
            GameObject[] roadPool = GameManager.Instance.roadSegments;

            // makes sure there are regular road segments available
            if (roadPool.Length > 0)
            {
                // selects a random road segment from the available road pool
                int random = Random.Range(0, roadPool.Length);
                // stores the randomly selected road segment
                nextRoad = roadPool[random];
            }
        }

        // creates the next road segment if a valid road was selected
        if (nextRoad != null)
        {
            // creates the road at the current road segment's spawn point and rotation
            Instantiate(nextRoad, road.spawnPoint.position, road.spawnPoint.rotation);
        }
    }
}
