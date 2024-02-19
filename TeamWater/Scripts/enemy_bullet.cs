using Godot;
using System;

public partial class enemy_bullet : Node3D
{

        public int SPEED = 15;

    public bool spreadShoot = false;

    public int damage = 2;
    Godot.Vector3 speed;
    MeshInstance3D mesh;
    RayCast3D ray;
    game_manger gameManger;
    Area3D area;
    float range;
    public override void _Ready()
    {

        mesh = GetNode<MeshInstance3D>("MeshInstance3D");
        ray = GetNode<RayCast3D>("RayCast3D");
        gameManger = GetTree().Root.GetNode<game_manger>("Game Manger");
        area = GetNode<Area3D>("Area3D");
        var rng = new RandomNumberGenerator();
        range = rng.RandfRange(-1.0f, 1.0f);
    }
    public override void _Process(double delta)
    {
        //Scale = new Godot.Vector3(0.7f, 0.7f, 0.7f);

        if (spreadShoot == false)
        {
            speed = new Godot.Vector3(0, 0, -SPEED);
            speed.Z *= (float)delta;
            GlobalPosition += Transform.Basis * speed;
        }
        else
        {

            speed = new Godot.Vector3(range, 0, -SPEED);
            speed.Z *= (float)delta;
            GlobalPosition += Transform.Basis * speed;


        }


    }

    public void _on_timer_timeout()
    {
        QueueFree();
    }

    public void Collision(Node objects)
    {
        
        gameManger._on_area_3d_area_entered(objects, damage);
    }

    public void CollisionDetectedEnemyBullet(Node collisionObject)
    {

    }
    public void CheckSpredShoot(bool spred)
    {
        spreadShoot = spred;
    }
}

