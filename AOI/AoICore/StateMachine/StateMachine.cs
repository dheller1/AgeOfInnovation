using AoICore.Commands;
using AoICore.Players;
using AoICore.StateMachine.States;
using Meeple.Util;

namespace AoICore.StateMachine
{
	internal class StateMachine : NotificationBase
	{
		private IGameState _currentState;

		public StateMachine() {
			var players = new[] { new Player("Aaron", Map.Terrain.Desert), new Player("Bob", Map.Terrain.Forest), new Player("Celine", Map.Terrain.Mountain) };
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
