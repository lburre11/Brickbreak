using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;
    public Player[] Players = new Player[1];
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("New Instance Initialized...");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate instance detected and eradicated...");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetGame();
    }

    void ResetGame()
    {
        foreach (Player p in Players)
        {
            p.Score = 0;
        }
    }

    public void ScorePoint(int playerNumber)
    {
        Players[playerNumber].Score += 100;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
