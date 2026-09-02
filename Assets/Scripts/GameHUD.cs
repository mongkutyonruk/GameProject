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

    public float Distance
    {
        get { return distance; }
    }
    
    public int TokenCount
    {
        get { return tokenCount; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        distance += GameManager.Instance.RoadSpeed * Time.deltaTime * distanceMult;
        distanceText.text = Mathf.FloorToInt(distance).ToString();
    }

    public void UpdateTokenCount(int tokenCount)
    {
        this.tokenCount = tokenCount;
        tokenText.text = tokenCount.ToString();
    }
}
