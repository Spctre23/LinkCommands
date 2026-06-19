using Microsoft.Xna.Framework;
using TShockAPI;

namespace PluginTemplate.Commands;

public class ExampleCommand : Command
{
    public override string Name => "example";

    protected override string Description => "An example command."; 

    protected override string Usage => "/example [test]";

    public override List<string> Permissions => ["example"];

    public override void Execute(CommandArgs args)
    {
        List<string> parameters = args.Parameters;
        if (parameters.Count == 0)
        {
            args.Player.SendMessage("This is an example command.", Color.Lime);
        }
        else if (parameters[0] == "test")
        {
            args.Player.SendMessage("Test message", Color.SkyBlue);
        }
        else
        {
            args.Player.SendMessage("Hello world!", Color.SkyBlue);
        }
    }
}
