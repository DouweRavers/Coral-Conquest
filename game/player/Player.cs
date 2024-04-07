using Godot;

public partial class Player : Node
{
    public Picker PickerMode { get; private set; }

    public void SetDefaultPicker()
    {
        PickerMode?.QueueFree();
        SetPicker(new DefaultPicker());
    }

    public void SetBuildingPicker(Workplace.BuildingType buildingType)
    {
        var buildingPicker = new BuildingPicker();
        buildingPicker.SetBuilding(buildingType);
        SetPicker(buildingPicker);
    }

    public void SetCitizenPicker(Citizen citizen)
    {
        var citizenPicker = new CitizenPicker();
        citizenPicker.SetCitizen(citizen);
        SetPicker(citizenPicker);
    }

    public void ShowWaypoint(Vector3 position)
    {
        GetNode<Node3D>("Waypoint").GlobalPosition = position;
        GetNode<AnimationPlayer>("Waypoint/AnimationPlayer").Play("go_down");
    }

    public void HideWaypoint()
    {
        GetNode<AnimationPlayer>("Waypoint/AnimationPlayer").Play("go_up");
    }



    public override void _Ready() { SetDefaultPicker(); }

    private void SetPicker(Picker picker)
    {
        PickerMode?.QueueFree();
        PickerMode = picker;
        AddChild(picker);
    }

}
