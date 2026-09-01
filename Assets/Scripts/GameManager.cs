using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float RoadSpeed { get; set; }

    public float normalRoadSpeed = 12f;
    public float boostedRoadSpeed = 20f;
    public float boostDuration = 5f;

    public GameObject[] roadSegments;

    private BaseState currentState;

    public BaseState CurrentState
    {
        get
        {
            return currentState;
        }
    }

    public bool IsPaused { get; private set; }

    public bool CanPlayerMove
    {
        get
        {
            if (IsPaused)
            {
                return false;
            }

            return CurrentState.CanMovePlayer;
        }
    }

    public DrivingState DrivingState { get; private set; }
    public BoostedState BoostedState { get; private set; }
    public GameOverState GameOverState { get; private set; }

    public event System.Action<bool> PausedState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DrivingState = new DrivingState(this);
        BoostedState = new BoostedState(this);
        GameOverState = new GameOverState(this);
    }

    private void Start()
    {
        ChangeState(DrivingState);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (!IsPaused && currentState != null)
        {
            currentState.Update();
        }
    }

    public void ChangeState(BaseState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter();

        Debug.Log("State changed to: " + currentState.GetType().Name);
    }

    public void PauseGame()
    {
        if (currentState == GameOverState)
        {
            return;
        }

        IsPaused = true;
        Time.timeScale = 0f;
        PausedState?.Invoke(true);

        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        PausedState?.Invoke(false);

        Debug.Log("Game Resumed");
    }
}
