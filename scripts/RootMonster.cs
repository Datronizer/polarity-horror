using Godot;
using System;

public abstract partial class RootMonster : RigidBody2D
{
	public int Speed = 100; // How fast the monster will move (pixels/sec).
	public int DetectionRadius = 100; // Units pixels.
	public int KillRadius = 25; // Units pixels.


	// Called when the node enters the scene tree for the first time.
	public override void _Ready() { }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) { }

	public void ApproachPlayer(Vector2 playerPos, double delta, int? approachSpeed = null)
	{
		var screenSize = GetViewportRect().Size;
		var velocity = playerPos - GlobalPosition;
		velocity = velocity.Normalized() * (approachSpeed ?? Speed);
		
		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, screenSize.X),
			y: Mathf.Clamp(Position.Y, 0, screenSize.Y)
		);

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
			animatedSprite2D.Animation = "walk";
			animatedSprite2D.FlipH = velocity.X < 0;
		}
		else
		{
			animatedSprite2D.Animation = "idle";
			animatedSprite2D.Play();
		}
	}


	public abstract void OnHitByLight();

	public abstract void Hunt(Vector2 playerPos);

	public abstract void Stalk(Vector2 playerPos);

	public abstract void Flee(Vector2 playerPos);

	public abstract void Search(Vector2? playerPos = null);
}
