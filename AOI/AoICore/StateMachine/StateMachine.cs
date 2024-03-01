using AoICore.Commands;
using AoICore.Players;
using AoICore.StateMachine.States;
using Meeple.Util;

namespace AoICore.StateMachine
{
	internal class StateMachine : NotificationBase
	{
		private IGameState _currentState;
		private readonly Dictionary<(IGameState, Type), IGameState> _transitions;

		public StateMachine() {
			var players = new[] { new Player("Aaron", Map.Terrain.Desert), new Player("Bob", Map.Terrain.Forest), new Player("Celine", Map.Terrain.Mountain) };

			_currentState = new PlaceInitialWorkshopState(0, players[0]);
			
			_transitions = new(){
				{ (new PlaceInitialWorkshopState(0, players[0]), typeof(PlaceInitialWorkshopCommand)), new PlaceInitialWorkshopState(0, players[1]) },
				{ (new PlaceInitialWorkshopState(0, players[1]), typeof(PlaceInitialWorkshopCommand)), new PlaceInitialWorkshopState(0, players[2]) },
				{ (new PlaceInitialWorkshopState(0, players[2]), typeof(PlaceInitialWorkshopCommand)), new PlaceInitialWorkshopState(1, players[2]) },
				{ (new PlaceInitialWorkshopState(1, players[2]), typeof(PlaceInitialWorkshopCommand)), new PlaceInitialWorkshopState(1, players[1]) },
				{ (new PlaceInitialWorkshopState(1, players[1]), typeof(PlaceInitialWorkshopCommand)), new PlaceInitialWorkshopState(1, players[0]) },
				{ (new PlaceInitialWorkshopState(1, players[0]), typeof(PlaceInitialWorkshopCommand)), new FinishedState() },
			};
		}

		public IGameState? PreviewCommand(ICommand command) {
			if(_transitions.TryGetValue((CurrentState, command.GetType()), out var newState)) {
				return newState;
			}
			return null;
		}

		public void ApplyCommand(ICommand command) {
			var newState = PreviewCommand(command) ?? throw new InvalidOperationException($"Invalid transition {CurrentState} -> {command}");
			command.Execute();
			CurrentState = newState;
		}

		public IGameState CurrentState {
			get => _currentState;
			private set => SetProperty(ref _currentState, value);
		}
	}
}
