using AoICore.Commands;
using AoICore.Map;
using AoICore.Players;
using AoICore.StateMachine.States;
using Meeple.Util;

namespace AoICore
{
	public class AoIGame : NotificationBase {
		public AoIGame() {
			Players = new[] { new Player("Aaron", Terrain.Desert), new Player("Bob", Terrain.Forest), new Player("Celine", Terrain.Mountain) };
			StateMachine = new(Players);
			StateMachine.PropertyChanged += OnStateMachine_PropertyChanged;
		}

		public IGameState CurrentState {
			get => StateMachine.CurrentState;
		}

		public void InvokeCommand(ICommand command) {
			StateMachine.ApplyCommand(command);
		}

		public IEnumerable<IPlayer> Players { get; }

		public SmallMap Map { get; } = new SmallMap();

		internal StateMachine.StateMachine StateMachine { get; }

		private void OnStateMachine_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if(e.PropertyName == nameof(StateMachine.CurrentState)) {
				NotifyPropertyChanged(nameof(CurrentState));
			}
		}
	}
}
