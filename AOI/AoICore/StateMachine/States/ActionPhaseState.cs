using AoICore.Commands;
using AoICore.Effects;
using AoICore.Players;

namespace AoICore.StateMachine.States
{
	public class ActionPhaseState : IActivePlayerGameState
	{
		public ActionPhaseState(IEnumerable<IPlayer> playerOrder) {
			ArgumentNullException.ThrowIfNull(playerOrder);

			_playerOrder = playerOrder.ToArray();
			_playerIndex = 0;
		}

		public ActionPhaseState(ActionPhaseState other, int startIndex) {
			_playerOrder = other._playerOrder;
			_playerIndex = startIndex;
		}

		private readonly IPlayer[] _playerOrder;
		private readonly int _playerIndex;  // index of the active player

		public IPlayer ActivePlayer => _playerOrder[_playerIndex];
		
		public IEnumerable<IGameState> ApplyCommand(ICommand command) {
			if(command is IPlayerCommand playerCommand && playerCommand.Player != ActivePlayer) {
				throw new InvalidOperationException($"The command must be issued by {ActivePlayer}.");
			}

			if(command is UpgradeBuildingCommand || command is TerraformAndBuildCommand) {
				var followup = new List<IGameState>();
				if(command is ITriggerPowerGain pgCommand) {
					followup.AddRange(PowerGain.InvokeFromBuild(pgCommand.Player, pgCommand.Map, pgCommand.Position));
				}
				var nextIndex = (_playerIndex + 1) % _playerOrder.Length;
				followup.Add(new ActionPhaseState(this, nextIndex));
				return followup;
			}
			throw new UnsupportedCommandException(command);
		}
	}
}
