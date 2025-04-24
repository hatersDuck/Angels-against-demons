using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    private InputSystem _input;
    private Rigidbody2D _rigidbody;
    private HumanMovementStateMachine _movementMachine;
    [SerializeField, Range(0.01f, 10f)] private float _speed = 2;

    private IListenInput _currentMachine;
    public InputSystem Input => _input;
    public Rigidbody2D Rigidbody => _rigidbody;

    private void Awake()
    {
        // TODO добавить инициализацию
        _input = new();
        _rigidbody = GetComponent<Rigidbody2D>();

        _movementMachine = new(this, _speed);
        _currentMachine = _movementMachine;
    }

    private void Update()
    {
        _currentMachine.HandleInput();
        _currentMachine.Update();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}