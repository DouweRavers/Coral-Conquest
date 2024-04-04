using Godot;

public partial class BuildingTemplateRay : RayCast3D
{
    [Export] public float Length { get; set; } = 30f;

    public Camera3D Camera { get; set; }
    public Node3D BuildingTemplate { set { AddChild(value); } }

    public BuildingTemplateRay()
    {
        CollisionMask = 0x3; // first, second mask
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (Camera is null) return;
        HandleMouseMovementInput(@event);
        HandleMousePressInput(@event);
    }

    private void HandleMouseMovementInput(InputEvent @event)
    {
        if (@event is not InputEventMouseMotion motionEvent) return;
        GlobalPosition = Camera.ProjectRayOrigin(motionEvent.Position);
        TargetPosition = Camera.ProjectRayNormal(motionEvent.Position) * Length;
        ForceRaycastUpdate();
        if (IsColliding()) GetChild<Node3D>(0).GlobalPosition = GetCollisionPoint();
        GetChild<Node3D>(0).Visible = ((CollisionObject3D)GetCollider()).GetCollisionLayerValue(2);
    }
    private void HandleMousePressInput(InputEvent @event)
    {
        if (@event is not InputEventMouseButton eventMouseButton) return;
        switch (eventMouseButton.ButtonIndex)
        {
            case MouseButton.Left:
                var child = GetChild<Node3D>(0);
                child.Reparent((Node3D)GetCollider());
                child.AddChild(new StaticBody3D());
                child.GetChild<Node3D>(0).AddChild(new CollisionShape3D() { Shape = new BoxShape3D() });
                QueueFree();
                break;
            case MouseButton.Right:
                QueueFree();
                break;
            default: return;
        }
    }
}
