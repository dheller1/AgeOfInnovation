using AoICore.Commands;
using AoICore.Players;

namespace AoICore.StateMachine.States
{
	public class AllowGainPowerState : IActivePlayerGameState
	{
		public AllowGainPowerState(IPlayer player, PowerTokens maxGain) {
			ActivePlayer = player;
			MaxGain = maxGain;
		}

		public IPlayer ActivePlayer { get; }
		public PowerTokens MaxGain { get; }

		public IEnumerable<IGameState> ApplyCommand(ICommand command) {
			if(command is ChooseGainPowerCommand or ChooseIgnoreGainPowerCommand) {
				return [];
			}
			throw new UnsupportedCommandException(command);
		}
	}
}
