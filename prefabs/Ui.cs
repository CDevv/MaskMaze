using Godot;
using System;

public partial class Ui : CanvasLayer
{
	public Panel MaskPartPanel { get; private set; }

	public Panel ResetPanel { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MaskPartPanel = GetNode<Panel>("MaskPartPanel");
		ResetPanel = GetNode<Panel>("ResetPanel");
		
		HideAllItems();
		MaskPartPanel.Hide();
		foreach (Node n in GetTree().GetNodesInGroup("mask_parts"))
		{
			n.Call(Sprite2D.MethodName.Hide);
		}
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

	public void ShowMaskPartsPanel()
	{
		
		MaskPartPanel.Show();
	}

	public void HideMaskPartsPanel()
	{
		MaskPartPanel.Hide();
	}

	public void ShowMaskPart(int id)
	{
		GetNode<Sprite2D>("MaskPartPanel/" + id.ToString()).Visible = true;
	}

	public void ShowVictory()
	{
		ResetPanel.Show();
	}

	public void HideVictory()
	{
		ResetPanel.Hide();
	}
	
	public void OnReset()
	{
		HideAllItems();
		Game.Instance.Maze.Player.Reset();
		HideVictory();
	}
}
