using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class HPChangerButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] [Range(-10, 10)] private int _damage;
    [SerializeField] private int _speedOfChange;

    private UnityAction<int,int> _onChangingHealth;

    private void Start()
    {
        _onChangingHealth += _player.StartChangingHealth;
    }

    public void OnChangingHealthButtonsClick() 
    {
        _onChangingHealth.Invoke(_damage, _speedOfChange);
    }
}
