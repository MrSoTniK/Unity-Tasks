using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SquadTurner : MonoBehaviour
{
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _minMouseMovement;

    private InputControl _inputControl;
    private NavMeshAgent _agent;
    private float _previousMousePositionX;
    private float _currentMousePositionX;
    private float _direction;

    private void Awake()
    {
        _inputControl = new InputControl();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _inputControl.Enable();
        _currentMousePositionX = 0;
    }

    private void OnDisable()
    {
        _inputControl.Disable();       
    }

    private void FixedUpdate()
    {
        _previousMousePositionX = _currentMousePositionX;
        _direction = SetDirection(out float difference);

        if(Mathf.Abs(difference) >= _minMouseMovement && _direction != 0) 
        {           
            Vector3 currentMousePosition = new Vector3(_direction, 0, 0);
            _agent.Move(currentMousePosition * Time.fixedDeltaTime);           
        }
    }

    private float SetDirection(out float difference) 
    {
        _currentMousePositionX = _inputControl.Squad.MousePosition.ReadValue<Vector2>().x;
        difference = _previousMousePositionX - _currentMousePositionX;

        if (_inputControl.Squad.MousePosition.ReadValue<Vector2>().x > _previousMousePositionX)       
            return _turnSpeed;
                
        if(_inputControl.Squad.MousePosition.ReadValue<Vector2>().x < _previousMousePositionX)         
            return -_turnSpeed;
                
         return 0;                  
    }
}