using Godot;
using System;

public partial class player : CharacterBody3D
{
    Node player1;
    Node player2;
    Node player3;
    Node player4;
    int id1;
    int id2;
    int id3 = -1;
    int id4 = -1;

    public override void _Ready()
    {
        var ids = Input.GetConnectedJoypads();
        id1 = ids[0];
        id2 = ids[1];
        if(ids.Count >= 3)
        {
            id3 = ids[2];
        }
        if(ids.Count == 4)
        {
            id4 = ids[3];
        }
        
        player1 = GetParent<Node3D>().GetChild(2); //id1
        player2 = GetParent<Node3D>().GetChild(1); //id2
        if(id3 != -1)
        {
            player3 = GetParent<Node3D>().GetChild(3); //id3
        }
        if(id4 != -1)
        {
            player4 = GetParent<Node3D>().GetChild(4); //id4
        }

    }

    public override void _PhysicsProcess(double delta)
    {
        
        //controlls for player 1
        if (Input.GetJoyAxis(id1, JoyAxis.LeftX) > 0.3f || Input.GetJoyAxis(id1, JoyAxis.LeftX) < -0.3f)
        {
            GD.Print("side1");
        }
        if (Input.GetJoyAxis(id1, JoyAxis.LeftY) > 0.3f || Input.GetJoyAxis(id1, JoyAxis.LeftY) < -0.3f)
        {
            GD.Print("front1");
        }
        //controlls for player 2
        if (Input.GetJoyAxis(id2, JoyAxis.LeftX) > 0.3f || Input.GetJoyAxis(id2, JoyAxis.LeftX) < -0.3f)
        {
            GD.Print("side2");
        }
        if (Input.GetJoyAxis(id2, JoyAxis.LeftY) > 0.3f || Input.GetJoyAxis(id2, JoyAxis.LeftY) < -0.3f)
        {
            GD.Print("front2");
        }
        if (player3 != null)
        {
            //controlls for player 3
            if (Input.GetJoyAxis(id3, JoyAxis.LeftX) > 0.3f || Input.GetJoyAxis(id3, JoyAxis.LeftX) < -0.3f)
            {
                GD.Print("side3");
            }
            if (Input.GetJoyAxis(id3, JoyAxis.LeftY) > 0.3f || Input.GetJoyAxis(id3, JoyAxis.LeftY) < -0.3f)
            {
                GD.Print("front3");
            }
        }
        if(player4 != null)
        {
            //controlls for player 4
            if (Input.GetJoyAxis(id4, JoyAxis.LeftX) > 0.3f || Input.GetJoyAxis(id4, JoyAxis.LeftX) < -0.3f)
            {
                GD.Print("side4");
            }
            if (Input.GetJoyAxis(id4, JoyAxis.LeftY) > 0.3f || Input.GetJoyAxis(id4, JoyAxis.LeftY) < -0.3f)
            {
                GD.Print("front4");
            }
        }
    }

}
