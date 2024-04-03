using Godot;

public partial class DebugEnvironmentManager : Node
{
    const string m_testDirectory = "res://debug_environment/";

    public override void _Ready()
    {
        var testEnvPath = m_testDirectory + "debugenv_" + GetTree().CurrentScene.Name + ".tscn";
        if (!ResourceLoader.Exists(testEnvPath)) return;
        var testEnv = ResourceLoader.Load<PackedScene>(testEnvPath).Instantiate();
        AddChild(testEnv);
    }
}
