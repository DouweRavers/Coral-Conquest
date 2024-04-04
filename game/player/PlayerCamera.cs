using Godot;

namespace GuppyEmpire;

public partial class PlayerCamera : Camera3D
{
    [Export(PropertyHint.Range, "0,10,0.01")] float m_sensitivity = 3;
    [Export(PropertyHint.Range, "0,10,0.01")] float m_speedScale = 1.17f;
    [Export(PropertyHint.Range, "1,100,0.1")] float m_boostSpeedMultiplier = 3.0f;
    [Export] float m_maxSpeed = 1000;
    [Export] float m_minSpeed = 0.2f;

    float m_velocity = 5;
    bool m_enableInput = true;

    public override void _UnhandledInput(InputEvent @event)
    {
        HandleMouseMovementInput(@event);
        HandleMousePressInput(@event);
    }

    private void HandleMouseMovementInput(InputEvent @event)
    {
        if (@event is not InputEventMouseMotion eventMouseMotion) return;
        if (Input.MouseMode != Input.MouseModeEnum.Captured) return;
        Rotation -= new Vector3(0, 1, 0) * eventMouseMotion.Relative.X / 1000 * m_sensitivity;
        Rotation -= new Vector3(1, 0, 0) * eventMouseMotion.Relative.Y / 1000 * m_sensitivity;
        Rotation = Rotation with { X = Mathf.Clamp(Rotation.X, Mathf.Pi / -2, Mathf.Pi / 2) };
    }

    private void HandleMousePressInput(InputEvent @event)
    {
        if (@event is not InputEventMouseButton eventMouseButton) return;
        switch (eventMouseButton.ButtonIndex)
        {
            case MouseButton.Middle:
                Input.MouseMode = (eventMouseButton.Pressed ? Input.MouseModeEnum.Captured : Input.MouseModeEnum.Visible);
                break;
            case MouseButton.WheelUp: // increase fly velocity
                m_velocity = Mathf.Clamp(m_velocity * m_speedScale, m_minSpeed, m_maxSpeed);
                break;
            case MouseButton.WheelDown: // decrease fly velocity
                m_velocity = Mathf.Clamp(m_velocity / m_speedScale, m_minSpeed, m_maxSpeed);
                break;
        }
    }

    public override void _Process(double delta)
    {
        var direction = new Vector3(
            Input.GetAxis("move_left", "move_right"),
            Input.GetAxis("move_up", "move_down"),
            Input.GetAxis("move_forward", "move_back")
        ).Normalized();

        if (Input.IsKeyPressed(Key.Shift)) // boost
            TranslateObjectLocal(direction * m_velocity * (float)delta * m_boostSpeedMultiplier);
        else
            TranslateObjectLocal(direction * m_velocity * (float)delta);
    }
}
