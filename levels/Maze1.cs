using Godot;
using System;

public partial class Maze1 : Maze
{
	public override void _Ready()
	{
		base._Ready();
		HideGroupNodes("blue_mask_active");
		HideGroupNodes("green_mask_active");
	}
	
	private void BlueMaskActivated()
	{
		GD.Print("Blue Mask Activated");
		ShowGroupNodes("blue_mask_active");
	}
	
	private void GreenMaskActivated()
	{
		GD.Print("Green Mask Activated");
		ShowGroupNodes("green_mask_active");
	}

	private void MaskReceived(int maskId)
	{
		if (maskId == 4)
		{
			GreenMaskActivated();
		}
	}

	public override void HideSpecial()
	{
		base.HideSpecial();
		HideGroupNodes("blue_mask_active");
		HideGroupNodes("green_mask_active");
	}
}
