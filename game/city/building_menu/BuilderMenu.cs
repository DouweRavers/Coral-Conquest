using Godot;

public partial class BuilderMenu : HBoxContainer
{
    public void SelectBuilding(string buildingName)
    {
        var buildingType = buildingName switch
        {
            "House" => BuildingType.HOUSE,
            "Farm" => BuildingType.FARM,
            "Lodge" => BuildingType.LODGE,
            "ConstructionOffice" => BuildingType.CONSTRUCTION_OFFICE,
            "Fort" => BuildingType.FORT,
            _ => throw new System.Exception()
        };
        Game.Instance.Player.SetBuildingPicker(buildingType);
    }
}
