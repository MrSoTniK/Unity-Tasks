using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class PowerText : MonoBehaviour
{
    [SerializeField] private Squad _squad;

    private TMP_Text _poitnsText;

    private void Awake()
    {
        _poitnsText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {        
        _poitnsText.text = "1";
        _squad.PointsChanged += OnPoitnsChanged;
    }

    private void OnDisable()
    {
        _squad.PointsChanged -= OnPoitnsChanged;
    }

    private void OnPoitnsChanged(int receivedPoitns) 
    {
        _poitnsText.text = receivedPoitns.ToString();
    }
}