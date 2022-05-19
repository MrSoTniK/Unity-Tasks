using UnityEngine;

public class NavMeshMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        transform.position += Vector3.forward * _speed * Time.fixedDeltaTime;
    }
}