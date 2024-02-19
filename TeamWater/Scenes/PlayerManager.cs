using Godot;
using System;
using System.Linq;

public partial class PlayerManager : Node3D
{
    [Export]
    public PackedScene playerScene { get; set; }
    player player1;
    player player2;
    player player3;
    player player4;
    InputManger inputManger;
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
        inputManger = GetParent<Node3D>().GetNode<InputManger>("InputManager");
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

    public void _on_pickup_area_entered(player body, weapon_pickup pickup)
    {
        GD.Print("player");
        //gets player id so that we know who to give the pickups stats to
        int id = body.GetId();
        if (id == 0)
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
            body.spreadShoot = pickup.GetSpread();
            body.amountOfBullets = pickup.GetAmountOfBUllets();
            body.SetFrameGap(pickup.GetAmountOfFrames());
        }

        if (id == 3)
        {
            body.spreadShoot = pickup.GetSpread();
            body.amountOfBullets = pickup.GetAmountOfBUllets();
            body.SetFrameGap(pickup.GetAmountOfFrames());
        }
        GD.Print(body.amountOfBullets);
    }

    public Vector3 CheckClosestPlayer(Vector3 position)
    {

        player[] playerList = inputManger.GetPlayerList();
        
        Vector3 closestPlayer = playerList[0].GlobalPosition;
        float closestDist = closestPlayer.Length();

        for (int i = 0; i < playerList.Length; i++)
        {
            Vector3 dist = position - playerList[i].GlobalPosition;
            float magnitude = dist.Length();

            if (closestDist > magnitude)
            {
                closestPlayer = playerList[i].GlobalPosition;
            }


        }




        //if (player3Spawned)
        //{
        //    if (Math.Abs(player3.GlobalPosition.X - position.X) < closestPosX &&
        //        Math.Abs(player3.Position.Z - position.Z) < closestPosZ)
        //    {
        //        closestPosZ = Math.Abs(player3.Position.Z - position.Z);
        //        closestPosX = Math.Abs(player3.Position.X - position.X);

        //    }
        //}
        //if (player4Spawned)
        //{
        //    if (Math.Abs(player4.Position.X - position.X) < closestPosX &&
        //        Math.Abs(player4.Position.Z - position.Z) < closestPosZ)
        //    {
        //        closestPosZ = Math.Abs(player4.Position.Z - position.Z);
        //        closestPosX = Math.Abs(player4.Position.X - position.X);

        //    }
        return closestPlayer;
        //}
    }
}
