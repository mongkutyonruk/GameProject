using UnityEngine;

public class DrivingState : BaseState
{
    // creates the driving state and gives it access to the game manager
    public DrivingState(GameManager gameManager) : base(gameManager)
    {
    }

    // sets up the game when the player enters the normal driving staet
    public override void Enter()
    {
        // displays a message in the console when normal driving begins
        Debug.Log("Started Driving");

        // sets the road speed back to the normal driving speed
        gameManager.RoadSpeed = gameManager.normalRoadSpeed;

        // allows the player to move while in the driving state
        CanMovePlayer = true;
    }
}