using Godot;
using System;

public partial class Maze : Node3D
{
	[Signal] public delegate void MaskReceivedEventHandler(int maskId);
	public Player Player { get; private set; }
	public Camera3D Camera { get; private set; }
	public DirectionalLight3D Light { get; private set; }
	public AudioStreamPlayer Music { get; set; }
	public Marker3D StartPos { get; private set; }
	public Ui UI { get; private set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UI = GetNode<Ui>("UI");
		StartPos = GetNode<Marker3D>("StartPos");
		Light = GetNode<DirectionalLight3D>("%MainLight");
		Camera = GetNode<Camera3D>("Camera");
		Player = GetNode<Player>("%Player");
		Music = GetNode<AudioStreamPlayer>("Music");
		Player.Scene = this;

		foreach (var node in GetTree().GetNodesInGroup("toggles"))
		{
			MaskToggleBlock block = (MaskToggleBlock)node;
			Player.WornMaskChanged += block.Toggle;
		}
		
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

	private void OnExitEntered()
	{
		GD.Print("Exited");
	}

	public void ActivatePlatforms(int maskId)
	{
		foreach (var platform in GetTree().GetNodesInGroup("platforms"))
		{
			platform.EmitSignal("Activated", maskId);
		}
	}

	public void DeactivatePlatforms()
	{
		foreach (var platform in GetTree().GetNodesInGroup("platforms"))
		{
			platform.Call(MaskPlatform.MethodName.Show);
		}
	}

	public void ShowMasks()
	{
		foreach (var node in GetTree().GetNodesInGroup("masks"))
		{
			node.Call(Node3D.MethodName.Show);
		}
	}

	public void HideGroupNodes(string group)
	{
		foreach (var node1 in GetTree().GetNodesInGroup(group))
		{
			var node = (Node3D)node1;
			node.Set("visible", false);
		}
	}

	public void ShowGroupNodes(string group)
	{
		foreach (var node in GetTree().GetNodesInGroup(group))
		{
			node.Call(Node3D.MethodName.Show);
		}
	}

	public void WarpToEnd()
	{
		var endPos = GetNode<Marker3D>("EndPos").Position;
		Player.Position = endPos;
	}
	
	public virtual void HideSpecial() {}
}
