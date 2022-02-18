using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class PlayerMaxHealthField : MonoBehaviour
{
    [SerializeField] private Player _player;
    private TMP_Text _health;

    private void Start()
    {
        _health = GetComponent<TMP_Text>();
        _health.text = _player.HP.ToString();
    }
}