using Godot;
using System;

public abstract partial class Fish : CharacterBody3D
{
    [Export] protected float m_health = 10f;
    [Export] protected float m_speed = 5f;
    public bool Swimming { get; private set; }
    Action m_onArrive;

    #region References
    NavigationAgent3D Navigation { get { return GetNode<NavigationAgent3D>("Navigation"); } }
    AnimationNodeStateMachinePlayback MovementAnimationStateMachine { get { return (AnimationNodeStateMachinePlayback)GetNode<AnimationTree>("AnimationTree").Get("parameters/Movement/playback"); } }
    AnimationNodeStateMachinePlayback GeneralAnimationStateMachine { get { return (AnimationNodeStateMachinePlayback)GetNode<AnimationTree>("AnimationTree").Get("parameters/playback"); } }
    #endregion


    public void GoTo(Vector3 position, Action onArrive = null)
    {
        MovementAnimationStateMachine.Travel("Swim");
        Navigation.TargetPosition = position with { Y = 5 };
        Swimming = true;
        m_onArrive = onArrive;
    }

    protected void OnArrive()
    {
        MovementAnimationStateMachine.Travel("Idle");
        Swimming = false;
        m_onArrive?.Invoke();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Swimming) MovementProcess((float)delta);
    }

    private void MovementProcess(float delta)
    {
        if (Navigation.GetFinalPosition().DistanceTo(GlobalPosition) < 1f) { OnArrive(); return; }
        var nextPos = Navigation.GetNextPathPosition();
        var step = ToLocal(nextPos).Normalized();
        Velocity = step * m_speed;
        MoveAndSlide();

        var visual = GetNode<Node3D>("Skin");
        visual.LookAt(nextPos, Vector3.Up, true);
    }
}
