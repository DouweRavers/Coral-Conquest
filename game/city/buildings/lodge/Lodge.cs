public partial class Lodge : Workplace
{
    public int StoredWood { get; private set; }
    protected override int WorkerLimit { get; set; } = 2;
    protected override JobTypes WorkType { get; set; } = JobTypes.WOODCUTTER;

    private Tree[] m_closeTrees;

    public override void _Ready()
    {
        m_closeTrees = Game.Instance.World.GetTreesInArea(GlobalPosition, 30f);
    }

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
        return true;
    }

    public Tree GetCuttableTree()
    {
        foreach (var tree in m_closeTrees)
        {
            if (tree.Cuttable) return tree;
        }
        return null;
    }
}
