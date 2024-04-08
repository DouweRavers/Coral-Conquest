using Godot;

public enum BuildingType { HOUSE, FARM, LODGE, CONSTRUCTION_OFFICE, FORT, CONSTRUCTION_SITE }
public abstract partial class Building : StaticBody3D
{
    abstract public BuildingType Type { get; set; } // replace to actual type checking 

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