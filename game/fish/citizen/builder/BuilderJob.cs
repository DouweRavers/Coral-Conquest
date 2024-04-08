public partial class BuilderJob : Job
{
    ConstructionSite m_targetConstruction;
    Lodge m_targetLodge;
    bool m_carringWood;
    public BuilderJob(Citizen citizen, Workplace workplace) : base(citizen, workplace) { }

    public override void WorkProcess()
    {
        if (m_citizen.Swimming) return;
        if (m_carringWood)
        {
            if (!IsInstanceValid(m_targetConstruction)) m_targetConstruction = null;
            if (m_targetConstruction == null)
            {
                m_targetConstruction = Game.Instance.City.GetClosestBuilding<ConstructionSite>(BuildingType.CONSTRUCTION_SITE, m_citizen.GlobalPosition);
                return;
            }
            if (m_targetConstruction.GlobalPosition.DistanceTo(m_citizen.GlobalPosition) < 10)
            {
                m_targetConstruction.AddConstructionWood();
                m_carringWood = false;
            }
            else m_citizen.GoTo(m_targetConstruction.GlobalPosition);
        }
        else
        {
            if (m_targetLodge == null)
            {
                m_targetLodge = Game.Instance.City.GetClosestBuilding<Lodge>(BuildingType.LODGE, m_citizen.GlobalPosition, (l) => 0 < l.StoredWood);
                return;
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

