public class IdlingState : MovementState
{
    public IdlingState(IStateSwitcher stateSwitcher, MachineMovementData data, Character character) : base(stateSwitcher, data, character)
    {}

    public override void Update()
    {
        base.Update();

        if (IsInputZero()) return;

        StateSwitcher.Switch<RunningState>();
    }
}