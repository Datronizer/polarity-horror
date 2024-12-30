using Godot;
using System;

public partial class Inventory : Node2D
{
	[Signal]
	public delegate void LightStrengthEventHandler(float strength);

	[Export]
	public float LightStrengthDiminish = 0.03f;

	private Area2D CrankArea;
	private Sprite2D Crank;
	private PointLight2D Light;

	private bool hasMouse = false;
	private bool isClicking = false;

	private float rotations = 0;
	private float maxRotations = 15;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CrankArea = GetNode<Area2D>("Area2D");
		Crank = CrankArea.GetNode<Sprite2D>("Crank");
		Light = GetNode<PointLight2D>("Light");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (rotations > 10)
		{
			Light.Energy = 5;
		}
		else
		{
			Light.Energy = rotations / 2;
		}

		if (hasMouse && isClicking)
		{
			var initialRotation = Crank.Rotation;
			Crank.LookAt(GetGlobalMousePosition());
			var newRotation = Crank.Rotation;

			rotations += Mathf.Abs(newRotation - initialRotation);
			EmitSignal(SignalName.LightStrength, rotations);
		}

		if (rotations > maxRotations)
		{
			rotations = maxRotations;
		}
		else if (rotations < 0)
		{
			rotations = 0;
		}

		rotations -= LightStrengthDiminish;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed == true)
			{
				isClicking = true;
			}
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed == false)
			{
				isClicking = false;
			}
		}
	}

	private void OnArea2DMouseEntered()
	{
		hasMouse = true;
	}

	private void OnArea2DMouseExited()
	{
		hasMouse = false;
	}
}
