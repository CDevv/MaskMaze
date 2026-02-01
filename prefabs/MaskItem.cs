using Godot;
using System;

public partial class MaskItem : Panel
{
	[Export] public Texture2D Sprite { get; set; }
	[Export] public int Id { get; set; }
	public Panel SelectBorder { get; private set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Sprite2D>("Sprite2D").Texture = Sprite;
		GetNode<Label>("%Label").Text = Id.ToString();
		
		SelectBorder = GetNode<Panel>("SelectBorder");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ToggleSelectBorder(bool value)
	{
		SelectBorder.Visible = value;
	}
}
