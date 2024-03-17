namespace AoICore.Commands
{
	/// <summary>
	/// An instance which manages execution of commands and properly adds them to the history.
	/// </summary>
	internal class CommandExecutor
	{
		public CommandHistory History { get; } = new();

		public void ExecuteCommand(ICommand command) {
			command.Execute();
			History.AddExecuted(command);
		}
	}
}
