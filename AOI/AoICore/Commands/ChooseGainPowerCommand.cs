using AoICore.Players;

namespace AoICore.Commands
{
	public class ChooseGainPowerCommand : IPlayerCommand
	{
		public ChooseGainPowerCommand(IPlayer player, PowerTokens gainedTokens) {
			Player = player;
			GainedTokens = gainedTokens;
		}

		public IPlayer Player { get; }
		public PowerTokens GainedTokens { get; }
		public VictoryPoints VictoryPointsCost => (VictoryPoints)((int)GainedTokens - 1);
		public bool CanExecute => Player.Power.MaxGain <= GainedTokens;

		private PowerSupply.GainDetails _gainDetails = new();

		public string AsText {
			get {
				if(VictoryPointsCost == 0) {
					return $"{Player}: Gain {GainedTokens} power.";
				}
				return $"{Player}: Gain {GainedTokens} power (pay {VictoryPointsCost} VP).";
			}
		}

		public void Execute() {
			Player.VictoryPoints -= VictoryPointsCost;
			_gainDetails = Player.Power.Gain(GainedTokens);
		}

		public void Undo() {
			Player.Power.UndoGain(_gainDetails);
			Player.VictoryPoints += VictoryPointsCost;
		}
	}
}
