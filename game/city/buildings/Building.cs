using Godot;

public abstract partial class Building : StaticBody3D
{
    public enum BuildingType { HOUSE, FARM, WOODCUTTER }

    [Export] public BuildingType Type { get; protected set; }

    public override void _MouseEnter()
    {
        GetNode<Node3D>("HUD").Visible = true;
    }

    public override void _MouseExit()
    {
        GetNode<Node3D>("HUD").Visible = false;
    }

}
