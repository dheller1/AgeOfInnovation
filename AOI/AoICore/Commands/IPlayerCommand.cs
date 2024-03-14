using AoICore.Players;

namespace AoICore.Commands
{
	/// <summary>
	/// A command which is executed on behalf of a specific player
	/// </summary>
	public interface IPlayerCommand : ICommand
	{
		public IPlayer Player { get; }
	}
}
