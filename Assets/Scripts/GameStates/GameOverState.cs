using UnityEngine;

public class GameOverState : BaseState
{
    // creates the game over state and gives it access to the game manager
    public GameOverState(GameManager gameManager) : base(gameManager)
    {
    }

    // sets up the game when the player enters the game over state
    public override void Enter()
    {
        // displays a message in the console when the game ends
        Debug.Log("Game Over");
        // prevents the player from moving after the game has ended
        CanMovePlayer = false;
        // removes invincibility so the player is no longer protected
        IsInvincible = false;
        // pauses the game by stopping the progression of time
        Time.timeScale = 0f;
    }
}