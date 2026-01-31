using Godot;
using System;

public partial class Game : Node
{
	public static Game Instance { get; set; }
	public Maze Maze { get; set; }
	public Player Player { get; set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
