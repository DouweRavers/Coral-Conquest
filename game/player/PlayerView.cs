using Godot;

namespace GuppyEmpire;

public partial class PlayerView : CharacterBody3D
{
    [ExportCategory("World bounds")]
    [Export] float m_maxDistanceFromCenter = 10;
    [Export] float m_maxHeight = 5;
    [Export] float m_minHeight = 1;

    [ExportCategory("Control properties")]
    [Export] float m_sensitivity = 0.01f;
    [Export] float m_speedScale = 1.17f;
    [Export] float m_boostSpeedMultiplier = 3.0f;
    [Export] float m_maxSpeed = 1000;
    [Export] float m_minSpeed = 0.2f;

    float m_velocity = 5;
    bool m_enableInput = true;

    public override void _Process(double delta)
    {
        ProcessMouseInput((float)delta);
        ProcessMovementInput((float)delta);
        ProcessOrientationInput((float)delta);
    }

    private void ProcessMovementInput(float delta)
    {
        var directionInput = (// Input direction
                GlobalBasis.X * Input.GetAxis("move_left", "move_right") +
                GlobalBasis.Y * Input.GetAxis("move_up", "move_down") +
                GlobalBasis.Z * Input.GetAxis("move_forward", "move_back")
            ).Normalized();
        var boostInput = Input.IsActionPressed("move_sprint") ? m_boostSpeedMultiplier : 1;
        var moveVector = directionInput * m_velocity * delta * boostInput;
        moveVector = CheckOutOfBounds(moveVector);
        _ = MoveAndCollide(moveVector);
    }

    private void ProcessOrientationInput(float delta)
    {
        if (Input.MouseMode != Input.MouseModeEnum.Captured) return;
        Rotation -= new Vector3(0, 1, 0) * Input.GetLastMouseVelocity().X * delta * m_sensitivity;
        Rotation -= new Vector3(1, 0, 0) * Input.GetLastMouseVelocity().Y * delta / 2 * m_sensitivity;
        Rotation = Rotation with { X = Mathf.Clamp(Rotation.X, Mathf.Pi / -2, Mathf.Pi / 2) };
    }

    private void ProcessMouseInput(float delta)
    {
        bool isRightMouseHold = Input.IsActionPressed("camera_aim");
        Input.MouseMode = (isRightMouseHold ? Input.MouseModeEnum.Captured : Input.MouseModeEnum.Visible);
        float scroll = Input.GetAxis("camera_speed_down", "camera_speed_up");
        m_velocity = Mathf.Clamp(m_velocity += m_speedScale * scroll * delta, m_minSpeed, m_maxSpeed);
    }

    private Vector3 CheckOutOfBounds(Vector3 moveVector)
    {
        var expectedHeight = GlobalPosition.Y + moveVector.Y;
        if (expectedHeight < m_minHeight || expectedHeight > m_maxHeight) moveVector.Y *= -1;
        
        var expectedPosition = GlobalPosition with { Y = 0 } + moveVector with { Y = 0 };
        if (expectedPosition.Length() > m_maxDistanceFromCenter) moveVector = (-1 * moveVector) with { Y = 0 };
        return moveVector;
    }
}
