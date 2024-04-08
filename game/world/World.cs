using Godot;
using System.Linq;

public partial class World : Node
{
    public override void _Ready()
    {
        var meshes = FindChildren("*", "MeshInstance3D");
        meshes.Cast<MeshInstance3D>().ToList().ForEach(mesh => mesh.VisibilityRangeEnd = 80f);
    }

    public Tree GetTreeInArea(Vector3 center, float radius)
    {
        return GetNode("Resources/Forest")
            .FindChildren("Tree*")
            .Cast<Tree>()
            .OrderBy((t) => center.DistanceTo(t.GlobalPosition))
            .FirstOrDefault((t) => t.Cuttable, null);
    }

    internal Enemy GetClosestEnemy(Vector3 globalPosition)
    {
        return GetNode("Enemies")
            .GetChildren()
            .Cast<Enemy>()
            .OrderBy((e) => globalPosition.DistanceSquaredTo(e.GlobalPosition))
            .FirstOrDefault(defaultValue: null);
    }

    internal void RemoveEnemy(Enemy enemy)
    {
        GetNode("Enemies").RemoveChild(enemy);
    }
}
