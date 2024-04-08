using Godot;
using System.Collections.Generic;

public abstract partial class Workplace : Building
{
    abstract protected int WorkerLimit { get; set; }
    abstract protected JobTypes WorkType { get; set; }

    public List<Citizen> Workers { get; private set; } = new();

    public void AddWorker(Citizen citizen)
    {
        if (Workers.Contains(citizen) || Workers.Count >= WorkerLimit) return;
        citizen.SetJobType(WorkType, this);
        Workers.Add(citizen);
    }

    public void RemoveWorker(Citizen citizen)
    {
        if (Workers.Contains(citizen)) Workers.Remove(citizen);
    }

    public override void _MouseEnter()
    {
        GetNode<BuildingHUD>("HUD").UpdateCitizens(Workers.ToArray(), WorkerLimit);
        base._MouseEnter();
        if (Game.Instance.Player.PickerMode is not CitizenPicker) return;
        GetNode<Node3D>("Indicator").Visible = true;
    }

    public override void _MouseExit()
    {
        GetNode<Node3D>("Indicator").Visible = false;
        base._MouseExit();
        if (Game.Instance.Player.PickerMode is not CitizenPicker) return;
    }
}

