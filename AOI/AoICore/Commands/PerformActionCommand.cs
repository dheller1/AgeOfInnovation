using AoICore.Players;

namespace AoICore.Commands
{
	public class PerformActionCommand
	{
		public PerformActionCommand(IPlayer player) {
			Player = player;
		}

		public IPlayer Player { get; }
	}
}
