public partial class ConstructionOffice : Workplace
{
    protected override int WorkerLimit { get; set; } = 2;
    protected override JobTypes WorkType { get; set; } = JobTypes.BUILDER;
}
