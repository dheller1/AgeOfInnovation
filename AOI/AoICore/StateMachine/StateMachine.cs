using AoICore.Commands;
using AoICore.Players;
using AoICore.StateMachine.States;
using Meeple.Util;

namespace AoICore.StateMachine
{
	internal class StateMachine : NotificationBase
	{
		private IGameState _currentState;

		public StateMachine(IEnumerable<IPlayer> players) {
			_currentState = new PlaceInitialWorkshopState(players);
		}

		public void ApplyCommand(ICommand command) {
			var newState = CurrentState.ApplyCommand(command);
			command.Execute();
			if(newState != null) {
				CurrentState = newState;
			}
			else {
				CurrentState = new FinishedState();
			}
		}

		public IGameState CurrentState {
			get => _currentState;
			private set => SetProperty(ref _currentState, value);
		}
	}
}
