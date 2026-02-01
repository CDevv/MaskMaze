using Godot;
using System;

public partial class MaskPlatform : StaticBody3D
{
	[Signal]
	public delegate void ActivatedEventHandler(int maskId);

	[Export] public int MaskId { get; set; } = 1;

	private Color _color = Colors.White;
	[Export]
	public Color Color
	{
		get { return _color; }
		set
		{
			_color = value;
		}
	}

	public CollisionShape3D Collision { get; private set; }
	public MeshInstance3D Mesh { get; private set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Collision = GetNode<CollisionShape3D>("Collision");
		Mesh = GetNode<MeshInstance3D>("%Mesh");
		AddToGroup("platforms");
		
		Mesh.Mesh.Get(BoxMesh.PropertyName.Material).As<StandardMaterial3D>().Set(StandardMaterial3D.PropertyName.AlbedoColor, _color);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Activate(int maskId)
	{
		if (maskId == this.MaskId)
		{
			Hide();
			GD.Print("Activated");
		}
	}

	private new void Hide()
	{
		Collision.Disabled = true;
		Mesh.Visible = false;
	}

	private new void Show()
	{
		Collision.Disabled = false;
		Mesh.Visible = true;
	}
}
