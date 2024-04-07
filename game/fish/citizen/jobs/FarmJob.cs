public partial class FarmJob : Job
{
    bool m_carringFood;

    public FarmJob(Citizen citizen, Workplace workplace) : base(citizen, workplace) { }

    public override void WorkProcess()
    {
        if (m_citizen.Swimming) return;
        if (m_carringFood)
        {
            if (m_citizen.Home.GlobalPosition.DistanceTo(m_citizen.GlobalPosition) < 10)
            {
                if (m_citizen.Home.FoodStorage < 10)
                {
                    m_citizen.Home.AddFood();
                    m_carringFood = false;
                }
            }
            else m_citizen.GoTo(m_citizen.Home.GlobalPosition);
        }
        else
        {
            if (m_workplace.GlobalPosition.DistanceTo(m_citizen.GlobalPosition) < 10)
            {
                var farm = (Farm)m_workplace;
                if (0 < farm.AvailableFood) m_carringFood = farm.Harvest();
            }
            else m_citizen.GoTo(m_workplace.GlobalPosition);
        }
    }

}

