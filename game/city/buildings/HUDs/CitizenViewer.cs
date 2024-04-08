using Godot;

public partial class CitizenViewer : HBoxContainer
{
    public void ShowCitizens(Citizen[] citizens, int maxCount)
    {
        for (int i = 0; i < maxCount; i++)
        {
            var texture = ResourceLoader.Load<Texture2D>(i < citizens.Length ?
                citizens[i].JobType switch
                {
                    JobTypes.VILLAGER => "res://game/fish/citizen/pictures/goldfish.png",
                    JobTypes.FARMER => "res://game/fish/citizen/pictures/coralgrouper.png",
                    JobTypes.WOODCUTTER => "res://game/fish/citizen/pictures/swordfish.png",
                    JobTypes.BUILDER => "res://game/fish/citizen/pictures/clownfish.png",
                    _ => "res://game/fish/citizen/pictures/no_fish.png"
                }
                : "res://game/fish/citizen/pictures/no_fish.png");
            if (i < GetChildCount())
            {
                GetChild<TextureRect>(i).Texture = texture;
            }
            else
            {
                var textureRect = new TextureRect();
                textureRect.Texture = texture;
                AddChild(textureRect);
            }
        }
        if (maxCount <= GetChildCount()) for (int j = maxCount; j < GetChildCount(); j++) GetChild(j).QueueFree();
    }
}
