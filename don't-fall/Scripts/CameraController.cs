using Godot;

public partial class CameraController : Camera2D
{
    [Export] public float BaseSpeed = 50f;
    [Export] public float SpeedIncreaseRate = 0.02f;
    [Export] public float MaxSpeed = 300f;

    private float _currentSpeed;
    private Vector2 _baseOffset = new Vector2(640, 360);
    private float _initialY;

    public override void _Ready()
    {
        _currentSpeed = BaseSpeed;
        Position = _baseOffset;
        MakeCurrent();
        _initialY = Position.Y;
    }

    public override void _Process(double delta)
    {
        _currentSpeed = Mathf.Min(_currentSpeed + SpeedIncreaseRate * (float)delta, MaxSpeed);
        Position = new Vector2(_baseOffset.X, Position.Y - _currentSpeed * (float)delta);
    }

    public float GetDistanceTraveled()
    {
        return (_initialY - Position.Y) / 100f;
    }

    public void ResetCamera()
    {
        Position = _baseOffset;
        _currentSpeed = BaseSpeed;
        _initialY = Position.Y;
    }
}
