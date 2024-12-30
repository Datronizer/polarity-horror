using Godot;
using System;

public partial class Wendigo : RootMonster
{
	public new int Speed = 50;
	public new int DetectionRadius = 500;
	public new int KillRadius = 100;

	public int FleeSpeed = 250;


	public Vector2 _playerGlobalPos;

	private float _delta;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_delta = (float)delta;
		Hunt(_playerGlobalPos);
	}

	public void OnBodyEntered(Node body)
	{
		if (body is Player)
		{
			GD.Print("Wendigo collided with Player");
		}
	}

	public void OnHuntTimerTimeout()
	{
		// GD.Print("Wendigo hunting Player at last known location: ", _playerGlobalPos);
		Hunt(_playerGlobalPos);
	}

	public override void Hunt(Vector2 playerPos)
	{
		if (GlobalPosition.DistanceTo(playerPos) < DetectionRadius)
		{
			Stalk(playerPos);
		}
		else
		{
			ApproachPlayer(playerPos, _delta, 50);
		}
	}

	public override void Stalk(Vector2 playerPos)
	{
		if (GlobalPosition.DistanceTo(playerPos) < KillRadius)
		{
			ApproachPlayer(playerPos, _delta, 999);
		}
		if (GlobalPosition.DistanceTo(playerPos) < 250)
		{
			Position += Vector2.Zero;
		}
	}

	public override void Flee(Vector2 playerPos)
	{
		if (GlobalPosition.DistanceTo(playerPos) < 200)
		{
			ApproachPlayer(playerPos, _delta, -FleeSpeed);
		}
	}

	/// <summary>
	/// The wendigo always knows where the player is. 
	/// </summary>
	/// <param name="playerPos"></param>
	public override void Search(Vector2? playerPos = null)
	{
		_playerGlobalPos = playerPos ?? Vector2.Zero;
	}

	public override void OnHitByLight()
	{
		GD.Print("Wendigo hit by light");
		Flee(_playerGlobalPos);
	}
}
