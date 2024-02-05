using Godot;
using System;

public partial class game_manger : Node3D
{
    [Export]
    public PackedScene playerScene { get; set; }

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
        if(GetTree().CurrentScene.Name == "main")
        {
            Spawn_Character();
        }
	}
	public void _on_pickup_area_entered(player body)
	{
        //gets player id so that we know who to give the pickups stats to
        body.GetId();
	}

	public void Spawn_Character()
	{
        if (Input.IsJoyButtonPressed(id1, JoyButton.A) && !player1Spawned)
        {
            player player1 = playerScene.Instantiate<player>();
            AddChild(player1);
            player1.SetId(id1);
            player1Spawned = true;
        }
        if (Input.IsJoyButtonPressed(id2, JoyButton.A) && !player2Spawned)
        {
            var player2 = playerScene.Instantiate<player>();
            AddChild(player2);
            player2.SetId(id2);
            player2.Position = new Vector3(2, 0, 0);
            player2Spawned = true;
        }
        if (Input.IsJoyButtonPressed(id3, JoyButton.A) && id3 != -1 && !player3Spawned)
        {
            var player3 = playerScene.Instantiate<player>();
            AddChild(player3);
            player3.SetId(id3);
            player3Spawned = true;
        }
        if (Input.IsJoyButtonPressed(id4, JoyButton.A) && id4 != -1 && !player4Spawned)
        {
            var player4 = playerScene.Instantiate<player>();
            AddChild(player4);
            player4.SetId(id4);
            player4Spawned = true;
        }
    }
}
