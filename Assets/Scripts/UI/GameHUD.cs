using UnityEngine;
using TMPro;

public class GameHUD : MonoBehaviour
{
    // provides a globally accessible reference to the game hud
    public static GameHUD Instance;

    // references the text used to display the player's collected token count
    [SerializeField] private TMP_Text tokenText;
    // references the text used to display the player's travelled distance
    [SerializeField] private TMP_Text distanceText;
    // controls how quickly the displayed distance increases relative to the road speed
    [SerializeField] private float distanceMult = 0.5f;

    // stores the player's current travelled distance
    private float distance = 0f;
    // stores the number of tokens collected by the player
    private int tokenCount = 0;

    // keeps track of whether score tracking has started
    private bool scoreStarted = false;
    // provides access to whether score tracking has started
    public bool ScoreStarted
    {
        get { return scoreStarted; }
    }

    // provides access to the player's current travelled distance
    public float Distance
    {
        get { return distance; }
    }

    // provides access to the player's current token count
    public int TokenCount
    {
        get { return tokenCount; }
    }

    // checks whether the tutorial is disabled and starts score tracking immediately if so
    private void Start()
    {
        // starts tracking the score immediately when there are no tutorial roads
        if (!GameManager.Instance.tutorialRoadsEnabled)
        {
            StartScore();
        }
    }

    // initializes the game hud instance when the object is created
    private void Awake()
    {
        // sets this game hud as the shared instance
        Instance = this;
    }

    // starts tracking the player's score
    public void StartScore()
    {
        // enables distance and token score tracking
        scoreStarted = true;
    }

    // updates the player's distance score every frame
    private void Update()
    {
        // prevents the score from updating before score tracking has started
        if (!scoreStarted)
        {
            return;
        }
        // increases the travelled distance based on the current road speed and elapsed time
        distance += GameManager.Instance.RoadSpeed * Time.deltaTime * distanceMult;
        // updates the distance displayed on the game hud using a whole number
        distanceText.text = Mathf.FloorToInt(distance).ToString();
    }

    // updates the number of tokens displayed on the game hud
    public void UpdateTokenCount(int tokenCount)
    {
        // prevents tokens from being counted before score tracking has started
        if (!scoreStarted)
        {
            return;
        }

        // stores the updated token count
        this.tokenCount = tokenCount;
        // updates the token count displayed on the game hud
        tokenText.text = tokenCount.ToString();
    }
}
