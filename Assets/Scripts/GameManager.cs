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
            TogglePause();
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

    private void TogglePause()
    {
        if (currentState == GameOverState)
        {
            return;
        }

        IsPaused = !IsPaused;

        if (IsPaused)
        {
            RoadSpeed = 0f;
            Debug.Log("Game Paused");
        }
        else
        {
            Debug.Log("Game Resumed");

            if (currentState == DrivingState)
            {
                RoadSpeed = normalRoadSpeed;
            }
            else if (currentState == BoostedState)
            {
                RoadSpeed = boostedRoadSpeed;
            }
        }
    }
}
