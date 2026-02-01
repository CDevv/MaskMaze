using Godot;
using System;

public partial class Ui : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		HideAllItems();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ShowItem(int id)
	{
		GetNode<MaskItem>(id.ToString()).Visible = true;

		ToggleSelectBorder(false);
		if (Game.Instance.Player.GetWornMask() == id)
		{
			GetNode<MaskItem>(id.ToString()).ToggleSelectBorder(true);
		}
	}

	public void HideItem(int id)
	{
		GetNode<MaskItem>(id.ToString()).Visible = false;
	}

	public void HideAllItems()
	{
		foreach (Node n in GetTree().GetNodesInGroup("item_panels"))
		{
			n.Set(Panel.PropertyName.Visible, false);
		}
	}

	public void ToggleSelectBorder(bool value)
	{
		foreach (Node n in GetTree().GetNodesInGroup("item_panels"))
		{
			MaskItem item = (MaskItem)n;
			item.ToggleSelectBorder(value);
		}
	}

	public void ToggleSelectBorder(bool value, int id)
	{
		foreach (Node n in GetTree().GetNodesInGroup("item_panels"))
		{
			MaskItem item = (MaskItem)n;
			if (item.Id == id)
			{
				item.ToggleSelectBorder(value);
			}
			else
			{
				item.ToggleSelectBorder(!value);
			}
		}
	}
}
