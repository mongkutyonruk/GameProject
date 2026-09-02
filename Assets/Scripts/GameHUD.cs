using UnityEngine;
using TMPro;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance;

    [SerializeField] private TMP_Text tokenText;
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private float distanceMult = 0.5f;

    private float distance = 0f;

    public float Distance
    {
        get { return distance; }
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
        tokenText.text = tokenCount.ToString();
    }
}
