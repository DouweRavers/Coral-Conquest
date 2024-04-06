using Godot;

public partial class BuildingPlacer : RayCast3D
{
    const float m_length = 30f;
    const float m_scrollStrength = Mathf.Pi;

    Building m_building;

    public override void _Ready()
    {
        SetProcess(false);
        GetTree().CreateTimer(0.1f).Timeout += () => { SetProcess(true); };
        SetCollisionMaskValue(1, true);
        SetCollisionMaskValue(2, true);
    }

    public void SetBuilding(PackedScene buildingPackedScene)
    {
        m_building = buildingPackedScene.Instantiate<Building>();
        m_building.SetBlueprintMode();
        AddChild(m_building);
    }

    public override void _Process(double delta)
    {
        ProcessInput();
        ProcessBuildingPosition((float)delta);
    }

    private void ProcessInput()
    {
        if (Input.IsActionJustPressed("mouse_select"))
        {
            Game.Instance.City.AddBuilding(m_building);
            QueueFree();
        }

        if (Input.IsActionJustPressed("mouse_deselect"))
        {
            GetTree().CreateTimer(0.1f).Timeout += () =>
            {
                if (!Input.IsActionPressed("mouse_deselect")) QueueFree();
            };
        }

        if (Input.IsActionJustPressed("building_rotate"))
        {
            m_building.RotateY(Mathf.Pi / 4);
        }
    }

    private void ProcessBuildingPosition(float delta)
    {
        var mousePosition = GetViewport().GetMousePosition();
        var camera = GetViewport().GetCamera3D();
        GlobalPosition = camera.ProjectRayOrigin(mousePosition);
        TargetPosition = camera.ProjectRayNormal(mousePosition) * m_length;
        if (IsColliding()) m_building.GlobalPosition = GetCollisionPoint();
        if (GetCollider() == null) return;
        m_building.Visible = ((CollisionObject3D)GetCollider()).GetCollisionLayerValue(2);
    }

}
