using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class Counter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;

    private TMP_Text _textField;
    private int _currentCountValue;

    public event UnityAction AllEnemiesDied;

    private void Awake()
    {
        _textField = GetComponent<TMP_Text>();
        _currentCountValue = 0;
        _textField.text = _currentCountValue.ToString();
    }

    private void OnEnable()
    {
        _player.EnemyDied += AddUnit;
    }

    private void OnDisable()
    {
        _player.EnemyDied -= AddUnit;
    }

    private void AddUnit() 
    {
        _currentCountValue++;
        _textField.text = _currentCountValue.ToString();
        if (_currentCountValue == _spawner.Quantity)
            AllEnemiesDied?.Invoke();
    }
}