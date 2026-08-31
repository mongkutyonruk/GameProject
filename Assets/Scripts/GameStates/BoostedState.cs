using UnityEngine;

public class BoostedState : BaseState
{
    private float boostTimer;

    private Renderer[] playerRenderers;
    private Color[] originalColors;

    public BoostedState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Enter()
    {
        Debug.Log("Started Boosting");
        gameManager.RoadSpeed = gameManager.boostedRoadSpeed;

        CanMovePlayer = true;
        IsInvincible = true;

        boostTimer = gameManager.boostDuration;

        //remove later only for debug (changes car color when boosting)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerRenderers = player.GetComponentsInChildren<Renderer>();

            originalColors = new Color[playerRenderers.Length];

            for (int i = 0; i < playerRenderers.Length; i++)
            {
                originalColors[i] = playerRenderers[i].material.color;
                playerRenderers[i].material.color = Color.green;
            }
        }
    }

    public override void Update()
    {
        boostTimer -= Time.deltaTime;

        if (boostTimer <= 0f)
        {
            gameManager.ChangeState(gameManager.DrivingState);
        }
    }

    public override void Exit()
    {
        IsInvincible = false;

        if (playerRenderers != null)
        {
            for (int i = 0; i < playerRenderers.Length; i++)
            {
                playerRenderers[i].material.color = originalColors[i];
            }
        }

        Debug.Log("Boost Ended");
    }
}