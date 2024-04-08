public partial class SoldierJob : Job
{
    Enemy m_target;

    public SoldierJob(Citizen citizen, Workplace workplace) : base(citizen, workplace) { }

    public override void WorkProcess()
    {
        if (!IsInstanceValid(m_target)) m_target = null;
        if (m_target == null)
        {
            var enemy = Game.Instance.World.GetClosestEnemy(m_citizen.GlobalPosition);
            if (enemy?.GlobalPosition.DistanceTo(m_citizen.GlobalPosition) < 40f) m_target = enemy;
            return;
        }
        if (m_target.GlobalPosition.DistanceTo(m_citizen.GlobalPosition) < 3f)
        {
            m_target.Hit(m_citizen.Attack());
        }
        else m_citizen.GoTo(m_target.GlobalPosition);
    }
}

