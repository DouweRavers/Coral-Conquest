using Godot;

public partial class Game : NavigationRegion3D
{
    public static Game Instance { get; private set; }
    public Game() { Instance = this; }
    public Player Player { get { return GetNode<Player>("Player"); } }
    public City City { get { return GetNode<City>("City"); } }
    public World World { get { return GetNode<World>("World"); } }
}
