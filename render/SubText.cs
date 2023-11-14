using Godot;
using System;

public partial class SubText : Label3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector3 lorenz_config = GetNode<Point>("../../RigidBody3D").get_lorenz_system();
		Text = String.Format("σ: {0:f}, β: {1:f}, ρ: {2:f}", lorenz_config.X, lorenz_config.Y, lorenz_config.Z);
	}
}
