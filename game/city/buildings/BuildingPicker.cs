using Godot;

public partial class BuildingPicker : Picker
{
    Workplace.BuildingType m_buildingType;
    BuildingTemplate m_buildingTemplate;

    public override void _Ready()
    {
        SetProcess(false);
        GetTree().CreateTimer(0.1f).Timeout += () => { SetProcess(true); };
        SetCollisionMaskValue(2, true);
    }

    public void SetBuilding(Workplace.BuildingType buildingType)
    {
        m_buildingType = buildingType;
        m_buildingTemplate = ResourceLoader.Load<PackedScene>(buildingType switch
        {
            Workplace.BuildingType.HOUSE => "res://game/city/building_menu/building_templates/house_template.tscn",
            Workplace.BuildingType.FARM => "res://game/city/building_menu/building_templates/farm_template.tscn",
            Workplace.BuildingType.WOODCUTTER => "res://game/city/building_menu/building_templates/woodcutter_template.tscn",
            _ => ""
        }).Instantiate<BuildingTemplate>();
        AddChild(m_buildingTemplate);
    }

    protected override void OnSelect() => m_buildingTemplate.Build();
    protected override void OnDeselect() => Game.Instance.Player.SetDefaultPicker();

    public override void _Process(double delta)
    {
        ProcessInput();
        ProcessBuildingPosition();
    }

    private void ProcessBuildingPosition()
    {
        CollisionMask = 0x2;
        UpdateCast();
        m_buildingTemplate.GlobalPosition = GetCollisionPoint();
        CollisionMask = 0x3;
        UpdateCast();
        var collider = GetCollider() as CollisionObject3D;
        if (collider == null) return;
        m_buildingTemplate.SetPlaceable(collider.GetCollisionLayerValue(2));
    }

    private void ProcessInput()
    {
        if (Input.IsActionJustPressed("building_rotate")) m_buildingTemplate.RotateY(Mathf.Pi / 4);
    }

    protected override void OnPoint() => throw new System.NotImplementedException();
}
