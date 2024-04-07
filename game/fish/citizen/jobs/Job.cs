using Godot;

public abstract partial class Job : RefCounted
{
    protected Citizen m_citizen;

    public Job(Citizen citizen) { m_citizen = citizen; }

    public abstract void WorkProcess();
    public abstract void Start();
    public abstract void Leave();

}
