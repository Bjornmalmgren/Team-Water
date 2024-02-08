using Godot;
using System;
using System.Security.Cryptography;

public partial class player : CharacterBody3D
{
    [Export]
    int id { get; set; }

    int frames;

    [Export]
    public float gravity { get; set; } = 9.8f;

    [Export]
    public int Speed { get; set; } = 14;

    private Vector3 _targetVelocity = Vector3.Zero;

    private int health = 15;

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
        if (health <= 0)
        {
            Die();
        }

        //GD.Print(Position + "p: " + id);
        var direction = Vector3.Zero;
        //controlls for player 1
        if (Input.GetJoyAxis(id, JoyAxis.LeftX) > 0.3f)
        {
            direction.X += 1.0f;
        }
        if(Input.GetJoyAxis(id, JoyAxis.LeftX) < -0.3f)
        {
            direction.X -= 1.0f;
        }
        if (Input.GetJoyAxis(id, JoyAxis.LeftY) > 0.3f)
        {
            direction.Z += 1.0f;
        }
        if(Input.GetJoyAxis(id, JoyAxis.LeftY) < -0.3f)
        {
            direction.Z -= 1.0f;
        }
        if (Input.IsJoyButtonPressed(id, JoyButton.X) && frames % 10 == 0)
        {
            var instance = bulletScene.Instantiate<bullet>();

            GetTree().Root.AddChild(instance);
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
            GetNode<Node3D>("Pivot").LookAt(Position - 60*direction);
        }

        _targetVelocity.X = direction.X * Speed;
        _targetVelocity.Z = direction.Z * Speed;

        // Moving the character
        Velocity = _targetVelocity;
        MoveAndSlide();
        frames++;
        if (frames == 60) {
            frames = 0;
        }
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
    }

    public void Hp()
    {
        GD.Print(health);
    }

    public void Die()
    {
        QueueFree();
    }

    public int GetId()
    {
        return id;
    }

}
