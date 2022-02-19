using UnityEngine;

public class PlayerController : PlayerEntity
{
    [SerializeField] private LayerMask _playerLayers;
    [SerializeField] private float _distance;
    [SerializeField] private PlayerMover _mover;

    private void OnEnable()
    {       
        PlayerInput.Enable();
        PlayerInput.Player.Movement.performed += ctx => TryToEnableMover();
        PlayerInput.Player.Movement.canceled += ctx => _mover.enabled = false;
    }

    private void OnDisable()
    {
        PlayerInput.Disable();
        PlayerInput.Player.Movement.performed -= ctx => TryToEnableMover();
        PlayerInput.Player.Movement.canceled -= ctx => _mover.enabled = false;
    }

    private void TryToEnableMover() 
    {      
        Vector3 targetPosition = MainCamera.ScreenToWorldPoint(PlayerInput.Player.Position.ReadValue<Vector2>());
        RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.zero, _distance, _playerLayers);
        if (hit.collider != null)
            _mover.enabled = true;
    }
}