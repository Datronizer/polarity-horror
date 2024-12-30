using Godot;
using System;

public partial class Lantern : Node2D
{
	public bool IsAiming = false;

	private PointLight2D SpotLight;
	private PointLight2D AreaLight;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SpotLight = GetNode<PointLight2D>("SpotLight");
		AreaLight = GetNode<PointLight2D>("AreaLight");

		SpotLight.Visible = false;
		AreaLight.Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!IsAiming)
		{
			SpotLight.Visible = false;
			AreaLight.Visible = true;
		}

		IsAiming = false;
	}

	public void OnPlayerAim(Vector2 mousePos)
	{
		SpotLight.Visible = true;
		AreaLight.Visible = false;

		IsAiming = true;

		var direction = (mousePos - GlobalPosition).Normalized();
		SpotLight.Rotation = direction.Angle() - Mathf.Pi / 2;

		GD.Print("Player is aiming");
	}
}
