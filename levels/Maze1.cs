using Godot;
using System;

public partial class Maze1 : Maze
{
	public override void _Ready()
	{
		base._Ready();
		HideGroupNodes("blue_mask_active");
	}
	
	private void BlueMaskActivated()
	{
		GD.Print("Blue Mask Activated");
		ShowGroupNodes("blue_mask_active");
	}
}
