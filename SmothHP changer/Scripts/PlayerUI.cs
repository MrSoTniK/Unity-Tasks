using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Player))]
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider _sliderHP;
    [SerializeField] private TMP_Text _fieldHP;

    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
        GetPlayerHealth();
    }

    private void FixedUpdate()
    {
        GetPlayerHealth();
    }

    private void GetPlayerHealth() 
    {
        _player.CheckHealth();
        _fieldHP.text = ((int)_player.Health).ToString();
        _sliderHP.value = (float)_player.Health;
    }
}