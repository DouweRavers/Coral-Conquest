using Godot;

public partial class Game : Node
{
    public static Game Instance { get; private set; }
    public Player Player { get { return GetNode<Player>("Player"); } }
    public City City { get { return GetNode<City>("City"); } }
    public Game() { Instance = this; }
}
