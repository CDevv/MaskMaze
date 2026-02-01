using Godot;
using System;

public partial class MaskPart : Area3D
{
	[Export]
	public int Id { get; set; }
	
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
		Game.Instance.Player.AddMaskPart(Id);
		Game.Instance.Maze.UI.ShowMaskPartsPanel();
		Game.Instance.Maze.UI.ShowMaskPart(Id);
		if (Game.Instance.Player.HasAllParts())
		{
			Game.Instance.Player.MaskParts.Clear();
			Game.Instance.Maze.UI.HideMaskPartsPanel();
			Game.Instance.Player.AddMask(4);
			Game.Instance.Maze.UI.ShowItem(4);
			Game.Instance.Maze.EmitSignal(Maze.SignalName.MaskReceived, 4);
		}
		Hide();
	}
}
