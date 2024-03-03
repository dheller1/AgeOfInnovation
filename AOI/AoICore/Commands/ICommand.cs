namespace AoICore.Commands
{
	public interface ICommand
	{
		bool CanExecute { get; }
		void Execute();
		void Undo();
	}
}
