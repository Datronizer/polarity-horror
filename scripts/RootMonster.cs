using Godot;
using System;

public abstract partial class RootMonster : RigidBody2D
{
	public int Speed = 100; // How fast the monster will move (pixels/sec).


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) { }

	public abstract void Hunt(Vector2 playerPos);

	public abstract void Stalk(Vector2 playerPos);

	public abstract void Flee(Vector2 playerPos);
}
