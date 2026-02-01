using Godot;
using System;

public partial class MaskToggleBlock : StaticBody3D
{
	[Export] public int MaskId { get; set; } = 1;
	[Export] public Color Color { get; set; } = Colors.White;
	public CollisionShape3D Collision { get; private set; }
	public MeshInstance3D Mesh { get; private set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Collision = GetNode<CollisionShape3D>("Collision");
		Mesh = GetNode<MeshInstance3D>("%Mesh");
		
		Mesh.Mesh.Get(BoxMesh.PropertyName.Material).As<StandardMaterial3D>().Set(StandardMaterial3D.PropertyName.AlbedoColor, Color);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Toggle(int mask)
	{
		GD.Print("Toggle");
		if (mask == MaskId)
		{
			Enable();
		}
		else
		{
			Disable();
		}
	}

	private void Enable()
	{
		Collision.Disabled = false;
		Mesh.Visible = true;
	}

	private void Disable()
	{
		Collision.Disabled = true;
		Mesh.Visible = false;
	}
}
