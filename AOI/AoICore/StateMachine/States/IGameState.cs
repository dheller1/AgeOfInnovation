using AoICore.Commands;

namespace AoICore.StateMachine.States
{
	public class UnsupportedCommandException(ICommand command)
		: Exception($"Command {command} not supported in current state.")
	{ }

	public interface IGameState
	{
		/// <summary>
		/// Applies <paramref name="command"/> to the game state, transitioning to a new game state.
		/// May throw UnsupportedCommandException exception if the command is not supported in this state.
		/// 
		/// This method can also return null, indicating that the command can be applied but allowing a
		/// higher-order instance (e.g. the game) to determine the next game state, such that it does not
		/// need to be constructed from within the current game state and keeping things separate.
		/// </summary>
		/// <param name="command">The command to apply</param>
		/// <returns>The resulting new game state, or null if the game shall decide its new state.
		/// The new game state should always be a new object.</returns>
		IGameState? ApplyCommand(ICommand command);
	}
}
