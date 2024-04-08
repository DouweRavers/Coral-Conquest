using Godot;

public partial class Citizen : Fish
{
    public JobTypes JobType { get; private set; }
    public House Home { get; private set; }

    Job m_job;

    #region References
    AnimationTree AnimationTree { get { return GetNode<AnimationTree>("AnimationTree"); } }

    protected override int Health { get; set; } = 10;
    protected override int AttackPoint { get; set; } = 3;
    #endregion

    public void SetIndicator(bool enable) { GetNode<Node3D>("Indicator").Visible = enable; }

    public void SetJobType(JobTypes jobType, Workplace building = null)
    {
        JobType = jobType;
        m_job?.Leave();
        m_job = jobType switch
        {
            JobTypes.VILLAGER => null,
            JobTypes.FARMER => new FarmJob(),
            JobTypes.WOODCUTTER => new WoodcutterJob(),
            JobTypes.BUILDER => new BuilderJob(),
            JobTypes.SOLDIER => new SoldierJob(),
            _ => throw new System.Exception()
        };
        if (m_job != null)
        {
            AddChild(m_job);
            m_job.AssignJob(this, building);
        }
        Reskin(jobType);
        if (jobType == JobTypes.SOLDIER) { Health = 25; AttackPoint = 6; }
    }

    public void OnSelect()
    {
        Game.Instance.Player.SetCitizenPicker(this);
    }

    public void SetHome(House home) { Home = home; }

    public override void _Ready()
    {
        SetJobType(JobTypes.VILLAGER);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        m_job?.WorkProcess();
    }

    protected override void Die()
    {
        Home.Inhabitants.Remove(this);
        m_job?.Leave();
        base.Die();
    }

    private void Reskin(JobTypes jobType)
    {
        GetNode("Skin")?.Free();
        var newSkin = ResourceLoader.Load<PackedScene>(jobType switch
        {
            JobTypes.VILLAGER => "res://game/fish/citizen/villager/villager.tscn",
            JobTypes.FARMER => "res://game/fish/citizen/farmer/farmer.tscn",
            JobTypes.WOODCUTTER => "res://game/fish/citizen/woodcutter/woodcutter.tscn",
            JobTypes.BUILDER => "res://game/fish/citizen/builder/builder.tscn",
            JobTypes.SOLDIER => "res://game/fish/citizen/soldier/soldier.tscn",
            _ => throw new System.Exception()
        }).Instantiate();
        AddChild(newSkin);
        newSkin.Name = "Skin";
    }

}
