using Godot;
using System;
using System.Numerics;

public partial class bullet : Node3D
{
	public int SPEED = 140;

	MeshInstance3D mesh;
	RayCast3D ray;

	public override void _Ready()
	{
		mesh = GetNode<MeshInstance3D>("MeshInstance3D");
		ray = GetNode<RayCast3D>("RayCast3D");
	}
	public override void _Process(double delta)
	{
        Scale = new Godot.Vector3(0.2f, 0.2f, 0.2f);
        Godot.Vector3 speed = new Godot.Vector3(0, 0, -SPEED);
        speed.Z *= (float)delta;
        GlobalPosition += Transform.Basis * speed;
	}
	
	public void _on_timer_timeout() 
	{
		QueueFree();
	}
}
