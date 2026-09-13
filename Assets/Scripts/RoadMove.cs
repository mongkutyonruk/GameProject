using UnityEngine;

public class RoadMove : MonoBehaviour
{
    // moves the road segment toward the player based on the current road speed
    void Update()
    {
        // moves the road backward along the z axis to create the effect of the player moving forward
        transform.position += new Vector3(0, 0, -1) * GameManager.Instance.RoadSpeed * Time.deltaTime;
    }

    // checks whether the road segment has reached the trigger used for destruction
    private void OnTriggerEnter(Collider other)
    {
        // checks whether the object entering the trigger is tagged for road destruction
        if (other.gameObject.CompareTag("Destroy"))
        {
            // removes the road segment from the scene once it is no longer needed
            Destroy(gameObject);
        }
    }
}
