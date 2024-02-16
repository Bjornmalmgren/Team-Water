using Godot;
using System;

public partial class Movement : RigidBody3D
{
    private const float MoveForce = 30f;

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
            if (inputMovement.LengthSquared() > 0)
                inputMovement = inputMovement.Normalized() * MoveForce/50;
                ApplyImpulse(inputMovement);
        }

        if (Input.IsActionPressed("MoveBack"))
        {
            inputMovement += GlobalTransform.Basis.Z;
            if (inputMovement.LengthSquared() > 0)
                inputMovement = inputMovement.Normalized() * MoveForce;
                ApplyForce(inputMovement);
        }

        if (Input.IsActionPressed("MoveLeft"))
        {
            inputMovement -= GlobalTransform.Basis.X;
            if (inputMovement.LengthSquared() > 0)
                ApplyTorqueImpulse(new Vector3(0,0.3f,0));
        }

        if (Input.IsActionPressed("MoveRight"))
        {
            inputMovement += GlobalTransform.Basis.X;
            if (inputMovement.LengthSquared() > 0)
                ApplyTorqueImpulse(new Vector3(0,-0.3f,0));
        }
    
    }
}