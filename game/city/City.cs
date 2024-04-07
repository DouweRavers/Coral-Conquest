using Godot;

public partial class City : Node
{
    public Building AddBuilding(Building.BuildingType buildingType)
    {
        var building = ResourceLoader.Load<PackedScene>(buildingType switch
        {
            Building.BuildingType.HOUSE => "res://game/city/buildings/house/house.tscn",
            Building.BuildingType.FARM => "res://game/city/buildings/farm/farm.tscn",
            Building.BuildingType.WOODCUTTER => "res://game/city/buildings/woodcutter/woodcutter.tscn",
            _ => ""
        }).Instantiate<Building>();
        GetNode<Node>("Buildings").AddChild(building);
        return building;
    }

    public Citizen AddCitizen()
    {
        var citizen = ResourceLoader.Load<PackedScene>("res://game/fish/citizen/citizen.tscn").Instantiate<Citizen>();
        GetNode<Node>("Citizens").AddChild(citizen);
        return citizen;
    }
}
