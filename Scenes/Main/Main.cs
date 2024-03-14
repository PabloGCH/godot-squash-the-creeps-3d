using Godot;
using System;

public partial class Main : Node
{
  [Export]
  public PackedScene MobScene { get; set; }

  private Player _player;

  private PathFollow3D _mobSpawnLocation;

  public void OnMobTimerTimeout()
  {
    // Create a new instance of the Mob scene.
    Mob mob = MobScene.Instantiate<Mob>();

    // Choose a random location on the SpawnPath.
    // We store the reference to the SpawnLocation node.
    // And give it a random offset.
    _mobSpawnLocation.ProgressRatio = GD.Randf();

    Vector3 playerPosition = _player.Position;
    mob.Initialize(_mobSpawnLocation.Position, playerPosition);

    // Spawn the mob by adding it to the Main scene.
    AddChild(mob);
  }

  public void OnPlayerHit() {
    GetNode<Timer>("MobTimer").Stop();
  }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
    _player = GetNode<Player>("Player");
    _mobSpawnLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLocation");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
