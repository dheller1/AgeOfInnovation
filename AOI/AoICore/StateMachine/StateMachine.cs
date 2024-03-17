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
		public IPlayer? ActivePlayer => CurrentState is IActivePlayerGameState apgs ? apgs.ActivePlayer : null;
		public CommandHistory CommandHistory => _executor.History;
		public IGameState CurrentState {
			get => _currentState;
			private set => SetProperty(ref _currentState, value);
		}

		public void ApplyCommand(ICommand command) {
			var newState = CurrentState.ApplyCommand(command);
			_executor.ExecuteCommand(command);
			if(newState != null) {
				UpdateState(newState);
			}
			else {
				UpdateState(new FinishedState());
			}
		}

		internal void Undo() {
			CommandHistory.Undo();
			CurrentState = _stateHistory.Pop();
			NotifyPropertyChanged(nameof(ActivePlayer));
		}

		private void UpdateState(IGameState newState) {
			_stateHistory.Push(CurrentState);
			CurrentState = newState;
			NotifyPropertyChanged(nameof(ActivePlayer));
		}

		private IGameState _currentState;
		private readonly CommandExecutor _executor = new();
		private readonly Stack<IGameState> _stateHistory = [];
	}
}
