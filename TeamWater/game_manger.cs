using Godot;
using System;

public partial class game_manger : Node3D
{
    [Export]
    public PackedScene playerScene { get; set; }
    [Export]
    public PackedScene weaponPickupScene { get; set; }
    [Export]
    public PackedScene enemyScene { get; set; }

    Enemy_Basic_1 enemy;
    weapon_pickup pickup;
    player player1;
    player player2;
    player player3;
    player player4;

    bool player1Spawned = false;
    bool player2Spawned = false;
    bool player3Spawned = false;
    bool player4Spawned = false;
    int id1;
    int id2;
    int id3 = -1;
    int id4 = -1;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        pickup = weaponPickupScene.Instantiate<weapon_pickup>();
        enemy = enemyScene.Instantiate<Enemy_Basic_1>();
        pickup.Position = new Vector3(0, 0.427f, 0);
        
        AddChild(pickup);
        var ids = Input.GetConnectedJoypads();
        id1 = ids[0];
        id2 = ids[1];
        if (ids.Count >= 3)
        {
            id3 = ids[2];
        }
        if (ids.Count >= 4)
        {
            id4 = ids[3];
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Spawn_Character();
        


    }
    public void _on_pickup_area_entered(player body)
	{
        GD.Print("player");
        //gets player id so that we know who to give the pickups stats to
        int id = body.GetId();
        if(id == 0)
        {
            body.spreadShoot = pickup.GetSpread();
            body.amountOfBullets = pickup.GetAmountOfBUllets();
            body.SetFrameGap(pickup.GetAmountOfFrames());
        }
        if (id == 1)
        {
            body.spreadShoot = pickup.GetSpread();
            body.amountOfBullets = pickup.GetAmountOfBUllets();
            body.SetFrameGap(pickup.GetAmountOfFrames());
        }

        if (id == 2)
        {

        }

        if (id == 3)
        {

        }
        GD.Print(body.amountOfBullets);
    }

	public void Spawn_Character()
	{
        
        if (Input.IsJoyButtonPressed(id1, JoyButton.A) && !player1Spawned)
        {
            player1 = playerScene.Instantiate<player>();
            player1.Scale = new Vector3(0.2f, 0.2f, 0.2f);
            player1.Position = new Vector3(0, 0, 0);
            AddChild(player1);
            player1.SetId(id1);
            player1Spawned = true;
        }
        if (Input.IsJoyButtonPressed(id2, JoyButton.A) && !player2Spawned)
        {
            player2 = playerScene.Instantiate<player>();
            player2.Scale = new Vector3(0.2f, 0.2f, 0.2f);
            player2.Position = new Vector3(2, 0, 0);
            AddChild(player2);
            player2.SetId(id2);

            player2Spawned = true;
        }
        if (Input.IsJoyButtonPressed(id3, JoyButton.A) && id3 != -1 && !player3Spawned)
        {
            player3 = playerScene.Instantiate<player>();
            player3.Scale = new Vector3(0.2f, 0.2f, 0.2f);
            AddChild(player3);
            player3.SetId(id3);
            player3Spawned = true;
        }
        if (Input.IsJoyButtonPressed(id4, JoyButton.A) && id4 != -1 && !player4Spawned)
        {
            player4 = playerScene.Instantiate<player>();
            player4.Scale = new Vector3(0.2f, 0.2f, 0.2f);
            AddChild(player4);
            player4.SetId(id4);
            player4Spawned = true;
        }
    }

    public void _on_area_3d_area_entered(Node collidingObject, int damage)
    {

        var type = collidingObject.GetType();
        //same code struture for everything that should be hit like enemies and other pickups
        if(type == player1.GetType())
        {
            player objectHit = (player)collidingObject;
            objectHit.TakeDamage(damage);
        }
        if (type == pickup.GetType())
        {
            weapon_pickup objectHit = (weapon_pickup)collidingObject;
            objectHit.TakeDamage(damage);
        }
        if (type == enemy.GetType())
        {
            Enemy_Basic_1 objectHit = (Enemy_Basic_1)collidingObject;
            objectHit.TakeDamage(damage);
        }
    }

    public Vector3 CheckClosestPlayer(Vector3 position)
    {
        var closestPosX = 0.0f;
        var closestPosZ = 0.0f;
        if (player1Spawned) 
        {
            closestPosX = Math. Abs(player1.Position.X - position.X);
            closestPosZ = Math.Abs(player1.Position.Z - position.Z);
        }
        if (player2Spawned)
        {
            if(Math.Abs(player2.Position.X - position.X) < closestPosX && 
                Math.Abs(player2.Position.Z - position.Z) < closestPosZ)
            {
                closestPosZ = Math.Abs(player2.Position.Z - position.Z);
                closestPosX = Math.Abs(player2.Position.X - position.X);
            }
        }
        if (player3Spawned)
        {
            if (Math.Abs(player3.Position.X - position.X) < closestPosX &&
                Math.Abs(player3.Position.Z - position.Z) < closestPosZ)
            {
                closestPosZ = Math.Abs(player3.Position.Z - position.Z);
                closestPosX = Math.Abs(player3.Position.X - position.X);
            }
        }
        if (player4Spawned)
        {
            if (Math.Abs(player4.Position.X - position.X) < closestPosX &&
                Math.Abs(player4.Position.Z - position.Z) < closestPosZ)
            {
                closestPosZ = Math.Abs(player4.Position.Z - position.Z);
                closestPosX = Math.Abs(player4.Position.X - position.X);
            }
        }
        return new Vector3(closestPosX, 0, closestPosZ);
    }

    public bool[] GetPlayerSpawned()
    {
        bool[] result = {player1Spawned,player2Spawned,player3Spawned,player4Spawned };
        return result;
    }
}
