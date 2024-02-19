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
        if (ids.Count >= 2)
        {
            id2 = ids[1];
        }
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
        
        


    }



    public void _on_area_3d_area_entered(Node collidingObject, int damage)
    {
        
        if(collidingObject as HitInterface != null)
        {
            (collidingObject as HitInterface).Hit(damage);
        }
    }



}
