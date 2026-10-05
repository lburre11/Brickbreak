using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreUI;
    [SerializeField] private TMP_Text playerLivesUI;

    private int _lives;
    public int Lives
    {
        get => _lives;
        set
        {
            _lives = value;
            playerLivesUI.SetText(_lives.ToString());
        }
    }

    private int _score;
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            _scoreUI.SetText(_score.ToString());
        }
    }
}
