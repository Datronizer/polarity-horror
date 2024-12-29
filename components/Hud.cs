using Godot;
using System;

public partial class Hud : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();
	
		
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// GetNode<Label>("DebugMobCount").Text = $"Mobs: {GetNode<Node2D>("Mobs").GetChildCount()}";
		
	}

	// public void UpdateScore(int score)
	// {
	// 	GetNode<Label>("ScoreLabel").Text = $"Score: {score}";
	// }

	public void UpdatePlayerPos(Vector2 pos)
	{
		GetNode<Label>("PlayerPos").Text = $"Player: {pos}";
	}

	public void UpdateCameraPos(Vector2 pos)
	{
		GetNode<Label>("CameraPos").Text = $"Camera: {pos}";
	}
	
	public void UpdateMouseViewPos(Vector2 pos)
	{
		GetNode<Label>("MouseViewPos").Text = $"Mouse: {pos}";
	}
}
