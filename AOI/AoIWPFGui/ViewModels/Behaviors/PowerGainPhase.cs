using AoICore;
using AoICore.StateMachine.States;
using AoIWPFGui.Util;
using System.Text;

namespace AoIWPFGui.ViewModels.Behaviors
{
	internal class PowerGainPhaseBehavior : StateBehavior<HexGridViewModel, IGameState>
	{
		public PowerGainPhaseBehavior(HexGridViewModel associatedObject)
			: base(associatedObject, state => state is AllowGainPowerState)
		{
		}

		private AoIGame Game => AssociatedObject.Game;
		protected override void OnNextState(bool wasActive, bool isActive) {
			if(isActive) {
				var state = (AllowGainPowerState)_currentState!;
				var cost = (int)state.MaxGain - 1;

				var sb = new StringBuilder();
				sb.Append($"{state.ActivePlayer.Name}: Gain {state.MaxGain} Power?");
				if(cost > 0) {
					sb.Append($" (pay {cost} VP)");
				}
				var res = MessageBox.Show(sb.ToString(), "Power gain", MessageBoxButton.YesNo, MessageBoxImage.Question);

				if(res == MessageBoxResult.Yes) {
					Game.InvokeCommand(state.GainPowerCommand());
				}
				else {
					Game.InvokeCommand(state.IgnoreGainPowerCommand());
				}
			}
		}

	}
}
