using Godot;
using System;

public partial class Lantern : Node2D
{
	[Signal]
	public delegate void LightHitEventHandler(Node2D monster);
	[Signal]
	public delegate void LightAimEventHandler();

	public bool IsAiming = false;

	private AnimatedSprite2D FlashlightSprite;
	private AnimatedSprite2D LanternSprite;

	private CollisionPolygon2D LightCone;
	private PointLight2D SpotLight;
	private PointLight2D AreaLight;

	private Node2D bodyInside;

	private bool isBodyInside = false;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LightCone = GetNode<CollisionPolygon2D>("Area2D/LightConeCollision");

		FlashlightSprite = GetNode<AnimatedSprite2D>("FlashlightSprite");
		LanternSprite = GetNode<AnimatedSprite2D>("LanternSprite");

		SpotLight = GetNode<PointLight2D>("SpotLight");
		AreaLight = GetNode<PointLight2D>("AreaLight");

		LightCone.Disabled = true;

		SpotLight.Visible = false;
		AreaLight.Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (isBodyInside)
		{
			EmitSignal(SignalName.LightHit, bodyInside);
		}

		if (!IsAiming)
		{
			LightCone.Disabled = true;

			SpotLight.Visible = false;
			AreaLight.Visible = true;

			FlashlightSprite.Visible = false;
			LanternSprite.Visible = true;

			bodyInside = null;
			isBodyInside = false;
		}

		IsAiming = false;
	}

	public void OnPlayerAim(Vector2 mousePos)
	{
		LightCone.Disabled = false;

		SpotLight.Visible = true;
		AreaLight.Visible = false;

		FlashlightSprite.Visible = true;
		LanternSprite.Visible = false;

		IsAiming = true;

		// Vector2 direction = new Vector2(Mathf.Cos(Mathf.Pi / 2), Mathf.Sin(Mathf.Pi / 2)).Normalized();
		// SpotLight.LookAt(GetGlobalMousePosition() - direction);

		EmitSignal(SignalName.LightAim);
	}


	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Monster"))
		{
			bodyInside = body;
			isBodyInside = true;
			GD.Print("Light hit monster: ", body.Name);
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if (body.IsInGroup("Monster"))
		{
			bodyInside = null;
			isBodyInside = false;
			GD.Print("Monster left light: ", body.Name);
		}
	}
}
