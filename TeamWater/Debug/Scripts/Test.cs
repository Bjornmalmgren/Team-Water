using Godot;
using System;

public partial class Test : Node3D
{

	[Export]
	Node3D target;



	public override void _Process(double delta)
	{

        Vector3 direction = target.GlobalPosition - this.GlobalPosition;
        direction = direction.Normalized();
        //float angle = direction.AngleTo(this.Basis.Z);
        Vector3 perp = direction.Cross(Vector3.Up);
        Basis b = new Basis(perp, Vector3.Up, direction);
        this.Basis = b;

    }
}
