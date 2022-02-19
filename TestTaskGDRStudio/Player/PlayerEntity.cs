using UnityEngine;

[RequireComponent(typeof(Player))]
public abstract class PlayerEntity : MonoBehaviour
{
    [SerializeField] protected Camera MainCamera;

    protected PlayerInput PlayerInput;

    protected virtual void Awake()
    {
        PlayerInput = new PlayerInput();
    }
}