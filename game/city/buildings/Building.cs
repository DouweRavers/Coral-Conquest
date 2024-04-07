using Godot;

public enum BuildingType { HOUSE, FARM, LODGE, CONSTRUCTION_OFFICE, BARACKS }
public abstract partial class Building : StaticBody3D
{

    [Export] public BuildingType Type { get; protected set; }

    public override void _MouseEnter()
    {
        GetNode<BuildingHUD>("HUD").Visible = true;
    }

    public override void _MouseExit()
    {
        GetNode<BuildingHUD>("HUD").Visible = false;
    }

    protected void UpdateHUD(Citizen[] citizens, int maxCount, int value)
    {
        var hud = GetNode<BuildingHUD>("HUD");
        hud.UpdateCitizens(citizens, maxCount);
        hud.UpdateBar(value);
    }
}