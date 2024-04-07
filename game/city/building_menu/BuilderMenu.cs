using Godot;

public partial class BuilderMenu : HBoxContainer
{
    public void SelectBuilding(string buildingName)
    {
        var buildingType = buildingName switch
        {
            "House" => Building.BuildingType.HOUSE,
            "Farm" => Building.BuildingType.FARM,
            "Woodcutter" => Building.BuildingType.WOODCUTTER,
            _ => Building.BuildingType.HOUSE
        };
        Game.Instance.Player.SetBuildingPicker(buildingType);
    }
}
