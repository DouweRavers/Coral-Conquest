using Godot;
using System.Linq;

public partial class Farm : Workplace
{
    public int AvailableFood { get { return CalculateFood(); } }
    protected override int WorkerLimit { get; set; } = 1;
    protected override JobTypes WorkType { get; set; } = JobTypes.FARMER;

    public bool Harvest()
    {
        foreach (FarmPlant plant in GetNode("Fence").GetChildren())
        {
            if (plant.Harvest())
            {
                GetNode<BuildingHUD>("HUD").UpdateBar(AvailableFood);
                return true;
            }
        }
        return false;
    }

    private int CalculateFood()
    {
        return GetNode("Fence").GetChildren()
            .Select((c) => (FarmPlant)c)
            .Where((p) => p.Ripe)
            .Count();
    }
}
