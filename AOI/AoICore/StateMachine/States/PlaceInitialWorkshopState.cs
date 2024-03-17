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
			_playerIndex = 0;
		}

		/// <summary>
		/// Copy ctor used to ensure a new game state object is produced after applying an operation
		/// </summary>
		private PlaceInitialWorkshopState(PlaceInitialWorkshopState state, int startIndex) {
			if(startIndex < 0 || startIndex >= state._playerOrder.Length) { throw new ArgumentOutOfRangeException(nameof(startIndex)); }
			_playerOrder = state._playerOrder.ToArray();
			_playerIndex = startIndex;
		}

		private readonly IPlayer[] _playerOrder;
		private readonly int _playerIndex;

		public IPlayer ActivePlayer => _playerOrder[_playerIndex];

		public IGameState? ApplyCommand(ICommand command) {
			if(!(command is PlaceInitialWorkshopCommand placeCmd)) {
				throw new UnsupportedCommandException(command);
			}

			if(placeCmd.Player != ActivePlayer) {
				throw new InvalidOperationException($"The command must be issued by {ActivePlayer}.");
			}

			var nextIndex = _playerIndex + 1;
			if(nextIndex < _playerOrder.Length) {
				return new PlaceInitialWorkshopState(this, nextIndex);
			}
			else {
				return new ActionPhaseState(_playerOrder.Take(_playerOrder.Length / 2));
			}
		}
	}
}
