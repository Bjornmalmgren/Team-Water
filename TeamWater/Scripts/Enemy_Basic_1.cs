using Godot;
using System;

public partial class Enemy_Basic_1 : RigidBody3D, HitInterface
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
    PlayerManager playerManager;
    InputManger inputManger;
    [Export]
    public PackedScene enemy_bulletScene { get; set; }

    public override void _Ready()
	{
        gameManger = GetTree().Root.GetNode<game_manger>("Game Manger");
        playerManager = GetParent<Node3D>().GetNode<PlayerManager>("PlayerManager");
        inputManger = GetParent<Node3D>().GetNode<InputManger>("InputManager");
        Gun = GetNode<Node3D>("Pivot/Gun");
        gun = GetNode<RayCast3D>("Pivot/Gun/RayCast3D");
        Healthbar = GetNode<ProgressBar>("SubViewport/HealthBar");
        heal = (HealthBar)Healthbar;
        heal.init_health(health);
        gun.Position = Position;
        
    }

	public override void _Process(double delta)
	{
        bool[] isSpawned = inputManger.GetPlayerSpawned();

        if (isSpawned[0] && isSpawned[1])
        {
            
            target = playerManager.CheckClosestPlayer(GlobalPosition);
            //GD.Print(target);
            //target = gameManger.playerPos();
            //LookAt(target,Vector3.Up);

            Vector3 direction = target - this.GlobalPosition;
            direction = direction.Normalized();
            //float angle = direction.AngleTo(this.Basis.Z);
            Vector3 perp = direction.Cross(Vector3.Up);
            Basis b = new Basis(perp, Vector3.Up, direction);
            this.Basis = b;


            if (!hasTarget)
            {
                hasTarget = true;
            }
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
            instance.Position = new Vector3(instance.Position.X, 0, instance.Position.Z);
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

    public void Hit(int damage)
    {
        TakeDamage(damage);
    }
}
