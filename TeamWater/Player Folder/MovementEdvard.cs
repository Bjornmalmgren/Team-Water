using Godot;
using System;

public partial class MovementEdvard : RigidBody3D
{
    private const float MoveForce = 30f;

    public override void _PhysicsProcess(double delta)
    {
        ProcessInput(delta);
    }

    private void ProcessInput(double delta)
    {
        Vector3 inputMovement = Vector3.Zero;
        
        if (Input.GetJoyAxis(1, JoyAxis.TriggerRight) > 0.3f)
        {
            inputMovement -= GlobalTransform.Basis.Z;
            if (inputMovement.LengthSquared() > 0)
                inputMovement = inputMovement.Normalized() * MoveForce/75;
                ApplyImpulse(inputMovement);
        }

        if (Input.GetJoyAxis(1, JoyAxis.TriggerLeft) > 0.3f)
        {
            inputMovement += GlobalTransform.Basis.Z;
            if (inputMovement.LengthSquared() > 0)
                inputMovement = inputMovement.Normalized() * MoveForce;
                ApplyForce(inputMovement);
        }

        if (Input.GetJoyAxis(1, JoyAxis.LeftX) < -0.3f)
        {
            inputMovement -= GlobalTransform.Basis.X;
            if (inputMovement.LengthSquared() > 0)
                ApplyTorqueImpulse(new Vector3(0,0.3f,0));
        }

        if (Input.GetJoyAxis(1, JoyAxis.LeftX) > 0.3)
        {
            inputMovement += GlobalTransform.Basis.X;
            if (inputMovement.LengthSquared() > 0)
                ApplyTorqueImpulse(new Vector3(0,-0.3f,0));
        }
    
    }
}