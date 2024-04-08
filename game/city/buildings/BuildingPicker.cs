using Godot;

public partial class BuildingPicker : Picker
{
    BuildingTemplate m_buildingTemplate;

    public override void _Ready()
    {
        SetProcess(false);
        GetTree().CreateTimer(0.1f).Timeout += () => { SetProcess(true); };
        SetCollisionMaskValue(2, true);
    }

    public void SetBuilding(BuildingType buildingType)
    {
        m_buildingTemplate = ResourceLoader.Load<PackedScene>(buildingType switch
        {
            BuildingType.HOUSE => "res://game/city/building_menu/building_templates/house_template.tscn",
            BuildingType.FARM => "res://game/city/building_menu/building_templates/farm_template.tscn",
            BuildingType.LODGE => "res://game/city/building_menu/building_templates/lodge_template.tscn",
            BuildingType.CONSTRUCTION_OFFICE => "res://game/city/building_menu/building_templates/construction_office_template.tscn",
            BuildingType.FORT => "res://game/city/building_menu/building_templates/fort_template.tscn",
            _ => throw new System.Exception()
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
