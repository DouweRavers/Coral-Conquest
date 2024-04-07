using Godot;
using System.Linq;

public partial class City : Node
{
    public Workplace AddBuilding(BuildingType buildingType)
    {
        var building = ResourceLoader.Load<PackedScene>(buildingType switch
        {
            BuildingType.HOUSE => "res://game/city/buildings/house/house.tscn",
            BuildingType.FARM => "res://game/city/buildings/farm/farm.tscn",
            BuildingType.LODGE => "res://game/city/buildings/woodcutter/woodcutter.tscn",
            BuildingType.CONSTRUCTION_OFFICE => "res://game/city/buildings/contruction_office/contruction_office.tscn",
            _ => throw new System.Exception()
        }).Instantiate<Workplace>();
        GetNode("Buildings").AddChild(building);
        UpdateVisibilityRange();
        return building;
    }
    public Building[] GetBuildingsInArea(Vector3 center, float radius)
    {
        return GetNode("Buildings")
            .GetChildren()
            .Cast<Building>()
            .Where((b) => center.DistanceTo(b.GlobalPosition) < radius)
            .ToArray();
    }
    public T[] GetBuildingsInArea<T>(Vector3 center, float radius, BuildingType type)
    {
        return GetBuildingsInArea(center, radius)
            .Where((b) => b.Type == type)
            .Cast<T>()
            .ToArray();
    }

    public Citizen AddCitizen()
    {
        var citizen = ResourceLoader.Load<PackedScene>("res://game/fish/citizen/citizen.tscn").Instantiate<Citizen>();
        GetNode("Citizens").AddChild(citizen);
        UpdateVisibilityRange();
        return citizen;
    }

    private void UpdateVisibilityRange()
    {
        var meshes = FindChildren("*", "MeshInstance3D");
        meshes.Cast<MeshInstance3D>().ToList().ForEach(mesh => mesh.VisibilityRangeEnd = 80f);
    }
}
