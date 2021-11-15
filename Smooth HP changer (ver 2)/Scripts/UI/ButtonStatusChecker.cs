using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStatusChecker : MonoBehaviour
{
    [SerializeField] private HealButton _healButton;
    [SerializeField] private DamageButton _damageButton;

    private Button _healer;
    private Button _damager;

    private void Awake()
    {
        _healer = _healButton.GetComponent<Button>();
        _damager = _damageButton.GetComponent<Button>();
    }

    private void OnEnable()
    {
        _damageButton.CoroutineIsWorking += SetInteraction;
        _healButton.CoroutineIsWorking += SetInteraction;
    }

    private void OnDisable()
    {
        _damageButton.CoroutineIsWorking -= SetInteraction;
        _healButton.CoroutineIsWorking -= SetInteraction;
    }

    private void SetInteraction(bool isCoroutineWorking) 
    {
        _healer.interactable = !isCoroutineWorking;
        _damager.interactable = !isCoroutineWorking;
    }
}