using Godot;
using System;
using System.Threading.Tasks;

public partial class health_bar : ProgressBar
{
    public health_bar(int health) => Health = health;

	[Export]
	int Health {
		get {return Health; }
		set { Health = _SetHealth(); }
	}

    private int _SetHealth()
    {

		SetHealth(Health);
		return 0;
    }

    public void init_health(int _health) 
	{
		Health = _health;
		MaxValue = Health;
		Value = Health;
	}
	public override void _Ready()
	{ 
	}
	
	public void SetHealth(int health) 
	{
        Health = health;
		Value = health;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
