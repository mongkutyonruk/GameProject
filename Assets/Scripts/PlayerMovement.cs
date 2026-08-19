using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed = 5f;
    [SerializeField] 
    private float leftLimit = -5f;
    [SerializeField] 
    private float rightLimit = 5f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        transform.position += new Vector3(1, 0, 0) * horizontal * moveSpeed * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, leftLimit, rightLimit);
        transform.position = pos;
    }
}
