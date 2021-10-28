using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Screen : MonoBehaviour
{
    [SerializeField] protected CanvasGroup CanvasGroup;
    [SerializeField] protected Button Button;

    private void OnEnable()
    {
        Button.onClick.AddListener(OnButtonClik);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnButtonClik);
    }

    protected abstract void OnButtonClik();

    public abstract void Open();

    public abstract void Close();
}
