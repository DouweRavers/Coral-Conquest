using Godot;

public partial class BuildingHUD : Node3D
{
    [Export] string m_title = "default title";
    [Export] int m_maxStore = 10;
    [Export] Color m_color = Colors.Red;

    public override void _Ready()
    {
        GetNode<Label>("SubViewport/BuildingHUD/ProgressBar/Label").Text = m_title;
        GetNode<ProgressBar>("SubViewport/BuildingHUD/ProgressBar").AddThemeStyleboxOverride("fill", new StyleBoxFlat() { BgColor = m_color });
        GetNode<ProgressBar>("SubViewport/BuildingHUD/ProgressBar").MaxValue = m_maxStore;
    }

    public void UpdateCitizens(Citizen[] citizens, int maxCount) => GetNode<CitizenViewer>("SubViewport/BuildingHUD/CitizenViewer").ShowCitizens(citizens, maxCount);
    public void UpdateBar(int value) => GetNode<ProgressBar>("SubViewport/BuildingHUD/ProgressBar").Value = value;
    public void SetMaxStore(int value)
    {
        m_maxStore = value;
        GetNode<ProgressBar>("SubViewport/BuildingHUD/ProgressBar").MaxValue = m_maxStore;
    }
}
