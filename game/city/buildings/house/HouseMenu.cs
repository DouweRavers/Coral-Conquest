using Godot;

public partial class HouseMenu : Control
{
    public override void _Ready() { UpdateViewer(); }
    private void AddCitizen()
    {
        GetParent<House>().AddInhabitant();
        UpdateViewer();
    }

    private void UpdateViewer()
    {
        GetNode<ProgressBar>("HouseHUD/ProgressBar").Value = GetParent<House>().FoodStorage;
        GetNode<CitizenViewer>("HouseHUD/InhabitansViewer").ShowCitizens(GetParent<House>().Inhabitants.ToArray(), 4);
    }
}
