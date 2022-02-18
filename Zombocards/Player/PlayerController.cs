using UnityEngine;
using IJunior.TypedScenes;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layers;
    [SerializeField] private float _distance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private Player _player;

    private PlayerInput _inputManager;
    private int _maxPlayerHP;

    private void Awake()
    {
        _inputManager = new PlayerInput();
        _maxPlayerHP = _player.HP;
    }

    private void OnEnable()
    {
        _inputManager.Enable();
        _inputManager.Player.TouchPress.performed += ctx => TryToInteract();
    }

    private void OnDisable()
    {
        _inputManager.Player.TouchPress.performed -= ctx => TryToInteract();
        _inputManager.Disable();
    }

    private void TryToInteract()
    {
        Vector3 targetPosition = _camera.ScreenToWorldPoint(_inputManager.Player.TouchPosition.ReadValue<Vector2>());
        RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.zero, _distance, _layers);
        if (hit.collider != null && hit.collider.TryGetComponent<Card>(out Card card)) 
        {
            Interact(card);
        }
    }

    private void Interact(Card card) 
    {
        Vector3 targetPosition = card.transform.position;
        Vector3 previousPosition = transform.position;

        if (Vector2.Distance(previousPosition, targetPosition) <= _maxDistance)
        {
            switch (card.Id)
            {
                case ConstantValuesContainer.IdForCards.IdForEnemies:
                    TryToAttack(card, targetPosition);
                    break;
                case ConstantValuesContainer.IdForCards.IdForItems:
                    CollectItem(card, targetPosition);
                    break;
                case ConstantValuesContainer.IdForCards.IdForMeleeWeapons:
                    break;
                case ConstantValuesContainer.IdForCards.IdForShootingWeapons:
                    break;
                case ConstantValuesContainer.IdForCards.IdForArmor:
                    break;
            }
        }                        
    }

    private void TryToAttack(Card card, Vector3 targetPosition) 
    {
        int enemyHP = card.HP;
        card.TakeDamage(_player.HP);
        _player.TakeDamage(enemyHP);

        if (_player.HP > 0)
        {
            card.Die();
            _player.Move(targetPosition);
        }
        else
            GameOverMenu.Load();
    }

    private void CollectItem(Card card, Vector3 targetPosition) 
    {
        _player.TakeHeal(card.HP, _maxPlayerHP);
        card.Die();
        _player.Move(targetPosition);
    }
}