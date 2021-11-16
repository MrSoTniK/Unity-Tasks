using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class HealthChangerButton : MonoBehaviour
{
    [SerializeField] protected float SpeedOfChange;
    [SerializeField] protected Player Player;
    [SerializeField] private Button _button;

    public UnityAction<bool> CoroutineIsWorking;
    public UnityAction<float> HealthChanged;
    protected abstract int ValueOfChange { get; }

    protected delegate bool CheckMethod();
    protected abstract CheckMethod VerificationHP { get; }

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _button.onClick.AddListener(delegate { OnButtonClick(ValueOfChange); });
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(delegate { OnButtonClick(ValueOfChange); });
    }

    private void OnButtonClick(int valueOfChange)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if(VerificationHP())
             _coroutine = StartCoroutine(ChangeHP(valueOfChange));
    }

    protected abstract IEnumerator ChangeHP(int valueOfChange);     
}
