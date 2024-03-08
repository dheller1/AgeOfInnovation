using AoICore;
using System.Reactive.Linq;

namespace AoIWPFGui.ViewModels
{
	public class AppViewModel : ReactiveObject
	{
		public AppViewModel() {
			var gameStateObserver = Game.WhenAnyValue(game => game.CurrentState);
			HexGridVM = new HexGridViewModel(Game, gameStateObserver);
			Player1SummaryVM = new PlayerSummaryViewModel(Game.Players.ElementAt(0));
			Player2SummaryVM = new PlayerSummaryViewModel(Game.Players.ElementAt(1));
			Player3SummaryVM = new PlayerSummaryViewModel(Game.Players.ElementAt(2));
		}

		public AoIGame Game { get; } = new AoIGame();
		
		public HexGridViewModel HexGridVM { get; }
		public PlayerSummaryViewModel Player1SummaryVM { get; }
		public PlayerSummaryViewModel Player2SummaryVM { get; }
		public PlayerSummaryViewModel Player3SummaryVM { get; }
	}
}
