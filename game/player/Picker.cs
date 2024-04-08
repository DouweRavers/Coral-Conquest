using Godot;

public abstract partial class Picker : RayCast3D
{
    const float m_length = 100f;
    protected abstract void OnSelect();
    protected abstract void OnDeselect();

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is not InputEventMouseButton mouseEvent) return;
        if (mouseEvent.IsActionPressed("mouse_select")) OnSelect();
        else if (mouseEvent.IsActionPressed("mouse_deselect")) OnDeselect();
    }

    protected void UpdateCast()
    {
        var mousePosition = GetViewport().GetMousePosition();
        var camera = GetViewport().GetCamera3D();
        GlobalPosition = camera.ProjectRayOrigin(mousePosition);
        TargetPosition = camera.ProjectRayNormal(mousePosition) * m_length;
        ForceRaycastUpdate();
    }
}
