using System.Collections.Generic;
using System.Linq;

public class HumanMovementStateMachine : IStateSwitcher, IListenInput
{
    private List<IStateInput> _states;
    private IStateInput _currentState;
    public HumanMovementStateMachine(Character character, float speed)
    {
        MachineMovementData data = new(){ Speed = speed};

        _states = new List<IStateInput>()
        {
            new IdlingState(this, data, character),
            new RunningState(this, data, character)
        };

        _currentState = _states[0];
        _currentState.Enter();
    }

    public void Switch<T>() where T : IState
    {
        IStateInput state = _states.FirstOrDefault(state => state is T) ?? throw new("State not found!");

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void HandleInput() => _currentState.HandleInput();
    public void Update() => _currentState.Update();
}
