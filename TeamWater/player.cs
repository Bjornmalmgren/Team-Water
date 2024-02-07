using Godot;
using System;

public partial class player : CharacterBody3D
{
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
            GD.Print("side" + id);
        }
        if (Input.GetJoyAxis(id, JoyAxis.LeftY) > 0.3f || Input.GetJoyAxis(id, JoyAxis.LeftY) < -0.3f)
        {
            GD.Print("front" + id);
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
