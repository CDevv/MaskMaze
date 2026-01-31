using Godot;
using System;

public partial class Mask : Area3D
{
	[Export] public int Id { get; set; } = 1;
	
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
		if (area.Name != "PlayerMaskArea") return;
		Game.Instance.Player.AddMask(Id);
		GD.Print("Mask ", Id, " given");
		Game.Instance.Maze.ActivatePlatforms(Id);
		QueueFree();
	}
}
