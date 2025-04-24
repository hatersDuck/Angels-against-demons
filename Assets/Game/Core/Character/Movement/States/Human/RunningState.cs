public class RunningState : MovementState
{
    public RunningState(IStateSwitcher stateSwitcher, MachineMovementData data, Character character) : base(stateSwitcher, data, character)
    {
    }

    public override void Update()
    {
        base.Update();

        if (IsInputZero()) StateSwitcher.Switch<IdlingState>();
    }
}