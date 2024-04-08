public partial class ConstructionOffice : Workplace
{
    public override BuildingType Type { get; set; } = BuildingType.CONSTRUCTION_OFFICE;
    protected override int WorkerLimit { get; set; } = 2;
    protected override JobTypes WorkType { get; set; } = JobTypes.BUILDER;
}
