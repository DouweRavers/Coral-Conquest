public partial class FarmJob : Job
{
    bool m_carringFood;
    Farm m_farm;

    public FarmJob(Citizen citizen, Farm farm) : base(citizen) { m_farm = farm; }


    public override void WorkProcess()
    {
        if (m_citizen.Swimming) return;
        if (m_carringFood)
        {
            if (m_citizen.Home.GlobalPosition.DistanceTo(m_citizen.GlobalPosition) < 10)
            {
                if (m_citizen.Home.FoodStorage < 10)
                {
                    m_citizen.Home.AddFood(1);
                    m_carringFood = false;
                }
            }
            else m_citizen.GoTo(m_citizen.Home.GlobalPosition);
        }
        else
        {
            if (m_farm.GlobalPosition.DistanceTo(m_citizen.GlobalPosition) < 10)
            {
                if (0 < m_farm.AvailableFood) m_carringFood = m_farm.Harvest();
            }
            else m_citizen.GoTo(m_farm.GlobalPosition);
        }
    }

    public override void Start() { }
    public override void Leave() { m_farm.RemoveWorker(m_citizen); }

}

