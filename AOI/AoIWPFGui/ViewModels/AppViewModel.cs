using AoICore;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoIWPFGui.ViewModels
{
	public class AppViewModel : ReactiveObject
	{
		public AppViewModel() {
			var gameStateObserver = Game.WhenAnyValue(game => game.CurrentState);
			HexGridVM = new HexGridViewModel(Game.Map, gameStateObserver);

			Game.TestProceedState();
		}
		
		public HexGridViewModel HexGridVM { get; }

		public AoIGame Game { get; } = new AoIGame();

	}
}
