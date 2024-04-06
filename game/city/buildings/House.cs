using Godot;
using System.Collections.Generic;

public partial class House : Building
{
    public List<Citizen> Inhabitants { get; private set; } = new();
    protected override void OnClick()
    {
        if (Inhabitants.Count >= 4) return;
        var popup = GetNode<ConfirmationDialog>("AddFishDialog");
        popup.Confirmed += () =>
        {
            var citizen = ResourceLoader.Load<PackedScene>("res://game/city/fish/goldfish/goldfish.tscn").Instantiate<Citizen>();
            Game.Instance.City.AddChild(citizen);
            citizen.GlobalPosition = ToGlobal(new Vector3(0, 4, 6));
            Inhabitants.Add(citizen);
            for (int i = 0; i < 4; i++)
            {
                var textureRect = GetNode<HBoxContainer>("SubViewport/Control").GetChild<TextureRect>(i);
                textureRect.Texture = ResourceLoader.Load<Texture2D>(i < Inhabitants.Count ? "res://game/city/fish/goldfish/goldfish.png" : "res://game/city/fish/goldfish/no_fish.png");
            }
        };
        popup.PopupCentered();
    }


}
