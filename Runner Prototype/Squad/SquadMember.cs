using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SquadMember : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private Transform _squad;

    private Rigidbody _rigidbody;
    private Vector3 _followPosition;

    public int Health { get; private set; }

    private void Awake()
    {
        _followPosition = new Vector3(0, 0, -1.5f);
        _rigidbody = GetComponent<Rigidbody>();
        Health = _maxHealth;
    }

    private void Update()
    {
        _rigidbody.velocity = new Vector3(_squad.position.normalized.x * 100f, 0, 10f);
        /*_rigidbody.AddForce(_squad.position.normalized.x, 0, 1f);*/
        /*transform.LookAt(_squad);*/
    }
}