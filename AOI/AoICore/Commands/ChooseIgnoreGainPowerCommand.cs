using AoICore.Players;

namespace AoICore.Commands
{
	public class ChooseIgnoreGainPowerCommand : IPlayerCommand
	{
		public ChooseIgnoreGainPowerCommand(IPlayer player) {
			Player = player;
		}

		public IPlayer Player { get; }

		public bool CanExecute => true;

		public string AsText => $"{Player} chooses not to gain Power.";

		public void Execute() {
			;
		}

		public void Undo() {
			;
		}
	}
}
