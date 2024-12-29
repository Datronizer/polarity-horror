using Godot;
using System;

public partial class Monster : RigidBody2D
{
	public int Speed = 200; // How fast the player will move (pixels/sec).


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		// string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
		// animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		GD.Print(animatedSprite2D.SpriteFrames.GetAnimationNames());

		string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
		animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) { }

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}
