using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // provides a globally accessible reference to the game manager
    public static GameManager Instance;

    // stores the current speed at which the road moves
    public float RoadSpeed { get; set; }

    // stores the normal road speed used during regular driving
    public float normalRoadSpeed = 12f;
    // stores the faster road speed used while the player is boosted
    public float boostedRoadSpeed = 20f;
    // stores how long the player's boost lasts
    public float boostDuration = 5f;

    // stores the regular road segments used during the main game
    public GameObject[] roadSegments;

    // determines whether the tutorial road segments should be used
    public bool tutorialRoadsEnabled = true;

    // stores the road segments used specifically for the tutorial
    public GameObject[] tutorialRoadSegments;

    // keeps track of whether the tutorial is currently active
    public bool IsTutorialActive { get; private set; }

    // keeps track of which tutorial road segment should be provided next
    private int tutorialRoadIndex = 0;

    // stores the state that is currently active
    private BaseState currentState;

    // provides access to the currently active game state
    public BaseState CurrentState
    {
        get
        {
            return currentState;
        }
    }

    // keeps track of whether the game is currently paused
    public bool IsPaused { get; private set; }

    // determines whether the player is currently allowed to move
    public bool CanPlayerMove
    {
        get
        {
            // prevents player movement while the game is paused
            if (IsPaused)
            {
                return false;
            }

            // otherwise uses the movement setting from the current state
            return CurrentState.CanMovePlayer;
        }
    }

    // stores the different states used by the game
    public DrivingState DrivingState { get; private set; }
    public BoostedState BoostedState { get; private set; }
    public GameOverState GameOverState { get; private set; }

    // sends an event whenever the paused state changes
    public event System.Action<bool> PausedStateChanged;
    // sends an event whenever the game over state is reached
    public event System.Action<bool> GameOverStateChanged;

    // initializes the game manager and creates all available game states
    private void Awake()
    {
        // checks whether a game manager instance already exists
        if (Instance == null)
        {
            // sets this object as the shared game manager instance
            Instance = this;
        }
        else
        {
            // removes duplicate game manager objects
            Destroy(gameObject);
            return;
        }

        // creates the normal driving state
        DrivingState = new DrivingState(this);
        // creates the boosted state
        BoostedState = new BoostedState(this);
        // creates the game over state
        GameOverState = new GameOverState(this);
    }

    // sets up the initial game state when the game starts
    private void Start()
    {
        // enables or disables the tutorial based on the configured setting
        IsTutorialActive = tutorialRoadsEnabled;

        // starts the game in the normal driving state
        ChangeState(DrivingState);
    }

    // updates the active game state and handles the pause input
    private void Update()
    {
        // checks whether the escape key was pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                // resumes the game if it is currently paused
                ResumeGame();
            }
            else
            {
                // pauses the game if it is currently running
                PauseGame();
            }
        }

        // updates the current state while the game is not paused
        if (!IsPaused && currentState != null)
        {
            currentState.Update();
        }
    }

    // changes the game from the current state to a new state
    public void ChangeState(BaseState newState)
    {
        // exits the previous state before changing to the new one
        if (currentState != null)
        {
            currentState.Exit();
        }

        // sets the new state as the active state
        currentState = newState;

        // runs the setup code for the new state
        currentState.Enter();

        // notifies other systems when the game enters the game over state
        if (currentState == GameOverState)
        {
            GameOverStateChanged?.Invoke(true);
        }

        // displays the name of the new state in the console
        Debug.Log("State changed to: " + currentState.GetType().Name);
    }

    // pauses the game and prevents gameplay from continuing
    public void PauseGame()
    {
        // prevents the game over state from being paused
        if (currentState == GameOverState)
        {
            return;
        }

        // marks the game as paused
        IsPaused = true;
        // stops the progression of game time
        Time.timeScale = 0f;
        // notifies other systems that the game has been paused
        if (PausedStateChanged != null)
        {
            PausedStateChanged.Invoke(true);
        }

        // displays a message in the console when the game is paused
        Debug.Log("Game Paused");
    }

    // resumes the game after it has been paused
    public void ResumeGame()
    {
        // marks the game as no longer paused
        IsPaused = false;
        // restores the normal progression of game time
        Time.timeScale = 1f;
        // notifies other systems that the game has resumed
        if (PausedStateChanged != null)
        {
            PausedStateChanged.Invoke(false);
        }

        // displays a message in the console when the game resumes
        Debug.Log("Game Resumed");
    }

    // returns the next available tutorial road segment
    public GameObject GetNextTutorialRoad()
    {
        // checks whether all tutorial road segments have already been used
        if (tutorialRoadIndex >= tutorialRoadSegments.Length)
        {
            return null;
        }

        // gets the current tutorial road segment
        GameObject road = tutorialRoadSegments[tutorialRoadIndex];

        // moves the index to the next tutorial road segment
        tutorialRoadIndex++;

        // returns the selected tutorial road segment
        return road;
    }

    // ends the tutorial and allows the main game to continue
    public void EndTutorial()
    {
        // marks the tutorial as no longer active
        IsTutorialActive = false;

        // displays a message in the console when the tutorial ends
        Debug.Log("Tutorial ended");
    }

    // immediately stops the road from moving
    public void FreezeRoad()
    {
        // sets the road speed to zero
        RoadSpeed = 0f;
    }

    // restores the appropriate road speed based on the current game state
    public void ResumeRoad()
    {
        // restores the boosted road speed if the player is currently boosted
        if (CurrentState == BoostedState)
        {
            RoadSpeed = boostedRoadSpeed;
        }
        // restores the normal road speed if the player is normally driving
        else if (CurrentState == DrivingState)
        {
            RoadSpeed = normalRoadSpeed;
        }
    }
}
