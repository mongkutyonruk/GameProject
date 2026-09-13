using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ocntrols how quickly the player moves horizontally
    [SerializeField] 
    private float moveSpeed = 5f;
    // sets the furthest left position the player can move to
    [SerializeField] 
    private float leftLimit = -5f;
    [SerializeField]
    // sets the furthest right position the player can move to
    private float rightLimit = 5f;

    // checks for player input and updates the player's horizontal movement
    void Update()
    {
        // prevents the player from moving when the current game state does not allow movement
        if (!GameManager.Instance.CanPlayerMove)
        {
            return;
        }

        // gets the player's horizontal input from the keyboard or configured input device
        float horizontal = Input.GetAxis("Horizontal");

        // moves the player horizontally based on the input, movement speed, and frame time
        transform.position += new Vector3(1, 0, 0) * horizontal * moveSpeed * Time.deltaTime;

        // stores the player's current position so the horizontal position can be limited
        Vector3 pos = transform.position;

        // prevents the player from moving beyond the defined left and right boundaries
        pos.x = Mathf.Clamp(pos.x, leftLimit, rightLimit);

        // applies the limited position back to the player
        transform.position = pos;
    }
}
