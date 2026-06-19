using PluginTemplate.Commands;
using Terraria;
using TerrariaApi.Server;
using TShockAPI.Hooks;

namespace PluginTemplate;

/// <summary>
/// The main plugin class should always be decorated with an ApiVersion attribute. The current API Version is 6.1
/// </summary>
[ApiVersion(6, 1)]
public class PluginTemplate(Main game) : TerrariaPlugin(game)
{
    /// <summary>
    /// The name of the plugin.
    /// </summary>
    public override string Name => "Template Plugin";

    /// <summary>
    /// The version of the plugin in its current state.
    /// </summary>
    public override Version Version => new(1, 0, 0);

    /// <summary>
    /// The author(s) of the plugin.
    /// </summary>
    public override string Author => "Ijwu, Spctre";

    /// <summary>
    /// A short, one-line, description of the plugin's purpose.
    /// </summary>
    public override string Description => "A simple, senseless, plugin used to demonstrate the code structure of a TShock and TS-API plugin.";

    public static PluginTemplate Instance { get; private set; }
    public Config? Config { get; private set; }
    public CommandManager CommandManager { get; private set; } = new();

    /// <summary>
    /// Performs plugin initialization logic.
    /// Add want you want to be executed on plugin reload here.
    /// </summary>
    public override void Initialize()
    {
        Instance = this;

        CommandManager.RegisterMasterCommand();

        GeneralHooks.ReloadEvent += Reload;
    }

    /// <summary>
    /// Performs plugin reload logic.
    /// Add your hooks, config file read/writes, etc here.
    /// </summary>
    private void Reload(ReloadEventArgs args)
    {
        Config = Config.Reload();
    }

    /// <summary>
    /// Performs plugin cleanup logic.
    /// Remove your hooks and perform general cleanup here.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            //unhook
            //dispose child objects
            //set large objects to null

            GeneralHooks.ReloadEvent -= Reload;
        }
        base.Dispose(disposing);
    }
}
