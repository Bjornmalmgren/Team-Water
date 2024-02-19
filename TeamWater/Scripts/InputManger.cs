using Godot;
using System;
using System.Linq;

public partial class InputManger : Node3D
{
    [Export]
    public PackedScene playerScene { get; set; }
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
        if (Input.IsKeyPressed(Key.F1))
        {
            GetTree().ReloadCurrentScene();
        }
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        Spawn_Character();	
    }
    public bool[] GetPlayerSpawned()
    {
        bool[] result = { player1Spawned, player2Spawned, player3Spawned, player4Spawned };
        return result;
    }

    public player[] GetPlayerList()
    {
        player[] list =  { player1};
        player[] list2 = { player1, player2 };
        player[] list3 = { player1, player2, player3 };
        player[] list4 = { player1, player2, player3, player4 };
        if (player2Spawned)
        {
            return list2;
        }
        if (player3Spawned)
        {
            return list3;
        }
        if (player4Spawned)
        {
            return list4;
        }

        return list;
    }
}
