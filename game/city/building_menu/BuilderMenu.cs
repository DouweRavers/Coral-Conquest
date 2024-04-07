using Godot;

public partial class BuilderMenu : HBoxContainer
{
    public void SelectBuilding(string buildingName)
    {
        var buildingType = buildingName switch
        {
            "House" => Workplace.BuildingType.HOUSE,
            "Farm" => Workplace.BuildingType.FARM,
            "Woodcutter" => Workplace.BuildingType.WOODCUTTER,
            _ => Workplace.BuildingType.HOUSE
        };
        Game.Instance.Player.SetBuildingPicker(buildingType);
    }
}
