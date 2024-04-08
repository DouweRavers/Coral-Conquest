public partial class Fort : Workplace
{
    public override BuildingType Type { get; set; } = BuildingType.FORT;
    protected override int WorkerLimit { get; set; } = 4;
    protected override JobTypes WorkType { get; set; } = JobTypes.SOLDIER;
}
