using Godot;
using System;

public partial class HealthBar : ProgressBar
{
    public HealthBar(int health) => Health = health;
    public HealthBar() { }
    // Called when the node enters the scene tree for the first time.
    [Export]
    int Health
    {
        get;
        set;
    } = 0;

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
