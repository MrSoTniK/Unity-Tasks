using UnityEngine;

public class MoveState : State
{
    [SerializeField] private float _speed;

    private Entity _target;

    private void OnEnable()
    {
        _target = GetComponent<Character>().Target;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
    }
}