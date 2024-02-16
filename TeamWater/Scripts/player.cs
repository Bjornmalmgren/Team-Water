using Godot;
using System;
using System.Security.Cryptography;

public partial class player : RigidBody3D, HitInterface
{
    [Export]
    int id { get; set; }
    public bool spreadShoot = false;
    public int amountOfBullets = 1;
    int frames;
    int amountOfFramesBetweenShoots = 10;
    [Export]
    public float gravity { get; set; } = 9.8f;
    public bool isSpawned = false;
    [Export]
    public int Speed { get; set; } = 14;

    private Vector3 _targetVelocity = Vector3.Zero;

    private int health = 15;
    private float MoveForce = 0;
    float rotationSpeed = 2f;
    Node3D Gun;
    RayCast3D gun;
    Node3D TargetPoint;
    ProgressBar Healthbar;
    HealthBar heal;
    [Export]
    public PackedScene bulletScene { get; set; }

    public override void _Ready()
    {
        isSpawned = true;
        Gun = GetNode<Node3D>("Pivot/Gun");
        gun = GetNode<RayCast3D>("Pivot/Gun/RayCast3D");
        TargetPoint = GetNode<Node3D>("Pivot/TargetPoint");
        Healthbar = GetNode<ProgressBar>("SubViewport/HealthBar");
        heal = (HealthBar)Healthbar;
        heal.init_health(health);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (health <= 0)
        {
            Die();
        }

        //GD.Print(Position + "p: " + id);
        var direction = Vector3.Zero;
        var inputMovemnt = Vector3.Zero;
        //controlls for player 1
        if (Input.GetJoyAxis(id, JoyAxis.LeftX) > 0.3f)
        {
            direction += Transform.Basis.X;
        }
        if (Input.GetJoyAxis(id, JoyAxis.LeftX) < -0.3f)
        {
            direction -= Transform.Basis.X;
        }
        if (Input.GetJoyAxis(id, JoyAxis.TriggerLeft) > 0.3f)
        {
            inputMovemnt += Transform.Basis.Z;
            MoveForce = 0.2f;
            if (inputMovemnt.LengthSquared() > 0)
            {
                inputMovemnt = inputMovemnt.Normalized() * MoveForce;
                ApplyForce(inputMovemnt);
            }
        }
        if (Input.GetJoyAxis(id, JoyAxis.LeftY) < -0.3f)
        {
            direction -= Transform.Basis.Z;
        }
        if (Input.GetJoyAxis(id, JoyAxis.TriggerRight) > 0.3f)
        {
            inputMovemnt -= Transform.Basis.Z;
            MoveForce = 0.2f;
            if (inputMovemnt.LengthSquared() > 0)
            {
                inputMovemnt = inputMovemnt.Normalized() * MoveForce;
                ApplyForce(inputMovemnt);
            }
        }
        else
        {
            MoveForce = 0;
        }
        if (Input.IsJoyButtonPressed(id, JoyButton.X) && frames == amountOfFramesBetweenShoots)
        {

            for (int i = 0; i < amountOfBullets; i++)
            {
                var instance = bulletScene.Instantiate<bullet>();
                instance.CheckSpredShoot(spreadShoot, (i-1));
                GetTree().Root.AddChild(instance);
                instance.GlobalPosition = gun.GlobalPosition;
                instance.GlobalRotation = gun.GlobalRotation;
            }

        }
        if (direction != Vector3.Zero)
        {
            double rotation = Mathf.LerpAngle(Rotation.Y, Mathf.Atan2(-direction.X, -direction.Z), delta * rotationSpeed);
            Rotation = new Vector3(0, (float)rotation, 0);


        }

        // Moving the character
        if (frames >= amountOfFramesBetweenShoots)
        {
            frames = 0;
        }
        frames++;
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    public void SetAmountOfShoots(int shoots)
    {
        amountOfBullets = shoots;
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        heal.SetHealth(health);

    }
    public void SetFrameGap(int frameGap)
    {
        amountOfFramesBetweenShoots = frameGap;
    }

    public bool IsPlayerSpawned()
    {
        return isSpawned;
    }

    public void Die()
    {
        isSpawned = false;
        QueueFree();
    }

    public int GetId()
    {
        return id;
    }

    public void MakeSpreadShot()
    {
        spreadShoot = true;
        amountOfBullets = 3;

    }

    public void Hit(int damage)
    {
        TakeDamage(damage);
    }
}
