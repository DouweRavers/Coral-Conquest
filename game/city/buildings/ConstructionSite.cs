using Godot;

public partial class ConstructionSite : Building
{
    public override BuildingType Type { get; set; } = BuildingType.CONSTRUCTION_SITE;

    BuildingType m_buildingType;
    private int m_woodRequired;
    private int m_woodDelivered;

    public void SetConstruction(BuildingType type, int cost)
    {
        m_buildingType = type;
        m_woodRequired = cost;
        GetNode<BuildingHUD>("HUD").SetMaxStore(cost);
    }

    public void AddConstructionWood()
    {
        m_woodDelivered++;
        if (m_woodDelivered >= m_woodRequired) BuildBuilding();
        UpdateHUD(new Citizen[0], 0, m_woodDelivered);
    }

    public void CheckInitialOverwrite()
    {
        if (m_buildingType switch
        {
            BuildingType.HOUSE => Game.Instance.City.GetBuildingCount(BuildingType.HOUSE) == 0,
            BuildingType.FARM => Game.Instance.City.GetBuildingCount(BuildingType.FARM) == 0,
            BuildingType.CONSTRUCTION_OFFICE => Game.Instance.City.GetBuildingCount(BuildingType.CONSTRUCTION_OFFICE) == 0,
            BuildingType.LODGE => Game.Instance.City.GetBuildingCount(BuildingType.LODGE) == 0,
            _ => false
        }) BuildBuilding();
    }

    private void BuildBuilding()
    {
        var building = ResourceLoader.Load<PackedScene>(m_buildingType switch
        {
            BuildingType.HOUSE => "res://game/city/buildings/house/house.tscn",
            BuildingType.FARM => "res://game/city/buildings/farm/farm.tscn",
            BuildingType.LODGE => "res://game/city/buildings/lodge/lodge.tscn",
            BuildingType.CONSTRUCTION_OFFICE => "res://game/city/buildings/construction_office/construction_office.tscn",
            BuildingType.FORT => "res://game/city/buildings/fort/fort.tscn",
            _ => throw new System.Exception()
        }).Instantiate<Building>();
        GetParent().AddChild(building);
        building.GlobalPosition = GlobalPosition;
        building.Rotation = Rotation;
        Game.Instance.BakeNavigationMesh();
        QueueFree();
    }
}
