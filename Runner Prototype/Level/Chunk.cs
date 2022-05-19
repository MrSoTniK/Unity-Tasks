using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Chunk : MonoBehaviour
{
    public UnityAction ColliderTouched;

    private void OnTriggerEnter(Collider body)
    {
        if (body.TryGetComponent<Squad>(out Squad squad))
            ColliderTouched?.Invoke();
    }
}