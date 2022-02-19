using UnityEngine;

public class PlayerMover : PlayerEntity
{
    [SerializeField] private float _speed;

    private void OnEnable()
    {
        PlayerInput.Enable();     
    }

    private void OnDisable()
    {
        PlayerInput.Disable();      
    }

    private void Update()
    {
        Vector2 targetPosition = MainCamera.ScreenToWorldPoint(PlayerInput.Player.Position.ReadValue<Vector2>());
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}