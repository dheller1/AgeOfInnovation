using AoICore;
using System.Reactive.Linq;

namespace AoIWPFGui.ViewModels
{
	public class AppViewModel : ReactiveObject
	{
		public AppViewModel() {
			var gameStateObserver = Game.WhenAnyValue(game => game.CurrentState);
			HexGridVM = new HexGridViewModel(Game, gameStateObserver);
			Player1SummaryVM = new PlayerSummaryViewModel(Game.Players.First());
		}

		public AoIGame Game { get; } = new AoIGame();
		
		public HexGridViewModel HexGridVM { get; }
		public PlayerSummaryViewModel Player1SummaryVM { get; }
	}
}
