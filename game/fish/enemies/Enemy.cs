public partial class Enemy : Fish
{
    Citizen m_target;

    protected override int Health { get; set; } = 15;
    protected override int AttackPoint { get; set; } = 5;

    public override void _Process(double delta)
    {
        if (!IsInstanceValid(m_target)) m_target = null;
        if (m_target == null)
        {
            var citizen = Game.Instance.City.GetClosestCitizen(GlobalPosition);
            if (citizen?.GlobalPosition.DistanceTo(GlobalPosition) < 20f) m_target = citizen;
            return;
        }
        if (m_target.GlobalPosition.DistanceTo(GlobalPosition) < 3f)
        {
            m_target.Hit(Attack());
        }
        else GoTo(m_target.GlobalPosition);
    }

}
