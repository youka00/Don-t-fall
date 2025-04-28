using Godot;

public partial class GameManager : Node2D
{
    [Export] public Camera2D Camera;
    [Export] public Label ScoreLabel;
    [Export] public Label CoinLabel;

    private float _startY;
    private float _score;

    public override void _Ready()
    {
        _startY = Camera.Position.Y;
        ScoreLabel.Text = "Score: 0.0m";
        CoinLabel.Text = "Coins: 0";
    }

    public void UpdateScore()
    {
        _score = Mathf.Max(0, (_startY - Camera.Position.Y) / 100f);
        ScoreLabel.Text = $"Score: {_score:0.0}m";
    }

    public void UpdateCoins(int coins)
    {
        CoinLabel.Text = $"Coins: {coins}";
    }

    public void ResetGame()
    {
        GetTree().ReloadCurrentScene();
    }
}
