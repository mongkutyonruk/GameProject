using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float RoadSpeed { get; set; }

    public float normalRoadSpeed = 12f;
    public float boostedRoadSpeed = 20f;

    public GameObject[] roadSegments;

    private BaseState currentState;

    public BaseState CurrentState
    {
        get
        {
            return currentState;
        }
    }

    public DrivingState DrivingState { get; private set; }
    public BoostedState BoostedState { get; private set; }
    public PausedState PausedState { get; private set; }
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
        PausedState = new PausedState(this);
        GameOverState = new GameOverState(this);
    }

    private void Start()
    {
        ChangeState(DrivingState);
    }

    private void Update()
    {
        currentState?.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == PausedState)
                ChangeState(DrivingState);
            else
                ChangeState(PausedState);
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
}
