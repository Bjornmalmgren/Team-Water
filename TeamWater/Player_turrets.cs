using Godot;
using System;

public partial class Player_turrets : CharacterBody3D
{
    [Export]
    public float gravity { get; set; } = 9.8f;

    [Export]
    public int Speed { get; set; } = 14;

    private Vector3 _targetVelocity = Vector3.Zero;

    Node3D Gun;
    RayCast3D gun;
    Node3D TargetPoint;

    [Export]
    public PackedScene bulletScene { get; set; }
    public override void _Ready()
    {
        Gun = GetNode<Node3D>("Pivot/Gun");
        gun = GetNode<RayCast3D>("Pivot/Gun/RayCast3D");
        TargetPoint = GetNode<Node3D>("Pivot/TargetPoint");
        
    }
    public override void _PhysicsProcess(double delta)
    {
        var direction = Vector3.Zero;

        if (Input.IsActionPressed("move_right"))
        {
            direction.X += 1.0f;
        }
        if (Input.IsActionPressed("move_left"))
        {
            direction.X -= 1.0f;
        }
        if (Input.IsActionPressed("move_down"))
        {
            direction.Z += 1.0f;
        }
        if (Input.IsActionPressed("move_up"))
        {
            direction.Z -= 1.0f;
        }

        if (Input.IsActionPressed("shoot")) 
        {
            var instance = bulletScene.Instantiate<bullet>();
            
            this.Owner.GetTree().Root.AddChild(instance);
            //instance.Transform.Scaled(new Vector3(0.1f, 0, 0.1f));
            //instance.Transform.ScaledLocal(new Vector3(0.1f, 0, 0.1f));
            instance.GlobalPosition = gun.GlobalPosition;
            instance.GlobalRotation = gun.GlobalRotation;
            //instance.GlobalTransform = gun.GlobalTransform;
            //gun.Rotation = this.Rotation;
            //gun.LookAt(TargetPoint.Position);

        }
        if (direction != Vector3.Zero)
        {
            direction = direction.Normalized();
            GetNode<Node3D>("Pivot").LookAt(Position - direction);
        }

        _targetVelocity.X = direction.X * Speed;
        _targetVelocity.Z = direction.Z * Speed;

        // Moving the character
        Velocity = _targetVelocity;
        MoveAndSlide();
    }
}
