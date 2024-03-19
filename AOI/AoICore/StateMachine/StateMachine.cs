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
			var followUpStates = CurrentState.ApplyCommand(command);
			_executor.ExecuteCommand(command);
			foreach(var state in followUpStates.Reverse()) {
				_followUpStates.Push(state);
			}
			ProceedState();
		}

		internal void Undo() {
			CommandHistory.Undo();
			CurrentState = _stateHistory.Pop();
			NotifyPropertyChanged(nameof(ActivePlayer));
		}

		private void ProceedState() {
			_stateHistory.Push(CurrentState);
			if(_followUpStates.TryPop(out var newState)) {
				CurrentState = newState;
			}
			else {
				CurrentState = new FinishedState();
			}
			
			NotifyPropertyChanged(nameof(ActivePlayer));
		}

		private IGameState _currentState;
		private readonly Stack<IGameState> _followUpStates = new();
		private readonly CommandExecutor _executor = new();
		private readonly Stack<IGameState> _stateHistory = [];
	}
}
