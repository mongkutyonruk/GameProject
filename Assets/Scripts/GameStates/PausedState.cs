using UnityEngine;

public class PausedState : BaseState
{
    public PausedState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Enter()
    {
        Debug.Log("Game Paused");
        gameManager.RoadSpeed = 0f;
        CanMovePlayer = false;
    }
}