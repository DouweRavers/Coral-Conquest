using Godot;

public partial class BuildingTemplateRay : RayCast3D
{
    [Export] public float Length { get; set; } = 30f;

    public Node3D BuildingTemplate { set { AddChild(value); } }

    public BuildingTemplateRay()
    {
        CollisionMask = 0x3; // first, second mask
    }

    public override void _Process(double delta)
    {
        ProcessInput();
        ProcessBuildingPosition();
    }

    private void ProcessInput()
    {
        if (Input.IsActionJustPressed("mouse_select"))
        {
            var child = GetChild<Node3D>(0);
            child.Reparent((Node3D)GetCollider());
            child.AddChild(new StaticBody3D());
            child.GetChild<Node3D>(0).AddChild(new CollisionShape3D() { Shape = new BoxShape3D() });
            QueueFree();
        }

        if (Input.IsActionJustPressed("mouse_deselect"))
        {
            GetTree().CreateTimer(0.1f).Timeout += () =>
            {
                if (!Input.IsActionPressed("mouse_deselect")) QueueFree();
            };
        }
    }

    private void ProcessBuildingPosition()
    {
        var mousePosition = GetViewport().GetMousePosition();
        var camera = GetViewport().GetCamera3D();
        GlobalPosition = camera.ProjectRayOrigin(mousePosition);
        TargetPosition = camera.ProjectRayNormal(mousePosition) * Length;
        if (IsColliding()) GetChild<Node3D>(0).GlobalPosition = GetCollisionPoint();
        if (GetCollider() == null) return;
        GetChild<Node3D>(0).Visible = ((CollisionObject3D)GetCollider()).GetCollisionLayerValue(2);
    }
}
