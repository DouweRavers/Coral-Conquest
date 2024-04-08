using Godot;
using System;
using System.Linq;

public partial class City : Node
{
    public ConstructionSite AddBuilding(BuildingType buildingType)
    {
        var constructionSide = ResourceLoader.Load<PackedScene>("res://game/city/buildings/construction_site.tscn").Instantiate<ConstructionSite>();
        GetNode("Buildings").AddChild(constructionSide);
        constructionSide.SetConstruction(buildingType, 10); // make building dependant in future
        UpdateVisibilityRange();
        return constructionSide;
    }
    public Citizen AddCitizen()
    {
        var citizen = ResourceLoader.Load<PackedScene>("res://game/fish/citizen/citizen.tscn").Instantiate<Citizen>();
        GetNode("Citizens").AddChild(citizen);
        UpdateVisibilityRange();
        return citizen;
    }

    public int GetBuildingCount(BuildingType type)
    {
        return GetNode("Buildings")
             .GetChildren()
             .Cast<Building>()
             .Where((b) => b.Type == type)
             .Count();
    }

    public int GetBuildingCount<T>(BuildingType type, Func<T, bool> predicate)
    {
        return GetNode("Buildings")
             .GetChildren()
             .Cast<Building>()
             .Where((b) => b.Type == type)
             .Cast<T>()
             .Count(predicate);
    }

    public Building GetClosestBuilding(Vector3 center)
    {
        return GetNode("Buildings")
            .GetChildren()
            .Cast<Building>()
            .OrderBy((b) => center.DistanceSquaredTo(b.GlobalPosition))
            .FirstOrDefault(defaultValue: null);
    }

    public T GetClosestBuilding<T>(BuildingType type, Vector3 center) where T : Building
    {
        return GetNode("Buildings")
            .GetChildren()
            .Cast<Building>()
            .Where((b) => b.Type == type)
            .Cast<T>()
            .OrderBy((b) => center.DistanceSquaredTo(b.GlobalPosition))
            .FirstOrDefault(defaultValue: null);
    }

    public T GetClosestBuilding<T>(BuildingType type, Vector3 center, Func<T, bool> predicate) where T : Building
    {
        return GetNode("Buildings")
            .GetChildren()
            .Cast<Building>()
            .Where((b) => b.Type == type)
            .Cast<T>()
            .OrderBy((b) => center.DistanceSquaredTo(b.GlobalPosition))
            .FirstOrDefault(predicate, null);
    }

    public Citizen GetClosestCitizen(Vector3 globalPosition)
    {
        return GetNode("Citizens")
            .GetChildren()
            .Cast<Citizen>()
            .OrderBy((c) => globalPosition.DistanceSquaredTo(c.GlobalPosition))
            .FirstOrDefault(defaultValue: null);
    }

    private void UpdateVisibilityRange()
    {
        var meshes = FindChildren("*", "MeshInstance3D");
        meshes.Cast<MeshInstance3D>().ToList().ForEach(mesh => mesh.VisibilityRangeEnd = 80f);
    }
}
