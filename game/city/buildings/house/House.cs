using Godot;
using System;
using System.Collections.Generic;

public partial class House : Building
{
    public List<Citizen> Inhabitants { get; private set; } = new();
    public int FoodStorage { get; private set; } = 10;

    public void AddFood(int food)
    {
        if (FoodStorage >= 10) return;
        FoodStorage += food;
        UpdateHUD();
    }

    public void OnSelect()
    {
        AddChild(ResourceLoader.Load<PackedScene>("res://game/city/buildings/house/house_menu.tscn").Instantiate<HouseMenu>());
    }

    public void AddInhabitant()
    {
        if (Inhabitants.Count >= 4) return;
        if (FoodStorage < 5) return;
        FoodStorage -= 5;
        var citizen = Game.Instance.City.AddCitizen();
        citizen.GlobalPosition = ToGlobal(new Vector3(2.5f - Random.Shared.NextSingle() * 5f, 5, 6));
        Inhabitants.Add(citizen);
        citizen.SetHome(this);
        UpdateHUD();
    }

    public override void _MouseEnter()
    {
        UpdateHUD();
        base._MouseEnter();
        if (Game.Instance.Player.PickerMode is DefaultPicker ||
            (Game.Instance.Player.PickerMode is CitizenPicker))
            GetNode<Node3D>("Indicator").Visible = true;
    }

    public override void _MouseExit()
    {
        base._MouseExit();
        if (Game.Instance.Player.PickerMode is not DefaultPicker) return;
        GetNode<Node3D>("Indicator").Visible = false;
    }

    private void UpdateHUD()
    {
        var hud = GetNode<Control>("HUD/SubViewport/HouseHUD");
        hud.GetNode<ProgressBar>("ProgressBar").Value = FoodStorage;
        hud.GetNode<CitizenViewer>("InhabitansViewer").ShowCitizens(Inhabitants.ToArray(), 4);
    }
}
