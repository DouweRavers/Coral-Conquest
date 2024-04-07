using Godot;

public partial class Game : Node
{
    public static Game Instance { get; private set; }
    public Player Player { get { return GetNode<Player>("Player"); } }
    public City City { get { return GetNode<City>("City"); } }
    public World World { get { return GetNode<World>("World"); } }
    public Game() { Instance = this; }
}
