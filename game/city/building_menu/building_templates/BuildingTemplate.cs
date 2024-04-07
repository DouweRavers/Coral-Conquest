using Godot;

public partial class BuildingTemplate : MeshInstance3D
{
    [Export] Building.BuildingType m_buildingType;
    bool m_placeable;

    #region References
    Material PlaceableMaterial { get { return ResourceLoader.Load<Material>("res://game/city/building_menu/placeable.tres"); } }
    Material NonPlaceableMaterial { get { return ResourceLoader.Load<Material>("res://game/city/building_menu/nonplaceable.tres"); } }
    #endregion

    public void SetPlaceable(bool placeable)
    {
        m_placeable = placeable;
        var material = placeable ? PlaceableMaterial : NonPlaceableMaterial;
        var meshes = FindChildren("*", "MeshInstance3D");
        meshes.Add(this);
        foreach (MeshInstance3D mesh in meshes) { mesh.MaterialOverride = material; }
    }

    public void Build()
    {
        if (!m_placeable) return;
        var building = Game.Instance.City.AddBuilding(m_buildingType);
        building.GlobalPosition = GlobalPosition;
        building.GlobalRotation = GlobalRotation;
    }
}
