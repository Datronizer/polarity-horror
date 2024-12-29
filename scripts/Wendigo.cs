using Godot;
using System;

public partial class Wendigo : RootMonster
{
	public new int Speed = 50;
	public int FleeSpeed = 250;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) { }

	public void OnBodyEntered(Node body)
	{
		if (body is Player)
		{
			GD.Print("Wendigo collided with Player");
		}
	}

	public override void Hunt(Vector2 playerPos)
	{
		throw new NotImplementedException();
	}

	public override void Stalk(Vector2 playerPos)
	{
		throw new NotImplementedException();
	}

	public override void Flee(Vector2 playerPos)
	{
		throw new NotImplementedException();
	}
}
