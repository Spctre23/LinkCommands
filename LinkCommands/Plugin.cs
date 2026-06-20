using Newtonsoft.Json;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace LinkCommands;

[ApiVersion(6, 1)]
public class Plugin(Main game) : TerrariaPlugin(game)
{
    public override string Name => "LinkCommands";
    public override Version Version => new(1, 0, 0);
    public override string Author => "Spctre";
    public override string Description => "A simple plugin for creating custom commands that display web urls.";

    private static readonly string configPath = Path.Combine(TShock.SavePath, "LinkCommands.json");

    private HashSet<Command> commands = [];
    public override void Initialize()
    {
        TShock.Log.ConsoleInfo("======= LinkCommands Plugin Initialized =======");

        GeneralHooks.ReloadEvent += Reload;

        RegisterCommands();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            GeneralHooks.ReloadEvent -= Reload;
        }

        base.Dispose(disposing);
    }

    private List<LinkCommand> Reload()
    {
        List<LinkCommand>? commands = null;

        if (File.Exists(configPath))
        {
            commands = JsonConvert.DeserializeObject<List<LinkCommand>>(File.ReadAllText(configPath));
        }

        if (commands == null)
        {
            commands = [new()];
            File.WriteAllText(configPath, JsonConvert.SerializeObject(commands, Formatting.Indented));
        }

        return commands;
    }

    private void Reload(ReloadEventArgs args)
    {
        TShock.Log.ConsoleInfo("======= LinkCommands Plugin Reloaded =======");

        Commands.ChatCommands.RemoveAll(command => commands.Contains(command));

        RegisterCommands();
    }

    private void RegisterCommands()
    {
        Reload().ForEach(linkCommand =>
        {
            Command command = new($"linkcommands.{linkCommand.Name}", linkCommand.Execute, linkCommand.Name);

            commands.Add(command);
            Commands.ChatCommands.Add(command);

            TShock.Log.ConsoleInfo($"[LinkCommands] Registered link command: {linkCommand.Name}");
        });
    }
}
