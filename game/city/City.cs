using Godot;

public partial class City : Node
{
    #region References
    [Export] Node3D m_buildings;
    #endregion
    public void AddBuilding(Building building)
    {
        building.Reparent(m_buildings);
        building.SetBuildingMode();
    }
}
