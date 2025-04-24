using System;
using UnityEngine;


public class MachineMovementData
{
    public float XVelocity;
    public float YVelocity;

    private float _speed;
    private float _xInput;
    private float _yInput;

    public float XInput
    {
        get => _xInput;
        set => _xInput = CheckRange(value);
    }
    public float YInput
    {
        get => _yInput;
        set => _yInput = CheckRange(value);
    }
    public float Speed
    {
        get => _speed;
        set {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(_speed));
            
            _speed = value;
        }
    }
    public Vector2 Velocity => new(XVelocity, YVelocity);
    private float CheckRange(float value, float min = -1, float max = 1)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(nameof(value));
        return value;
    }
}