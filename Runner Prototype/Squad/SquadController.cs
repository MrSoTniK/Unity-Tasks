using UnityEngine;

public class SquadController : MonoBehaviour
{
    [SerializeField] private SquadTurner _turner;

    private InputControl _inputControl;

    private void Awake()
    {
        _inputControl = new InputControl();
    }

    private void OnEnable()
    {
        _inputControl.Enable();
        _inputControl.Squad.MouseClick.performed += ctx => _turner.enabled = true;
    }

    private void OnDisable()
    {
        _inputControl.Disable();
        _inputControl.Squad.MouseClick.performed -= ctx => _turner.enabled = true;
    }

    private void Update()
    {
        if (_inputControl.Squad.MouseClick.ReadValue<float>() == 1)
            _turner.enabled = true;
        else
            _turner.enabled = false;
    }
}