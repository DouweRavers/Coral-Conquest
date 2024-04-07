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
        GetNode<ProgressBar>("BuildingHUD/ProgressBar").Value = GetParent<House>().FoodStorage;
        GetNode<CitizenViewer>("BuildingHUD/CitizenViewer").ShowCitizens(GetParent<House>().Inhabitants.ToArray(), 4);
    }
}
