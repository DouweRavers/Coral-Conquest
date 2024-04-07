using Godot;

public partial class Citizen : Fish
{
    public enum JobTypes { VILLAGER, FARMER }
    public JobTypes JobType { get; private set; }
    public House Home { get; private set; }

    Job m_job;

    AnimationPlayer m_waypointAnimation;

    #region References
    AnimationTree AnimationTree { get { return GetNode<AnimationTree>("AnimationTree"); } }
    #endregion

    public void SetIndicator(bool enable) { GetNode<Node3D>("Indicator").Visible = enable; }

    public void SetJobType(JobTypes jobType, Building building = null)
    {
        JobType = jobType;
        m_job?.Leave();
        m_job = jobType switch
        {
            JobTypes.VILLAGER => null,
            JobTypes.FARMER => new FarmJob(this, (Farm)building),
            _ => null
        };
        Reskin(jobType);
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
        m_job?.WorkProcess();
    }

    public override void _MouseEnter()
    {

        GetNode<Node3D>("HUD").Visible = true;
    }

    public override void _MouseExit()
    {
        GetNode<Node3D>("HUD").Visible = false;
    }

    private void Reskin(JobTypes jobType)
    {
        GetNode("Skin")?.Free();
        var newSkin = ResourceLoader.Load<PackedScene>(jobType switch
        {
            JobTypes.VILLAGER => "res://game/fish/citizen/villager/villager.tscn",
            JobTypes.FARMER => "res://game/fish/citizen/farmer/farmer.tscn",
            _ => "res://game/fish/citizen/villager/villager.tscn"
        }).Instantiate();
        AddChild(newSkin);
        newSkin.Name = "Skin";
    }

}
