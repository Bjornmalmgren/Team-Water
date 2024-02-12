using Godot;
using System;

public partial class Enemy_Basic_1 : RigidBody3D
{
    public int amountOfBullets = 1;
    int frames;
    int amountOfFramesBetweenShoots = 50;
    Node3D Gun;
    RayCast3D gun;
    public bool spreadShoot = false;
    ProgressBar Healthbar;
    HealthBar heal;

    [Export]
    public PackedScene enemy_bulletScene { get; set; }
    private int health = 15;
    public override void _Ready()
	{
		
        Gun = GetNode<Node3D>("Pivot/Gun");
        gun = GetNode<RayCast3D>("Pivot/Gun/RayCast3D");
        Healthbar = GetNode<ProgressBar>("SubViewport/HealthBar");
        heal = (HealthBar)Healthbar;
        heal.init_health(health);
        gun.Position = Position;
    }

	public override void _Process(double delta)
	{
        if (health <= 0)
        {
            Die();
        }
      
    }

	public void _on_timer_timeout() 
	{
		_random_shoot();
	}
	public void _random_shoot() 
	{
        for (int i = 0; i < amountOfBullets; i++)
        {

            var instance = enemy_bulletScene.Instantiate<enemy_bullet>();
            instance.CheckSpredShoot(spreadShoot);
            GetTree().Root.AddChild(instance);
            instance.Position = gun.Position;
            instance.GlobalRotation = gun.GlobalRotation;
           
        }
    }
    public void MakeSpreadShot()
    {
        spreadShoot = true;
        amountOfBullets = 4;
    }

    public void Die()
    {
        QueueFree();
    }
    public void TakeDamage(int damage)
    {
        health = health - damage;
        heal.SetHealth(health);
    }
}
