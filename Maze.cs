using Godot;
using System;

public partial class Maze : Node3D
{
	public Player Player { get; private set; }
	public Camera3D Camera { get; private set; }
	public DirectionalLight3D Light { get; private set; }
	public AudioStreamPlayer Music { get; set; }
	public Godot.Collections.Array<MaskPlatform> Platforms { get; private set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Light = GetNode<DirectionalLight3D>("%MainLight");
		Camera = GetNode<Camera3D>("Camera");
		Player = GetNode<Player>("%Player");
		Music = GetNode<AudioStreamPlayer>("Music");
		Player.Scene = this;
		
		Game.Instance.Player = Player;
		Game.Instance.Maze = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var lightPos = Light.Position;
		lightPos.X = Camera.Position.X;
		lightPos.Z = Camera.Position.Z;
		Light.Position = lightPos;
	}

	public void ActivatePlatforms(int maskId)
	{
		foreach (var platform in GetTree().GetNodesInGroup("plat"))
		{
			platform.EmitSignal("Activated", maskId);
		}
	}
}
