using Microsoft.Xna.Framework;
using System.Reflection;
using TShockAPI;

namespace PluginTemplate.Commands;

public class CommandManager
{
    public readonly HashSet<Command> commands = [];
    private string MasterCommandPrefix { get; } = "plugin";
    private string MasterCommandPermission { get; } = "templateplugin.commands";

    public void RegisterMasterCommand()
    {
        TShockAPI.Commands.ChatCommands.Add(new(MasterCommandPermission, HandleCommands, MasterCommandPrefix));

        Type baseType = typeof(Command);
        Assembly assembly = Assembly.GetExecutingAssembly();
        foreach (Type type in assembly.GetTypes())
        {
            if (!type.IsSubclassOf(baseType) || type == baseType || type.IsAbstract) continue;

            Command command = Activator.CreateInstance(type) as Command;
            commands.Add(command);
        }
    }

    public void HandleCommands(CommandArgs args)
    {
        foreach (Command command in commands)
        {
            if (args.Parameters.Count == 0)
            {
                args.Player.SendMessage("No subcommand provided.", Color.Red);
                return;
            }

            if (args.Parameters[0] != command.Name) continue;

            foreach (string permission in command.Permissions)
            {
                if (!args.Player.HasPermission($"{MasterCommandPermission}.{permission}"))
                {
                    args.Player.SendMessage("No permission.", Color.Red);
                    return;
                }
            }

            args.Parameters.Remove(command.Name);
            command.Execute(args);
            return;
        }

        args.Player.SendMessage("Invalid subcommand.", Color.Red);
    }
}
