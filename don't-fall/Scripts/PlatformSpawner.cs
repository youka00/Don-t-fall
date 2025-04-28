using Godot;
using System;
using System.Collections.Generic;

public partial class PlatformSpawner : Node2D
{
    [Export] public PackedScene PlatformScene;
    [Export] public PackedScene CoinScene;
    [Export] public float MinJumpHeight = 200f;
    [Export] public float MaxJumpHeight = 250f;
    [Export] public float MinHorizontalGap = 200f;
    [Export] public float MaxHorizontalGap = 350f;
    [Export] public float MinVerticalGap = 200f;
    [Export] public float PlayerSafeZone = 300f;
    [Export] public float ViewportWidth = 1000f;
    [Export] public float CoinSpawnChance = 0.5f;

    private Camera2D _camera;
    private CharacterBody2D _player;
    private float _nextSpawnY;
    private Random _random = new Random();
    private List<StaticBody2D> _activePlatforms = new List<StaticBody2D>();
    private List<Area2D> _activeCoins = new List<Area2D>();

    public override void _Ready()
    {
        _camera = GetViewport().GetCamera2D();
        _player = GetParent().GetNode<CharacterBody2D>("Player");
        _nextSpawnY = _player.Position.Y + 300f;
        SpawnInitialPlatforms();
    }

    private void SpawnInitialPlatforms()
    {
        SpawnPlatform(new Vector2(_player.Position.X, _player.Position.Y + 200f));
    }

    public override void _Process(double delta)
    {
        if (_nextSpawnY > _camera.Position.Y - 800f)
        {
            TrySpawnPlatform();
            _nextSpawnY -= MinJumpHeight;
        }
        CleanupPlatforms();
        CleanupCoins();
    }

    private void TrySpawnPlatform()
    {
        Vector2 newPos;
        bool validPosition;
        int attempts = 0;

        do
        {
            float randomX = (float)_random.Next((int)(_player.Position.X - MaxHorizontalGap), (int)(_player.Position.X + MaxHorizontalGap));

            randomX = Mathf.Clamp(randomX, 0 + 50f, ViewportWidth - 50f);

            newPos = new Vector2(randomX, _nextSpawnY);

            validPosition = IsPositionValid(newPos);
            attempts++;
        } while (!validPosition && attempts < 20);

        if (validPosition)
        {
            SpawnPlatform(newPos);
            TrySpawnCoin(newPos);
        }
    }

    private bool IsPositionValid(Vector2 newPos)
    {
        foreach (var platform in _activePlatforms)
        {
            if (Mathf.Abs(platform.Position.Y - newPos.Y) < MinVerticalGap &&
                Mathf.Abs(platform.Position.X - newPos.X) < MinHorizontalGap)
            {
                return false;
            }
        }
        return true;
    }

    private void SpawnPlatform(Vector2 position)
    {
        StaticBody2D platform = PlatformScene.Instantiate<StaticBody2D>();
        platform.Position = position;
        AddChild(platform);
        _activePlatforms.Add(platform);
    }

    private void TrySpawnCoin(Vector2 platformPosition)
    {
        if (GD.Randf() < CoinSpawnChance)
        {
            Area2D coin = CoinScene.Instantiate<Area2D>();
            coin.Position = platformPosition + new Vector2(0, -20f);
            AddChild(coin);
            _activeCoins.Add(coin);
        }
    }

    private void CleanupPlatforms()
	{
		for (int i = _activePlatforms.Count - 1; i >= 0; i--)
		{
			if (_activePlatforms[i] != null && IsInstanceValid(_activePlatforms[i]))
			{
				if (_activePlatforms[i].Position.Y > _camera.Position.Y + 1000f)
				{
					GD.Print("Removing platform at: " + _activePlatforms[i].Position);
					_activePlatforms[i].QueueFree();
					_activePlatforms.RemoveAt(i);
				}
			}
		}
	}

	private void CleanupCoins()
	{
		for (int i = _activeCoins.Count - 1; i >= 0; i--)
		{
			if (_activeCoins[i] != null && IsInstanceValid(_activeCoins[i]))
			{
				if (_activeCoins[i].Position.Y > _camera.Position.Y + 1000f)
				{
					GD.Print("Removing coin at: " + _activeCoins[i].Position);
					_activeCoins[i].QueueFree();
					_activeCoins.RemoveAt(i);
				}
			}
		}
	}

}
