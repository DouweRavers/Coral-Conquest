using Godot;

namespace GuppyEmpire;

public partial class PlayerView : CharacterBody3D
{
    [ExportCategory("World bounds")]
    [Export] float m_maxDistanceFromCenter = 500;
    [Export] float m_maxHeight = 50;
    [Export] float m_minHeight = 1;

    [ExportCategory("Control properties")]
    [Export] float m_sensitivity = 0.01f;
    [Export] float m_velocity = 10f;
    [Export] float m_boostSpeedMultiplier = 3.0f;


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
                GlobalBasis.Y * Input.GetAxis("move_down", "move_up") +
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
    }

    private Vector3 CheckOutOfBounds(Vector3 moveVector)
    {
        var expectedHeight = GlobalPosition.Y + moveVector.Y;
        if (expectedHeight < m_minHeight && moveVector.Y < 0) moveVector.Y = 0;
        if (expectedHeight > m_maxHeight && moveVector.Y > 0) moveVector.Y = 0;

        var expectedPosition = GlobalPosition with { Y = 0 } + moveVector with { Y = 0 };
        if (expectedPosition.Length() > m_maxDistanceFromCenter) moveVector = (-expectedPosition.Normalized()) with { Y = 0 };
        return moveVector;
    }
}
