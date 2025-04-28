using Godot;

public partial class Coin : Area2D
{
    [Export] public int Value = 1;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            player.CollectCoin(this);
            QueueFree();
        }
    }
}
