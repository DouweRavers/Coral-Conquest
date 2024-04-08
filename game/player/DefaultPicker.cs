using Godot;

public partial class DefaultPicker : Picker
{
    protected override void OnSelect()
    {
        UpdateCast();
        var collider = GetCollider();
        if (collider is House house) house.OnSelect();
        else if (collider is Citizen citizen) citizen.OnSelect();
        else;
        Game.Instance.Player.GetNode<AudioStreamPlayer>("select").Play();
    }
    protected override void OnDeselect() { }

}
