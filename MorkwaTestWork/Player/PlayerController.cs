using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] private PlayerMover _mover;

    private InputControl _inputControl;

    public float HorizontalDirection { get; private set; }
    public float VerticalDirection { get; private set; }

    private void Awake()
    {
        _inputControl = new InputControl();        
    }

    private void OnEnable()
    {
        _inputControl.Enable();
        _inputControl.Player.HorizontalMove.performed += ctx => TryToMove();
        _inputControl.Player.VerticalMove.performed += ctx => TryToMove();
        _inputControl.Player.HorizontalMove.canceled += ctx => Stop();
        _inputControl.Player.VerticalMove.canceled += ctx => Stop();
    }

    private void OnDisable()
    {      
         _inputControl.Player.HorizontalMove.performed -= ctx => TryToMove();
         _inputControl.Player.VerticalMove.performed -= ctx => TryToMove();
        _inputControl.Disable();
    }

    private void TryToMove()
    {
        HorizontalDirection = _inputControl.Player.HorizontalMove.ReadValue<float>();
        VerticalDirection = _inputControl.Player.VerticalMove.ReadValue<float>();
        _mover.enabled = true;
    }

    private void Stop() 
    {
        HorizontalDirection = _inputControl.Player.HorizontalMove.ReadValue<float>();
        VerticalDirection = _inputControl.Player.VerticalMove.ReadValue<float>();
        if (HorizontalDirection == 0 && VerticalDirection == 0)
            _mover.enabled = false;
    }
}
