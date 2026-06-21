using LinkCommands.Helpers;
using TShockAPI;

namespace LinkCommands;

public class LinkCommand
{
    public string Name { get; set; } = "google";
    public string Description { get; set; } = "This is a sample description.";
    public string Link { get; set; } = "google.com";
    public RGB DescriptionColor { get; set; } = new(0, 255, 0);
    public RGB LinkColor { get; set; } = new(0, 132, 255);

    public void Execute(CommandArgs args)
    {
        args.Player.SendMessage($"{Description}", DescriptionColor.R, DescriptionColor.G, DescriptionColor.B);
        args.Player.SendMessage($"{Link}", LinkColor.R, LinkColor.G, LinkColor.B);
    }
}