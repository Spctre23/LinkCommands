using Microsoft.Xna.Framework;
using TShockAPI;

namespace PluginTemplate.Commands;

public class HelpCommand : Command
{
    public override string Name => "help";

    protected override string Description => "Provides information about commands.";

    protected override string Usage => "/help [command]";

    public override List<string> Permissions => ["help"];

    public override void Execute(CommandArgs args)
    {
        List<string> parameters = args.Parameters;
        if (parameters.Count == 0)
        {
            foreach (Command command in PluginTemplate.Instance.CommandManager.commands)
            {
                if (command == this) continue;
                args.Player.SendMessage($"/{command.Name}", Color.Yellow);
            }
            return;
        }

        if (parameters[1] == Name)
        {
            args.Player.SendMessage($"/{Name}\n• {Description}\n• {Usage}", Color.Yellow);
            return;
        }

        args.Player.SendMessage($"Command '/{parameters[0]}' does not exist.", Color.Red);
    }
}
