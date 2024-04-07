using System.Linq;

public partial class BuilderJob : Job
{
    Lodge m_targetLodge;
    bool m_carringWood;
    public BuilderJob(Citizen citizen, Workplace workplace) : base(citizen, workplace) { }

    public override void WorkProcess()
    {
        if (m_citizen.Swimming) return;

        if (m_carringWood)
        {
            var buildings = Game.Instance.City.GetBuildingsInArea(m_citizen.GlobalPosition, 30f);
            // filter for construction which you have to define


            if (m_workplace.GlobalPosition.DistanceTo(m_citizen.GlobalPosition) < 10)
            {
                var lodge = (Lodge)m_workplace;
                if (lodge.StoredWood < 10)
                {
                    lodge.AddWood();
                    m_carringWood = false;
                }
            }
            else m_citizen.GoTo(m_workplace.GlobalPosition);
        }
        else
        {
            if (m_targetLodge == null)
            {
                var lodges = Game.Instance.City.GetBuildingsInArea<Lodge>(m_citizen.GlobalPosition, 30f, BuildingType.LODGE);
                m_targetLodge = lodges.FirstOrDefault((l) => 0 < l.StoredWood, null);
                if (m_targetLodge == null) return;
            }
            if (m_targetLodge.GlobalPosition.DistanceTo(m_citizen.GlobalPosition) < 10)
            {
                if (0 < m_targetLodge.StoredWood)
                {
                    m_carringWood = m_targetLodge.GetWood();
                }
                m_targetLodge = null;
            }
            else m_citizen.GoTo(m_targetLodge.GlobalPosition);
        }
    }
}

