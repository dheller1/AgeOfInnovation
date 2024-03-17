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

		public void InvokeCommand(ICommand command) {
			StateMachine.ApplyCommand(command);
		}

		public void UndoCommand() {
			StateMachine.Undo();
		}

		public IEnumerable<IPlayer> Players { get; }
		public IGameState CurrentState => StateMachine.CurrentState;
		public IPlayer? ActivePlayer => StateMachine.ActivePlayer;
		
		public IMap Map { get; } = new SmallMap();
		public CommandHistory CommandHistory => StateMachine.CommandHistory;

		internal StateMachine.StateMachine StateMachine { get; }

		private void OnStateMachine_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch(e.PropertyName) {
				case nameof(StateMachine.CurrentState):
					NotifyPropertyChanged(nameof(CurrentState));
					break;
				case nameof(StateMachine.ActivePlayer):
					NotifyPropertyChanged(nameof(ActivePlayer));
					break;
			}
		}
	}
}
