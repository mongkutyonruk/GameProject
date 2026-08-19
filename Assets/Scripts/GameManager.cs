using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float roadSpeed = 2f;

    public GameObject[] roadSegments;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
