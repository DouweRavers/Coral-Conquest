using Godot;

public partial class Minimap : VBoxContainer
{
    public override void _Process(double delta)
    {
        var overCam = GetNode<Node3D>("SubViewportContainer/SubViewport/Camera3D");
        var playerCam = Game.Instance.Player.GetNode<Node3D>("PlayerView/PlayerCamera");
        overCam.GlobalPosition = playerCam.GlobalPosition with { Y = 50 };
    }

    private void SetZoom(float zoom)
    {
        var overCam = GetNode<Camera3D>("SubViewportContainer/SubViewport/Camera3D");
        overCam.Size = zoom;
    }
}
