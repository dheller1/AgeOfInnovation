using AoICore.Players;

namespace AoIWPFGui.ViewModels
{
	public class PlayerSummaryViewModel : ReactiveObject
	{
		public PlayerSummaryViewModel(IPlayer player) {
			Player = player;
		}

		public IPlayer Player { get; }
	}
}
