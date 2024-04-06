using Godot;

public partial class BuilderMenu : HBoxContainer
{
    #region References
    [Export] PackedScene m_house, m_farm;
    #endregion

    public void SelectBuilding(string buildingType)
    {
        var building = buildingType switch
        {
            "House" => m_house,
            "Farm" => m_farm,
            _ => null
        };
        if (building == null) return;

        var buildingPlacer = new BuildingPlacer();
        buildingPlacer.SetBuilding(building);
        Game.Instance.Player.AddChild(buildingPlacer);
    }
}
