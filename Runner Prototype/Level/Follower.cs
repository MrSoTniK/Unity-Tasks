using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _offsetZ;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _target.transform.position.z + _offsetZ);
    }
}