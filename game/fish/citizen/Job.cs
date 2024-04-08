using Godot;

public enum JobTypes { VILLAGER, FARMER, WOODCUTTER, BUILDER, SOLDIER }

public abstract partial class Job : RefCounted
{
    protected Citizen m_citizen;
    protected Workplace m_workplace;

    public Job(Citizen citizen, Workplace workplace) { m_citizen = citizen; m_workplace = workplace; }

    public abstract void WorkProcess();
    public void Leave() { m_workplace.RemoveWorker(m_citizen); }

}
