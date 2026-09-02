using UnityEngine;

public class GameOverState : BaseState
{
    public GameOverState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Enter()
    {
        Debug.Log("Game Over");
        //gameManager.RoadSpeed = 0f;
        CanMovePlayer = false;
        IsInvincible = false;
        Time.timeScale = 0f;
    }
}