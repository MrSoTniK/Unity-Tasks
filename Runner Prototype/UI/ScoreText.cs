using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ScoreText : MonoBehaviour
{
    [SerializeField] private Squad _squad;
    [SerializeField] private int _points;

    private int _score;
    private TMP_Text _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();
        _scoreText.text = "0";
        _score = 0;
    }

    private void OnEnable()
    {
        _squad.ChangeScore += ChangeScore;
    }

    private void OnDisable()
    {
        _squad.ChangeScore -= ChangeScore;
    }

    private void ChangeScore() 
    {
        _score += _points;
        _scoreText.text = _score.ToString();
    }
}