using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;

public class MovementState : IStateInput
{
    protected readonly IStateSwitcher StateSwitcher;
    protected readonly MachineMovementData Data;
    private readonly Character _character;

    protected InputSystem Input => _character.Input;
    protected Rigidbody2D Rigidbody => _character.Rigidbody;
    public MovementState(IStateSwitcher stateSwitcher, MachineMovementData data, Character character)
    {
        StateSwitcher = stateSwitcher;
        Data = data;
        _character = character;
    }

    public void Enter()
    {
        Debug.Log($"Enter {GetType()}");
    }

    public void Exit()
    {
        Debug.Log($"Exit {GetType()}");
    }

    public virtual void HandleInput()
    {
        Vector2 input = ReadInput();
        Data.XInput = input.x;
        Data.YInput = input.y;

        Data.XVelocity = Data.XInput * Data.Speed;
        Data.YVelocity = Data.YInput * Data.Speed; // Человек конечно не должен летать, но в ТЗ не было про прыжок.
    }

    public virtual void Update()
    {
        Rigidbody.MovePosition(Rigidbody.position + Data.Velocity * Time.deltaTime);
    }

    protected bool IsInputZero() => IsHorizontalInputZero() && IsVerticalInputZero();
    protected bool IsHorizontalInputZero() => Data.XInput == 0;
    protected bool IsVerticalInputZero() => Data.YInput == 0;
    /*
     * TODO
     * Далее нужно будет убрать обработку от тачскрина из этой функции
     * А то несколько схем управления в одну функцию сувать pukIng, так ещё и UI будет
     * Сейчас как костыль пойдёт, т.к. ТЗ не понятно, а через бота на тестовом спрашивать по ТЗ супер в падлу.
    */
    private const float JoystickRadius = 500f;

    private Vector2 _joystickPosition;
    private bool _isTouchActive;
    
    private Vector2 ReadInput()
    {
        var input = Input.Player.Move.ReadValue<Vector2>();
        if (Input.Player.TouchContact.IsPressed())
        {
            
            if (!_isTouchActive)
            {
                _joystickPosition = input;
                _isTouchActive = true;
            } else
            {
                _joystickPosition += input;
            }
            Debug.DrawRay(_character.transform.position, _joystickPosition.normalized * Data.Velocity.magnitude);
            float distance = _joystickPosition.magnitude;

            if (distance > JoystickRadius)
                _joystickPosition = _joystickPosition.normalized * JoystickRadius;

            return _joystickPosition / JoystickRadius;
        }

        _isTouchActive = false;
        return input.normalized;
    }
}