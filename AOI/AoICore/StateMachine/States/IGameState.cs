using AoICore.Commands;

namespace AoICore.StateMachine.States
{
	public class UnsupportedCommandException(ICommand command)
		: Exception($"Command {command} not supported in current state.")
	{ }

	public interface IGameState
	{
		/// <summary>
		/// Applies <paramref name="command"/> to the game state, after which the game transitions to a new state.
		/// May throw UnsupportedCommandException exception if the command is not supported in this state.
		/// 
		/// Returns a list of followup game states which will be active in sequence next.
		/// If the list is empty (or after all followup states, including their return values, are finished),
		/// the game itself chooses the next state.
		/// </summary>
		/// <param name="command">The command to apply</param>
		/// <returns>List of followup game states, which may be empty.</returns>
		IEnumerable<IGameState> ApplyCommand(ICommand command);
	}
}
