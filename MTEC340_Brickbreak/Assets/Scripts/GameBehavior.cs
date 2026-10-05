using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;
	private Utilities.GameState _state;
	[SerializeField] private int playerLives = 3;
    public Utilities.GameState State
    {
        get => _state;
        set
        {
            _state = value;
            _pauseUI.enabled = State == Utilities.GameState.Pause;
        }
    }
    public Player[] Players = new Player[1];
	[SerializeField] private TMP_Text _pauseUI;

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
        State = Utilities.GameState.Play;
        ResetGame();
    }

    private void Update()
    {
        //state machine transition
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            State = State == Utilities.GameState.Play ?
                Utilities.GameState.Pause :
                Utilities.GameState.Play;
        }
    }

	 public bool Death()
    {
        playerLives--;
	     foreach (Player p in Players)
        {
	         p.Lives = playerLives;
	     }

	     if (playerLives <= 0)
	     {
	         SceneManager.LoadScene("GameOver");
            return false;
        }

        return true;
    }

    void ResetGame()
    {
        foreach (Player p in Players)
        {
            p.Score = 0;
            p.Lives = playerLives;
        }
    }

    public void ScorePoint(int playerNumber)
    {
        Players[playerNumber].Score += 100;
    }
}
