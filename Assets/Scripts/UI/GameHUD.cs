using UnityEngine;
using TMPro;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance;

    [SerializeField] private TMP_Text tokenText;
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private float distanceMult = 0.5f;

    private float distance = 0f;
    private int tokenCount = 0;

    private bool scoreStarted = false;
    public bool ScoreStarted
    {
        get { return scoreStarted; }
    }

    public float Distance
    {
        get { return distance; }
    }
    
    public int TokenCount
    {
        get { return tokenCount; }
    }

    private void Start()
    {
        if (!GameManager.Instance.tutorialRoadsEnabled)
        {
            StartScore();
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void StartScore()
    {
        scoreStarted = true;
    }

    private void Update()
    {
        if (!scoreStarted)
        {
            return;
        }

        distance += GameManager.Instance.RoadSpeed * Time.deltaTime * distanceMult;
        distanceText.text = Mathf.FloorToInt(distance).ToString();
    }

    public void UpdateTokenCount(int tokenCount)
    {
        if (!scoreStarted)
        {
            return;
        }

        this.tokenCount = tokenCount;
        tokenText.text = tokenCount.ToString();
    }
}
