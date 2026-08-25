using UnityEngine;

public class BoostedState : BaseState
{
    public BoostedState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Enter()
    {
        Debug.Log("Started Boosting");
        gameManager.RoadSpeed = gameManager.boostedRoadSpeed;
        CanMovePlayer = true;
    }
}