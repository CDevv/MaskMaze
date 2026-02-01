using Godot;
using System;

public partial class TrapMask : Area3D
{
	[Export] public int Id { get; set; } = 1;
	[Export] public Color Color { get; set; } = Colors.Wheat;
	public MeshInstance3D Mesh { get; private set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Mesh = GetNode<MeshInstance3D>("%Mesh");
		Mesh.GetSurfaceOverrideMaterial(0).Set(StandardMaterial3D.PropertyName.AlbedoColor, Color);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnAreaEntered(Area3D area)
	{
		if (area.Name != "PlayerMaskArea") return;
		Game.Instance.Player.Reset();
		Game.Instance.Maze.UI.HideAllItems();
	}
}
