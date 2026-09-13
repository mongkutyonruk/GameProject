using UnityEngine;

public abstract class BaseState
{
    // stores a reference to the game manager so each state can access and control shared game systems
    protected GameManager gameManager;

    // determines whether the player is allowed to move while this state is active
    public bool CanMovePlayer { get; protected set; }
    // determines whether the player is protected from damage or other hazards while this state is active
    public bool IsInvincible { get; protected set; }

    // creates the state and gives it access to the game manager
    public BaseState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    // called when the state becomes active and can be overridden by individual states
    public virtual void Enter() { }
    // called every frame while the state is active and can be overridden by individual states
    public virtual void Update() { }
    // called when the state is no longer active and can be overridden by individual states
    public virtual void Exit() { }
}
