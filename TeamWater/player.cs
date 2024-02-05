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

    [Export]
    int id { get; set; }

    public override void _Ready()
    {


    }

    public override void _PhysicsProcess(double delta)
    {
        
        //controlls for player 1
        if (Input.GetJoyAxis(id, JoyAxis.LeftX) > 0.3f || Input.GetJoyAxis(id, JoyAxis.LeftX) < -0.3f)
        {
            GD.Print("side1");
        }
        if (Input.GetJoyAxis(id, JoyAxis.LeftY) > 0.3f || Input.GetJoyAxis(id, JoyAxis.LeftY) < -0.3f)
        {
            GD.Print("front1");
        }
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    public int GetId()
    {
        return id;
    }

}
