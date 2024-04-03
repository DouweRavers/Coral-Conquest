using Godot;

namespace RollAbility;
public partial class FreeCamera : Camera3D
{
    [Export(PropertyHint.Range, "0,10,0.01")] public float Sensitivity = 3;
    [Export(PropertyHint.Range, "0,10,0.01")] public float SpeedScale = 1.17f;
    [Export(PropertyHint.Range, "1,100,0.1")] public float BoostSpeedMultiplier = 3.0f;
    [Export] public float MaxSpeed = 1000;
    [Export] public float MinSpeed = 0.2f;

    private float m_velocity = 5;
    private bool m_enableInput = true;

    public override void _Input(InputEvent @event)
    {
        if (!Current || !m_enableInput)
            return;

        if (Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            if (@event is InputEventMouseMotion eventMouseMotion)
            {
                Rotation -= new Vector3(0, 1, 0) * eventMouseMotion.Relative.X / 1000 * Sensitivity;
                Rotation -= new Vector3(1, 0, 0) * eventMouseMotion.Relative.Y / 1000 * Sensitivity;
                Rotation = Rotation with { X = Mathf.Clamp(Rotation.X, Mathf.Pi / -2, Mathf.Pi / 2) };
            }
        }

        if (@event is InputEventMouseButton eventMouseButton)
        {
            switch (eventMouseButton.ButtonIndex)
            {
                case MouseButton.Right:
                    Input.MouseMode = (eventMouseButton.Pressed ? Input.MouseModeEnum.Captured : Input.MouseModeEnum.Visible);
                    break;
                case MouseButton.WheelUp: // increase fly velocity
                    m_velocity = Mathf.Clamp(m_velocity * SpeedScale, MinSpeed, MaxSpeed);
                    break;
                case MouseButton.WheelDown: // decrease fly velocity
                    m_velocity = Mathf.Clamp(m_velocity / SpeedScale, MinSpeed, MaxSpeed);
                    break;
            }
        }
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("debug_toggle_camera"))
        {
            m_enableInput = !m_enableInput;
            Fov = m_enableInput ? 70 : 75;
        }

        if (!Current || !m_enableInput)
            return;

        var direction = new Vector3(
            (Input.IsPhysicalKeyPressed(Key.D) ? 1 : Input.IsPhysicalKeyPressed(Key.A) ? -1 : 0),
            (Input.IsPhysicalKeyPressed(Key.E) ? 1 : Input.IsPhysicalKeyPressed(Key.Q) ? -1 : 0),
            (Input.IsPhysicalKeyPressed(Key.S) ? 1 : Input.IsPhysicalKeyPressed(Key.W) ? -1 : 0)
        ).Normalized();

        if (Input.IsKeyPressed(Key.Shift)) // boost
            Translate(direction * m_velocity * (float)delta * BoostSpeedMultiplier);
        else
            Translate(direction * m_velocity * (float)delta);
    }
}
