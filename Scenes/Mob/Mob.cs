using System;
using Godot;

public partial class Mob : CharacterBody3D
{
    [Export]
    public int MinSpeed { get; set; } = 10;

    // Maximum speed of the mob in meters per second
    [Export]
    public int MaxSpeed { get; set; } = 18;

    [Signal]
    public delegate void SquashedEventHandler();

    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }

    public void OnVisibilityNotifierScreenExited()
    {
        QueueFree();
    }

    public void Squash()
    {
        EmitSignal(SignalName.Squashed);
        QueueFree();
    }

    public void Initialize(Vector3 startPosition, Vector3 playerPosition)
    {
        // We position the mob by placing it at startPosition
        // and rotate it towards playerPosition, so it looks at the player.
        LookAtFromPosition(startPosition, playerPosition, Vector3.Up);
        // Rotate this mob randomly within range of -45 and +45 degrees,
        // so that it doesn't move directly towards the player.
        RotateY((float)GD.RandRange(-Mathf.Pi / 4.0, Mathf.Pi / 4.0));

        // We set the z rotation to 0, so that the mob is not tilted.
        Rotation = new Vector3(0, Rotation.Y, 0);

        Position = new Vector3(startPosition.X, startPosition.Y + 1, startPosition.Z);

        // We calculate a random speed (integer).
        int randomSpeed = GD.RandRange(MinSpeed, MaxSpeed);
        // We calculate a forward velocity that represents the speed.
        Velocity = Vector3.Forward * randomSpeed;
        // We then rotate the velocity vector based on the mob's Y rotation
        // in order to move in the direction the mob is looking.
        Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);
    }
}
