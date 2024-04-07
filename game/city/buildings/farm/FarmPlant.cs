using Godot;
using System;

public partial class FarmPlant : Node3D
{
    public bool Ripe { get; private set; } = true;

    public bool Harvest()
    {
        if (!Ripe) return false;
        Ripe = false;
        GetNode<AnimationPlayer>("AnimationPlayer").Play("plant_harvest");

        var timer = new Timer();
        AddChild(timer);
        timer.Timeout += () =>
        {
            Ripe = true;
            GetNode<AnimationPlayer>("AnimationPlayer").Play("plant_grow");
        };
        timer.Start(Random.Shared.NextSingle() * 60f + 30f);
        return true;
    }
}
