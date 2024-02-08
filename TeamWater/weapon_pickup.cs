using Godot;
using System;

public partial class weapon_pickup : Area3D
{
	int firerate;
	int size_of_bullets;
	CollisionObject2D collisionObject;

	int health = 15;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(health <= 0)
		{
			Die();
		}
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
	}

	public void Die()
	{
		QueueFree();
	}
}
