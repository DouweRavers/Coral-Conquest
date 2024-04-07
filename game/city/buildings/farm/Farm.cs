using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class Farm : Building
{
    public List<Citizen> Workers { get; private set; } = new();
    public int AvailableFood { get { return CalculateFood(); } }

    public bool Harvest()
    {
        foreach (FarmPlant plant in GetNode("Fence").GetChildren())
        {
            if (plant.Harvest()) return true;
        }
        return false;
    }

    public void AddWorker(Citizen citizen)
    {
        if (Workers.Contains(citizen) || Workers.Count >= 2) return;
        citizen.SetJobType(Citizen.JobTypes.FARMER, this);
        Workers.Add(citizen);
    }

    public void RemoveWorker(Citizen citizen)
    {
        if (Workers.Contains(citizen)) Workers.Remove(citizen);
    }

    public override void _MouseEnter()
    {
        GetNode<CitizenViewer>("HUD/SubViewport/WorkerViewer").ShowCitizens(Workers.ToArray(), 2);
        base._MouseEnter();
        if (Game.Instance.Player.PickerMode is not CitizenPicker) return;
        GetNode<Node3D>("Indicator").Visible = true;
    }

    public override void _MouseExit()
    {
        base._MouseExit();
        if (Game.Instance.Player.PickerMode is not CitizenPicker) return;
        GetNode<Node3D>("Indicator").Visible = false;
    }


    private int CalculateFood()
    {
        return GetNode("Fence").GetChildren()
            .Select((c) => (FarmPlant)c)
            .Where((p) => p.Ripe)
            .Count();
    }
}
