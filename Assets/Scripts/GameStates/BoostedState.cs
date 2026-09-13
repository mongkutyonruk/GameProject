using UnityEngine;
using TMPro;

public class BoostedState : BaseState
{
    // stores the remaining amount of time for the current boost
    private float boostTimer;
    // stores the text component used to display the remaining boost time above the player
    private TMP_Text boostTimerText;

    // stores all renderers belonging to the player so their appearance can be changed during the boost
    private Renderer[] playerRenderers;

    // stores temporary color changes applied to the player's renderers without modifying their materials
    private MaterialPropertyBlock colorBlock;

    // creates the boosted state and gives it access to the game manager
    public BoostedState(GameManager gameManager) : base(gameManager)
    {
    }

    // sets up the player and game systems when the boost begins
    public override void Enter()
    {
        // displays a message in the console when boosting begins
        Debug.Log("Started Boosting");

        // changes the road speed to the faster boosted speed
        gameManager.RoadSpeed = gameManager.boostedRoadSpeed;

        // allows the player to move while boosted
        CanMovePlayer = true;
        // prevents the player from being affected by obstacles while boosted
        IsInvincible = true;

        // sets the boost timer to the duration configured in the game manager
        boostTimer = gameManager.boostDuration;

        // finds the player using the player tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // finds the TextMeshPro text used to display the boost timer
            boostTimerText = player.GetComponentInChildren<TMP_Text>(true);

            if (boostTimerText != null)
            {
                // makes the boost timer visible when boosting begins
                boostTimerText.gameObject.SetActive(true);

                // displays the starting boost duration above the player
                boostTimerText.text = boostTimer.ToString("F1");
            }

            // finds every renderer belonging to the player and its child objects
            playerRenderers = player.GetComponentsInChildren<Renderer>();

            // creates a property block for changing renderer properties without modifying the original materials
            colorBlock = new MaterialPropertyBlock();

            foreach (Renderer renderer in playerRenderers)
            {
                // gets the currnt property values from the renderer
                renderer.GetPropertyBlock(colorBlock);

                // changes the player's displayed color to yellow while boosted
                colorBlock.SetColor("_BaseColor", Color.yellow);

                // applies the yellow color to the renderer
                renderer.SetPropertyBlock(colorBlock);
            }
        }
    }

    // updates the boost timer every frame while the boosted state is active
    public override void Update()
    {
        // decreases the remaining boost time based on the time between frames
        boostTimer -= Time.deltaTime;

        if (boostTimerText != null)
        {
            // updates the timer displayed above the player and prevents it from showing a negative value
            boostTimerText.text = Mathf.Max(0f, boostTimer).ToString("F1");
        }

        if (boostTimer <= 0f)
        {
            // returns the player to the normal driving state when the boost expires
            gameManager.ChangeState(gameManager.DrivingState);
        }
    }

    // resets the player and game systems when the boost ends
    public override void Exit()
    {
        // removes the player's invincibility when the boost ends
        IsInvincible = false;

        if (boostTimerText != null)
        {
            // hides the boost timer after the boost ends
            boostTimerText.gameObject.SetActive(false);
        }

        if (playerRenderers != null)
        {
            foreach (Renderer renderer in playerRenderers)
            {
                // removes the temporary color change and restores the renderer's original appearance
                renderer.SetPropertyBlock(null);
            }
        }

        // displays a message in the console when boosting ends
        Debug.Log("Boost Ended");
    }
}