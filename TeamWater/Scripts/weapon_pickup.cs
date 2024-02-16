using Godot;
using System;

public partial class weapon_pickup : Area3D, HitInterface
{
	int firerate;
	int size_of_bullets;
	CollisionObject2D collisionObject;
	bool SpreadShoot = true;
	int amountOfShoots = 3;
	int health = 15;
	int amountOfFramesBetweenShots = 40;
	game_manger gameManger;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        gameManger = GetTree().Root.GetNode<game_manger>("Game Manger");
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

	public bool GetSpread()
	{
		return SpreadShoot;
	}

	public void CallGameManger(player body)
	{
		gameManger._on_pickup_area_entered(body);
		Die();
	}

    public int GetAmountOfBUllets()
    {
        return amountOfShoots;
    }

    public int GetAmountOfFrames()
    {
        return amountOfFramesBetweenShots;
    }

    public void Die()
	{
		QueueFree();
	}

    public void Hit(int damage)
    {
        TakeDamage(damage);
    }
}
