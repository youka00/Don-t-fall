using Godot;

public partial class Platform : StaticBody2D
{
    [Export] public float CollisionWidth = 100f;
    [Export] public float CollisionHeight = 20f;

    public override void _Ready()
    {
        var collision = GetNode<CollisionShape2D>("CollisionShape2D");
        var shape = new RectangleShape2D();
        shape.Size = new Vector2(CollisionWidth, CollisionHeight);
        collision.Shape = shape;
    }
}
