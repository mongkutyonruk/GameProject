using UnityEngine;

public abstract class BaseState
{
    protected GameManager gameManager;

    public bool CanMovePlayer { get; protected set; }

    public BaseState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
