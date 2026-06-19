using TShockAPI;

namespace PluginTemplate.Commands;

public abstract class Command
{
    public abstract string Name { get; }

    protected abstract string Description { get; }

    protected abstract string Usage { get; }

    public abstract List<string> Permissions { get; }

    public abstract void Execute(CommandArgs args);
}