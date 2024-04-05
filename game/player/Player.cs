using Godot;

public partial class Player : Node
{
    #region References
    [ExportCategory("References")]
    [Export] Camera3D m_playerCamera;
    #endregion

    public void ShowBuildingTemplate()
    {
        var ray = new BuildingTemplateRay()
        {
            BuildingTemplate = ResourceLoader.Load<PackedScene>("res://game/buildings/House.tscn").Instantiate<Node3D>()
        };
        AddChild(ray);
    }
}
