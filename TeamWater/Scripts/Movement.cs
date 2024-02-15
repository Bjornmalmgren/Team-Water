using Godot;
using System;

public partial class Movement : RigidBody3D
{
    private const float MoveForce = 0.5f;

    public override void _PhysicsProcess(double delta)
    {
        ProcessInput(delta);
    }

    private void ProcessInput(double delta)
    {
        Vector3 inputMovement = Vector3.Zero;

        if (Input.IsActionPressed("MoveForward"))
        {
            inputMovement -= GlobalTransform.Basis.Z;
        }

        if (Input.IsActionPressed("MoveBack"))
        {
            inputMovement += GlobalTransform.Basis.Z;
        }

        if (Input.IsActionPressed("MoveLeft"))
        {
            inputMovement -= GlobalTransform.Basis.X;
        }

        if (Input.IsActionPressed("MoveRight"))
        {
            inputMovement += GlobalTransform.Basis.X;
        }

        if (inputMovement.LengthSquared() > 0)
        {
            inputMovement = inputMovement.Normalized() * MoveForce;
            ApplyImpulse(inputMovement);
        }
    }
}