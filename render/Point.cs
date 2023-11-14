using Godot;
using System;
using System.Diagnostics;

public partial class Point : RigidBody3D
{
	Vector3 lorenz_config = Vector3.Zero; // Order is Sigma, Beta, Rho
	public Point() {
		Random rnd = new Random();
		// TODO -> Refactor this to remove duplication
		// Modifiers which change the base value randomly between +- 10%
		float s_mod = (float)((rnd.NextDouble() - 0.5)/10.0) + 1.0f;
		float b_mod = (float)((rnd.NextDouble() - 0.5)/10.0) + 1.0f;
		float r_mod = (float)((rnd.NextDouble() - 0.5)/10.0) + 1.0f;

		// Reassign config based on base
		lorenz_config = new Vector3(s_mod * 10f, b_mod * 8/3f, r_mod * 40f);
	}
	
	public Vector3 get_lorenz_system()
	{
		return lorenz_config;
	}

	Vector3 get_dstate(Vector3 pos) {
		float sigma = lorenz_config.X;
		float beta = lorenz_config.Y;
		float rho = lorenz_config.Z;

		float dx = sigma*(pos.Y - pos.X);
		float dy = pos.X*(rho - pos.Z) - pos.Y;
		float dz = pos.X*pos.Y - beta*pos.Z;

		return new Vector3(dx, dy, dz);
	}
	public override void _IntegrateForces(PhysicsDirectBodyState3D state)
	{
		base._IntegrateForces(state);

		Vector3 pos = Position;
		Vector3 vel = get_dstate(pos);

		state.LinearVelocity = vel*0.1f;
	}
}
