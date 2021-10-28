using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BirdMover))]
public class Bird : MonoBehaviour
{
    private BirdMover _mover;
    private int _score;

    public UnityAction GameOver;
    public UnityAction<int> ScoreChanged;

    private void Start()
    {
        _mover = GetComponent<BirdMover>();
    }

    public void ResetPlayer() 
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
        _mover.ResetBird();
    }

    public void IncreaseScore() 
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }

    public void Die() 
    {
        GameOver?.Invoke();
    }
}
