using Godot;

public partial class CitizenPicker : Picker
{
    public Citizen Citizen { get; private set; }

    public void SetCitizen(Citizen citizen)
    {
        Citizen = citizen;
        Citizen.SetIndicator(true);
    }

    protected override void OnSelect()
    {
        if (!IsInstanceValid(Citizen)) Game.Instance.Player.SetDefaultPicker();
        UpdateCast();
        if (GetCollider() is not CollisionObject3D collsionObject) return;
        if (collsionObject.GetCollisionLayerValue(2)) OnFloorClick();
        else if (collsionObject is House house) OnHouseClick(house);
        else if (collsionObject is Workplace workplace) OnWorkClick(workplace);
    }

    protected override void OnDeselect()
    {
        Citizen.SetIndicator(false);
        Game.Instance.Player.SetDefaultPicker();
    }

    protected override void OnPoint() => throw new System.NotImplementedException();

    private void OnFloorClick()
    {
        Game.Instance.Player.ShowWaypoint(GetCollisionPoint());
        Citizen.GoTo(GetCollisionPoint(), () => Game.Instance.Player.HideWaypoint());
    }

    private void OnHouseClick(House house)
    {
        Game.Instance.Player.ShowWaypoint(house.GlobalPosition);
        Citizen.GoTo(house.GlobalPosition, () =>
        {
            Citizen.SetJobType(JobTypes.VILLAGER);
            Game.Instance.Player.HideWaypoint();
        });
        OnDeselect();
    }

    private void OnWorkClick(Workplace workplace)
    {
        Game.Instance.Player.ShowWaypoint(workplace.GlobalPosition);
        Citizen.GoTo(workplace.GlobalPosition, () =>
        {
            workplace.AddWorker(Citizen);
            Game.Instance.Player.HideWaypoint();
        });
        OnDeselect();
    }
}
