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
	private InventoryHud inventoryHud;

	private Lantern lantern;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("inventory"))
		{
			inventoryHud.Visible = !inventoryHud.Visible;
		}

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
		var lantern = GetNode<Lantern>("Player/Lantern");
		var startPosition = GetNode<Marker2D>("StartPosition");

		player.Start(startPosition.Position);

		GetNode<Timer>("Wendigo/HuntTimer").Start();
		GetNode<Wendigo>("Wendigo").Search(player.GlobalPosition);

		hud = GetNode<Hud>("HUD");
		inventoryHud = GetNode<InventoryHud>("InventoryHud");
	}

	public void OnLightHit(Node2D monster)
	{
		monster.Call("OnHitByLight");
	}

	public void InformPlayerLocation(Vector2 playerPos)
	{
		GetNode<Wendigo>("Wendigo").Search(playerPos);
	}
}
