using Godot;
using System;

public partial class StaticWall : StaticBody3D
{
	public CollisionShape3D CollisionShape { get; private set; }
	public MeshInstance3D Mesh { get; private set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var mesh = GetNodeOrNull<MeshInstance3D>("MeshInstance3D");
		if (mesh != null)
		{
			Mesh = mesh;
		}
		
		CollisionShape = GetNode<CollisionShape3D>("Collision");
		if (Mesh == null) return;
		Mesh = GetNode<MeshInstance3D>("Mesh");
		CollisionShape.SetScale(Mesh.Scale);
		CollisionShape.Position = Mesh.Position;
		CollisionShape.Rotation = Mesh.Rotation;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
