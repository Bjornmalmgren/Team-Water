using Godot;
using System;

public partial class Enemy_Basic_1 : RigidBody3D
{   
    private int health = 15;
    public int amountOfBullets = 1;
    int frames;
    int amountOfFramesBetweenShoots = 50;
    public bool spreadShoot = false;
    bool hasTarget = false;
    Vector3 target;

    Node3D Gun;
    RayCast3D gun;
    ProgressBar Healthbar;
    HealthBar heal;
    player player1;
    game_manger gameManger;
    [Export]
    public PackedScene enemy_bulletScene { get; set; }

    public override void _Ready()
	{
        gameManger = GetTree().Root.GetNode<game_manger>("Game Manger");
        Gun = GetNode<Node3D>("Pivot/Gun");
        gun = GetNode<RayCast3D>("Pivot/Gun/RayCast3D");
        Healthbar = GetNode<ProgressBar>("SubViewport/HealthBar");
        heal = (HealthBar)Healthbar;
        heal.init_health(health);
        gun.Position = Position;
        
    }

	public override void _Process(double delta)
	{
        bool[] isSpawned = gameManger.GetPlayerSpawned();
        
        if (isSpawned[0] && isSpawned[1])
        {
            if (!hasTarget)
            {
                target = gameManger.CheckClosestPlayer(Position);
                hasTarget = true;
            }
            GD.Print(gameManger.CheckClosestPlayer(Position));
            LookAt(target, null);
            
            Rotation = new Vector3(0.1f, Rotation.Y, 0.1f);
        }
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
