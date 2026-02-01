using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody3D
{
	public const int MaskPartCount = 3;
	public const float Speed = 5.0f;

	[Signal] public delegate void WornMaskChangedEventHandler(int mask);
	public Maze Scene { get; set; }
	public HashSet<int> MaskInventory { get; private set; } = new();
	public Godot.Collections.Array<int> WornMasks { get; private set; } = new();
	public HashSet<int> MaskParts { get; private set; } = new();

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		
		//Update camera position
		var camPos = Scene.Camera.Position;
		camPos.X = Position.X;
		camPos.Z = Position.Z;
		Scene.Camera.Position = camPos;

		if (Position.Y <= -5)
		{
			Reset();
			Game.Instance.Maze.UI.HideAllItems();
		}
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventKey eventKey && eventKey.Pressed)
		{
			if (eventKey.Keycode >= Key.Key1 && eventKey.Keycode <= Key.Key9)
			{
				long num = (eventKey.Keycode - Key.Key1) + 1;
				GD.Print(num);
				WearMask((int)num);
			}
		}
	}

	public bool HasMask(int mask)
	{
		return MaskInventory.Contains(mask);
	}

	public void AddMask(int mask)
	{
		MaskInventory.Add(mask);
		WornMasks.Clear();
		WornMasks.Add(mask);
		
		EmitSignal(SignalName.WornMaskChanged, mask);
	}

	public int GetWornMask()
	{
		return WornMasks[0];
	}

	public void WearMask(int mask)
	{
		WornMasks.Clear();
		WornMasks.Add(mask);
		
		Scene.UI.ToggleSelectBorder(true, mask);
		
		EmitSignal(SignalName.WornMaskChanged, mask);
	}

	public void AddMaskPart(int part)
	{
		MaskParts.Add(part);
	}

	public bool HasAllParts()
	{
		return MaskParts.Count == MaskPartCount;
	}

	public void Reset()
	{
		MaskInventory.Clear();
		WornMasks.Clear();
		Position = Game.Instance.Maze.StartPos.Position;
		
		Game.Instance.Maze.DeactivatePlatforms();
		Game.Instance.Maze.ShowMasks();
		Game.Instance.Maze.HideSpecial();
	}
}
