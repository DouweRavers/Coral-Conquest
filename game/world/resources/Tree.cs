using Godot;
using System;

public partial class Tree : Node3D
{
    public bool Cuttable { get; private set; } = true;

    public void Cut()
    {
        var anim = GetNode<AnimationPlayer>("AnimationPlayer");
        Cuttable = false;
        anim.Play("tree_cut");
        var timer = new Timer();
        AddChild(timer);
        timer.Start(Random.Shared.NextSingle() * 60f + 30f);
        timer.Timeout += () =>
        {
            Cuttable = true;
            anim.Play("tree_grow");
        };
    }
}
