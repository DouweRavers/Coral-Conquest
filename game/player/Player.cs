using Godot;
using GuppyEmpire;

public partial class Player : Node
{
    #region References
    [ExportCategory("References")]
    [Export] PlayerView m_playerCamera;
    #endregion

    public override void _Process(double delta)
    {
        
    }

    public void ShowBuildingTemplate()
    {
        var ray = new BuildingTemplateRay()
        {
            BuildingTemplate = ResourceLoader.Load<PackedScene>("res://game/buildings/House.tscn").Instantiate<Node3D>()
        };
        AddChild(ray);
    }
}
