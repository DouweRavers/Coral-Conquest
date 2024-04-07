using Godot;
using System.Linq;

public partial class World : Node
{
    public override void _Ready()
    {
        var meshes = FindChildren("*", "MeshInstance3D");
        meshes.Cast<MeshInstance3D>().ToList().ForEach(mesh => mesh.VisibilityRangeEnd = 80f);
        GD.Print(GetTreesInArea(Vector3.Zero, 100f));
    }

    public Tree[] GetTreesInArea(Vector3 center, float radius)
    {
        return GetNode("Resources/Forest")
            .FindChildren("Tree*")
            .Cast<Tree>()
            .Where((t) =>
            {
                return center.DistanceTo(t.GlobalPosition) < radius;
            }).ToArray();
    }
}
