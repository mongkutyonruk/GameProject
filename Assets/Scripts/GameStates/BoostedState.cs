using UnityEngine;
using TMPro;

public class BoostedState : BaseState
{
    private float boostTimer;
    private TMP_Text boostTimerText;

    private Renderer[] playerRenderers;
    private MaterialPropertyBlock colorBlock;

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

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // find the TextMeshPro text somewhere under the player
            boostTimerText = player.GetComponentInChildren<TMP_Text>(true);

            if (boostTimerText != null)
            {
                boostTimerText.gameObject.SetActive(true);
                boostTimerText.text = boostTimer.ToString("F1");
            }

            // find every renderer belonging to the player
            playerRenderers = player.GetComponentsInChildren<Renderer>();

            colorBlock = new MaterialPropertyBlock();

            foreach (Renderer renderer in playerRenderers)
            {
                renderer.GetPropertyBlock(colorBlock);

                colorBlock.SetColor("_BaseColor", Color.yellow);

                renderer.SetPropertyBlock(colorBlock);
            }
        }
    }

    public override void Update()
    {
        boostTimer -= Time.deltaTime;

        if (boostTimerText != null)
        {
            boostTimerText.text = Mathf.Max(0f, boostTimer).ToString("F1");
        }

        if (boostTimer <= 0f)
        {
            gameManager.ChangeState(gameManager.DrivingState);
        }
    }

    public override void Exit()
    {
        IsInvincible = false;

        if (boostTimerText != null)
        {
            boostTimerText.gameObject.SetActive(false);
        }

        if (playerRenderers != null)
        {
            foreach (Renderer renderer in playerRenderers)
            {
                renderer.SetPropertyBlock(null);
            }
        }

        Debug.Log("Boost Ended");
    }
}