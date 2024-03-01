using AoICore.Commands;
using AoICore.Map;
using AoICore.StateMachine.States;
using Meeple.Util;

namespace AoICore
{
	public class AoIGame : NotificationBase {
		public AoIGame() {
			StateMachine.PropertyChanged += OnStateMachine_PropertyChanged;
		}

		private void OnStateMachine_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if(e.PropertyName == nameof(StateMachine.CurrentState)) {
				NotifyPropertyChanged(nameof(CurrentState));
			}
		}

		public IGameState CurrentState {
			get => StateMachine.CurrentState;
		}

		public void TestProceedState() {
			StateMachine.ApplyCommand(new PlaceInitialWorkshopCommand());
		}

		internal StateMachine.StateMachine StateMachine { get; } = new();
		public SmallMap Map { get; } = new SmallMap();
	}
}
