using UnityEngine;

public class DrivingState : BaseState
{
    public DrivingState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Enter()
    {
        Debug.Log("Started Driving");
        gameManager.RoadSpeed = gameManager.normalRoadSpeed;
        CanMovePlayer = true;
    }
}