using AoICore.Commands;
using AoICore.Players;
using AoICore.StateMachine.States;
using Meeple.Util;

namespace AoICore.StateMachine
{
	internal class StateMachine : NotificationBase
	{
		public StateMachine(IEnumerable<IPlayer> players) {
			_currentState = new PlaceInitialWorkshopState(players);
		}

		/// <summary>
		/// The player who is currently required to act (if any)
		/// </summary>
		public IPlayer? ActivePlayer { get => _activePlayer; private set => SetProperty(ref _activePlayer, value); }

		public void ApplyCommand(ICommand command) {
			var newState = CurrentState.ApplyCommand(command);
			command.Execute();
			if(newState != null) {
				CurrentState = newState;
				if(newState is IActivePlayerGameState apgs) {
					ActivePlayer = apgs.ActivePlayer;
				}
				else {
					ActivePlayer = null;
				}
			}
			else {
				CurrentState = new FinishedState();
			}
		}

		public IGameState CurrentState {
			get => _currentState;
			private set => SetProperty(ref _currentState, value);
		}

		private IGameState _currentState;
		private IPlayer? _activePlayer;
	}
}
