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
		hud.UpdatePlayerPos(GetNode<Player>("Player").GlobalPosition);
		hud.UpdateCameraPos(GetNode<Camera2D>("Player/Camera2D").GlobalPosition);
		// hud.UpdatePlayerPos(GetNode<Camera2D>("Player/Camera2D").GetTargetPosition());

		InformPlayerLocation(GetNode<Player>("Player").Position);
	}

	public void GameOver()
	{
		GD.Print("You died");
	}

	public void NewGame()
	{
		_score = 0;

		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);

		GetNode<Timer>("Wendigo/HuntTimer").Start();
		GetNode<Wendigo>("Wendigo").Search(player.GlobalPosition);

		hud = GetNode<Hud>("HUD");
	}

	public void InformPlayerLocation(Vector2 playerPos)
	{
		GetNode<Wendigo>("Wendigo").Search(playerPos);
	}

	// We also specified this function name in PascalCase in the editor's connection window.
	// private void OnMobTimerTimeout()
	// {
	// 	// Create a new instance of the Mob scene.
	// 	RootMonster monster = MobScene.Instantiate<RootMonster>();
	// 	GD.Print(monster);

	// 	// Choose a random location on Path2D.
	// 	var monsterSpawnLocation = GetNode<PathFollow2D>("MonsterPath/MonsterSpawnLocation");
	// 	monsterSpawnLocation.ProgressRatio = GD.Randf();

	// 	// Set the mob's direction perpendicular to the path direction.
	// 	float direction = monsterSpawnLocation.Rotation + Mathf.Pi / 2;

	// 	// Set the mob's position to a random location.
	// 	monster.Position = monsterSpawnLocation.Position;

	// 	// Add some randomness to the direction.
	// 	direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
	// 	monster.Rotation = direction;

	// 	// Choose the velocity.
	// 	var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
	// 	monster.LinearVelocity = velocity.Rotated(direction);

	// 	// Spawn the mob by adding it to the Main scene.
	// 	AddChild(monster);

	// 	GD.Print("Mob spawned");
	// 	GD.Print(monster.Position);
	// }
}
