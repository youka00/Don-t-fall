using Godot;

public partial class Player : CharacterBody2D
{
    [Export] public float JumpForce = -800f;
    [Export] public float Gravity = 1200f;
    [Export] public float MoveSpeed = 400f;

    private Camera2D _camera;
    private GameManager _gameManager;
    private int _coinsCollected;

    public override void _Ready()
    {
        _camera = GetViewport().GetCamera2D();
        _gameManager = GetTree().CurrentScene as GameManager;

    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        velocity.Y += Gravity * (float)delta;

        if (IsOnFloor() && Input.IsActionJustPressed("ui_accept"))
        {
            velocity.Y = JumpForce;
        }

        velocity.X = Input.GetAxis("left", "right") * MoveSpeed;
        Velocity = velocity;
        MoveAndSlide();

        Position = new Vector2(Mathf.Clamp(Position.X, 50, 1230), Position.Y);

        if (_gameManager != null)
        {
            _gameManager.UpdateScore();
        }

        if (Position.Y > _camera.Position.Y + 400f)
        {
            GetTree().ReloadCurrentScene();
        }
    }

    public void CollectCoin(Coin coin)
    {
        _coinsCollected += coin.Value;
        _gameManager?.UpdateCoins(_coinsCollected);
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Coin coin)
        {
            CollectCoin(coin);
        }
    }
}
