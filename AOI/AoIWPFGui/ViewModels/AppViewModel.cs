using AoICore;
using System.Reactive.Linq;

namespace AoIWPFGui.ViewModels
{
	public class AppViewModel : ReactiveObject
	{
		public AppViewModel() {
			var gameStateObserver = Game.WhenAnyValue(game => game.CurrentState);
			var activePlayerObserver = Game.WhenAnyValue(game => game.ActivePlayer);

			HexGridVM = new HexGridViewModel(Game, gameStateObserver);
			Player1SummaryVM = new PlayerSummaryViewModel(Game.Players.ElementAt(0), activePlayerObserver);
			Player2SummaryVM = new PlayerSummaryViewModel(Game.Players.ElementAt(1), activePlayerObserver);
			Player3SummaryVM = new PlayerSummaryViewModel(Game.Players.ElementAt(2), activePlayerObserver);

			CommandHistoryVM = new CommandHistoryViewModel(Game, Game.CommandHistory);
		}

		public AoIGame Game { get; } = new AoIGame();
		
		public HexGridViewModel HexGridVM { get; }
		public CommandHistoryViewModel CommandHistoryVM { get; }
		public PlayerSummaryViewModel Player1SummaryVM { get; }
		public PlayerSummaryViewModel Player2SummaryVM { get; }
		public PlayerSummaryViewModel Player3SummaryVM { get; }
	}
}
