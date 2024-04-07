public partial class DefaultPicker : Picker
{
    protected override void OnSelect()
    {
        UpdateCast();
        var collider = GetCollider();
        if (collider is House house) house.OnSelect();
        if (collider is Citizen citizen) citizen.OnSelect();
    }
    protected override void OnDeselect() { }

    protected override void OnPoint() => throw new System.NotImplementedException();
}
