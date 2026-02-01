using Godot;
using System;

public partial class Exit : Area3D
{
	[Signal] public delegate void ExitEnteredEventHandler();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnAreaEntered(Area3D area)
	{
		EmitSignal(SignalName.ExitEntered);
		Game.Instance.Maze.UI.ShowVictory();
		Game.Instance.Maze.WarpToEnd();
	}
}
