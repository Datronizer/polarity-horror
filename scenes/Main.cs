using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }

	private int _score;


	// private Label debugMobCount;
	private Label debugPlayerPos;
	private Label debugMouseViewPos;


	private Hud hud;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		hud.UpdateMouseViewPos(GetViewport().GetMousePosition());
		hud.UpdatePlayerPos(GetNode<Player>("Player").Position);
		hud.UpdatePlayerPos(GetNode<Camera2D>("Player/Camera2D").GetTargetPosition());
	}

	public void GameOver()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
	}

	public void NewGame()
	{
		_score = 0;

		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);

		GD.Print("Starting Timer");
		GetNode<Timer>("StartTimer").Start();

		
		hud = GetNode<Hud>("HUD");
	}
	//
	// We also specified this function name in PascalCase in the editor's connection window.
	private void OnScoreTimerTimeout()
	{
		_score++;
	}

	// We also specified this function name in PascalCase in the editor's connection window.
	private void OnStartTimerTimeout()
	{
		GD.Print("StartTimerTimeout");
		GetNode<Timer>("MonsterTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}

	// We also specified this function name in PascalCase in the editor's connection window.
	private void OnMobTimerTimeout()
	{
		// Create a new instance of the Mob scene.
		Monster monster = MobScene.Instantiate<Monster>();
		GD.Print(monster);

		// Choose a random location on Path2D.
		var monsterSpawnLocation = GetNode<PathFollow2D>("MonsterPath/MonsterSpawnLocation");
		monsterSpawnLocation.ProgressRatio = GD.Randf();

		// Set the mob's direction perpendicular to the path direction.
		float direction = monsterSpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the mob's position to a random location.
		monster.Position = monsterSpawnLocation.Position;

		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		monster.Rotation = direction;

		// Choose the velocity.
		var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
		monster.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(monster);

		GD.Print("Mob spawned");
		GD.Print(monster.Position);
	}
}
