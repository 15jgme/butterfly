using Godot;
using System;

public partial class Root : Node3D
{
	Vector3 lorenz_config = Vector3.Zero; // Order is Sigma, Beta, Rho
	public Root() {
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
