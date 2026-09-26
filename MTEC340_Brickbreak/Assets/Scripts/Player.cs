using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreUI;
    //backing variable
    private int _score;
    //public variable
    public int Score
    {
        //get => _score;
        get
        {
            return _score;
        }
        //set => _score = value;
        set
        {
            _score = value;
            _scoreUI.SetText(Score.ToString());
        }
    }
}