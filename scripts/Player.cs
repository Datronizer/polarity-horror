using Godot;
using System;

public partial class Player : Area2D
{
	[Signal]
	public delegate void HitEventHandler();
	[Signal]
	public delegate void AimEventHandler(Vector2 mousePos);


	[Export]
	public int Speed = 100; // How fast the player will move (pixels/sec).


	public Vector2 ScreenSize; // Size of the game window.
	private Camera2D Camera;
	private bool isAiming = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Camera = GetNode<Camera2D>("Camera2D");

		Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero; // The player's movement vector.
		if (isAiming)
		{
			ComputeCameraPosition();
			Speed = 30;
		}
		else
		{
			Camera.GlobalPosition = GlobalPosition;
			Speed = 100;
		}

		#region Player Inputs
		if (Input.IsActionPressed("move_right"))
		{
			velocity.X += 1;
		}

		if (Input.IsActionPressed("move_left"))
		{
			velocity.X -= 1;
		}

		if (Input.IsActionPressed("move_down"))
		{
			velocity.Y += 1;
		}

		if (Input.IsActionPressed("move_up"))
		{
			velocity.Y -= 1;
		}

		if (Input.IsActionPressed("aim"))
		{
			isAiming = true;
			EmitSignal(SignalName.Aim, GetViewport().GetMousePosition());
		}
		else
		{
			isAiming = false;
		}
		#endregion

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();

			animatedSprite2D.Animation = "walk_3";
			animatedSprite2D.FlipH = GetPlayerTurnDirection(velocity);
		}
		else
		{
			animatedSprite2D.Animation = "idle";
			animatedSprite2D.Play();
			animatedSprite2D.FlipH = GetPlayerTurnDirection(velocity);
		}


		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
	}

	public void Start(Vector2 position)
	{
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}

	// We also specified this function name in PascalCase in the editor's connection window.
	private void OnBodyEntered(Node2D body)
	{
		Hide(); // Player disappears after being hit.
		EmitSignal(SignalName.Hit);
		// Must be deferred as we can't change physics properties on a physics callback.
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}


	/// <summary>
	/// Returns false if the mouse if right of the player, true if left. This is because 
	/// the player sprite is facing right by default. So by returning false, it flips them the correct way.
	/// </summary>
	/// <returns>False if mouse is to the right of the player, True otherwise</returns>
	private bool GetPlayerTurnDirection(Vector2 velocity)
	{
		var mousePos = GetViewport().GetMousePosition();
		var playerGlobalPos = GlobalPosition;
		var middlePoint = (mousePos + playerGlobalPos) / 2;
		var direction = middlePoint.X - playerGlobalPos.X;

		if (isAiming)
		{
			return direction < 0;
		}
		else
		{
			return velocity.X < 0;
		}
	}
	private void ComputeCameraPosition()
	{
		var mousePos = GetViewport().GetMousePosition();
		if (mousePos.X < 0)
		{
			mousePos.X = 0;
		}
		else if (mousePos.X > ScreenSize.X)
		{
			mousePos.X = ScreenSize.X;
		}

		if (mousePos.Y < 0)
		{
			mousePos.Y = 0;
		}
		else if (mousePos.Y > ScreenSize.Y)
		{
			mousePos.Y = ScreenSize.Y;
		}

		var playerGlobalPos = GlobalPosition;
		var middlePoint = (mousePos + playerGlobalPos) / 2;

		Camera.GlobalPosition = middlePoint;
	}
}
