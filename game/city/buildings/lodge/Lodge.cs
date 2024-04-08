public partial class Lodge : Workplace
{
    public int StoredWood { get; private set; }
    public override BuildingType Type { get; set; } = BuildingType.LODGE;
    protected override int WorkerLimit { get; set; } = 2;
    protected override JobTypes WorkType { get; set; } = JobTypes.WOODCUTTER;

    public void AddWood()
    {
        if (StoredWood >= 10) return;
        StoredWood++;
        UpdateHUD(Workers.ToArray(), WorkerLimit, StoredWood);
    }

    public bool GetWood()
    {
        if (StoredWood <= 0) return false;
        StoredWood--;
        UpdateHUD(Workers.ToArray(), WorkerLimit, StoredWood);
        return true;
    }

    public Tree GetCuttableTree()
    {
        return Game.Instance.World.GetTreeInArea(GlobalPosition, 30f);
    }
}
