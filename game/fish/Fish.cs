using Godot;
using System;

public abstract partial class Fish : CharacterBody3D
{
    abstract protected int Health { get; set; }
    abstract protected int AttackPoint { get; set; }

    protected bool canAttack = true;
    protected float m_speed = 5f;
    public bool Swimming { get; private set; }
    Action m_onArrive;

    #region References
    NavigationAgent3D Navigation { get { return GetNode<NavigationAgent3D>("Navigation"); } }
    AnimationNodeStateMachinePlayback MovementAnimationStateMachine { get { return (AnimationNodeStateMachinePlayback)GetNode<AnimationTree>("AnimationTree").Get("parameters/Movement/playback"); } }
    AnimationNodeStateMachinePlayback GeneralAnimationStateMachine { get { return (AnimationNodeStateMachinePlayback)GetNode<AnimationTree>("AnimationTree").Get("parameters/playback"); } }
    #endregion

    public int Attack()
    {
        if (!canAttack) return 0;
        canAttack = false;
        GetTree().CreateTimer(3f).Timeout += () => canAttack = true;

        var hitpoint = Mathf.RoundToInt(AttackPoint * (Random.Shared.NextSingle() * 1f + 0.5f));
        GeneralAnimationStateMachine.Travel("Attack");

        var visual = new Label3D() { Text = "" + hitpoint, FontSize = 80, Billboard = BaseMaterial3D.BillboardModeEnum.Enabled };
        AddChild(visual);
        visual.Position = Vector3.Up * 2;
        GetTree().CreateTimer(2).Timeout += () => visual.QueueFree();

        return hitpoint;
    }
    public void Hit(int attack)
    {
        Health -= attack;
        if (Health < 0) Die();
    }

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

    public override void _MouseEnter()
    {

        GetNode<Node3D>("HUD").Visible = true;
    }

    public override void _MouseExit()
    {
        GetNode<Node3D>("HUD").Visible = false;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Swimming) MovementProcess((float)delta);
    }



    protected virtual void Die()
    {
        GeneralAnimationStateMachine.Travel("Die");
        ProcessMode = ProcessModeEnum.Disabled;
        GetTree().CreateTimer(10f).Timeout += () => QueueFree();
    }


    private void MovementProcess(float delta)
    {
        if (Navigation.GetFinalPosition().DistanceTo(GlobalPosition) < 1f)
        {
            Velocity = Vector3.Zero;
            OnArrive();
            return;
        }
        var nextPos = Navigation.GetNextPathPosition();
        var step = ToLocal(nextPos).Normalized();
        var variation = step.Cross(Vector3.Up).Normalized() * Random.Shared.NextSingle(); // stuck prevention
        Velocity = step * m_speed + variation;
        MoveAndSlide();

        var visual = GetNode<Node3D>("Skin");
        visual.LookAt(nextPos, Vector3.Up, true);
    }
}
