using Godot;

public abstract partial class Building : StaticBody3D
{
    [Export] Control m_HUD;

    public override void _MouseEnter() { m_HUD.Visible = true; }
    public override void _MouseExit() { m_HUD.Visible = false; }


    public void SetBlueprintMode()
    {
        var blueprintMaterial = ResourceLoader.Load<Material>("res://game/city/BuilderMenu/Blueprint.tres");
        var meshes = FindChildren("*", "MeshInstance3D");
        foreach (MeshInstance3D mesh in meshes) { mesh.MaterialOverride = blueprintMaterial; }
        ProcessMode = ProcessModeEnum.Disabled;
    }
    public void SetBuildingMode()
    {
        var meshes = FindChildren("*", "MeshInstance3D");
        foreach (MeshInstance3D mesh in meshes) { mesh.MaterialOverride = null; }
        ProcessMode = ProcessModeEnum.Inherit;
    }

    protected abstract void OnClick();

    void OnInputEvent(Node camera, InputEvent @event, Vector3 position, Vector3 normal, long shapeIdx)
    {
        if (@event is not InputEventMouseButton mouseButtonEvent) return;
        if (mouseButtonEvent.ButtonMask == MouseButtonMask.Left && mouseButtonEvent.Pressed)
        {
            GetTree().CreateTimer(0.3f).Timeout += () =>
            {
                if (Input.IsMouseButtonPressed(MouseButton.Left)) return;
                OnClick();
            };
        }
    }

}
