public partial class WoodcutterJob : Job
{
    Tree m_targetTree;
    bool m_carringWood;

    public override void WorkProcess()
    {
        if (m_citizen.Swimming) return;
        if (m_carringWood)
        {
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
            if (m_targetTree == null)
            {
                m_targetTree = ((Lodge)m_workplace).GetCuttableTree();
                return;
            }
            if (m_targetTree.GlobalPosition.DistanceTo(m_citizen.GlobalPosition) < 10)
            {
                if (m_targetTree.Cuttable)
                {
                    m_targetTree.Cut();
                    m_carringWood = true;
                }
                m_targetTree = null;
            }
            else m_citizen.GoTo(m_targetTree.GlobalPosition);
        }
    }
}

