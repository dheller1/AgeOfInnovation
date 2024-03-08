using AoICore.Commands;
using AoICore.Players;

namespace AoICore.StateMachine.States
{
	public class ActionPhaseState : IActivePlayerGameState
	{
		public ActionPhaseState(IEnumerable<IPlayer> playerOrder) {
			ArgumentNullException.ThrowIfNull(playerOrder);

			_playerOrder = new Queue<IPlayer>(playerOrder);
		}

		private readonly Queue<IPlayer> _playerOrder;
		public IPlayer ActivePlayer => _playerOrder.Peek();
		
		public IGameState? ApplyCommand(ICommand command) {
			if(command is PerformActionCommand actionCmd) {
				if(actionCmd.Player != ActivePlayer) {
					throw new InvalidOperationException($"The command must be issued by {ActivePlayer}.");
				}

				_playerOrder.Enqueue(_playerOrder.Dequeue());
				return new ActionPhaseState(_playerOrder);
			}
			else if(command is UpgradeBuildingCommand upgradeBuildingCmd) {
				if(upgradeBuildingCmd.Player != ActivePlayer) {
					throw new InvalidOperationException($"The command must be issued by {ActivePlayer}.");
				}

				_playerOrder.Enqueue(_playerOrder.Dequeue());
				return new ActionPhaseState(_playerOrder);
			}
			throw new UnsupportedCommandException(command);
		}
	}
}
