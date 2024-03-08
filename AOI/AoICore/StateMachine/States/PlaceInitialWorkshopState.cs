using AoICore.Commands;
using AoICore.Players;

namespace AoICore.StateMachine.States
{
	/// <summary>
	/// State which expects a player to place one of their two initial workshops.
	/// </summary>
	public class PlaceInitialWorkshopState : IActivePlayerGameState
	{
		public PlaceInitialWorkshopState(IEnumerable<IPlayer> playerOrder) {
			ArgumentNullException.ThrowIfNull(playerOrder);

			_playerOrder = playerOrder.Concat(playerOrder.Reverse()).ToArray();
			_playerEnumerator = _playerOrder.GetEnumerator();
			_playerEnumerator.MoveNext();
		}

		/// <summary>
		/// Copy ctor used to ensure a new game state object is produced after applying an operation
		/// </summary>
		private PlaceInitialWorkshopState(PlaceInitialWorkshopState state) {
			_playerOrder = state._playerOrder;
			_playerEnumerator = state._playerEnumerator;
		}

		private readonly IEnumerable<IPlayer> _playerOrder;
		private readonly IEnumerator<IPlayer> _playerEnumerator;

		public IPlayer ActivePlayer => _playerEnumerator.Current;

		public IGameState? ApplyCommand(ICommand command) {
			if(!(command is PlaceInitialWorkshopCommand placeCmd)) {
				throw new UnsupportedCommandException(command);
			}

			if(placeCmd.Player != ActivePlayer) {
				throw new InvalidOperationException($"The command must be issued by {ActivePlayer}.");
			}

			if(_playerEnumerator.MoveNext()) {
				return new PlaceInitialWorkshopState(this);
			}
			else {
				return new ActionPhaseState(_playerOrder.Take(_playerOrder.Count() / 2));
			}
		}
	}
}
