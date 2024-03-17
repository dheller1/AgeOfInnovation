using AoICore;
using AoICore.Commands;

namespace AoIWPFGui.ViewModels
{
	public class CommandHistoryViewModel : ReactiveObject
	{
		private readonly AoIGame _game;

		public CommandHistoryViewModel(AoICore.AoIGame game, CommandHistory history) {
			_game = game;
			History = history;
		}

		public CommandHistory History { get; }

		internal void Redo(object sender, RoutedEventArgs e) {
			//_game.;
		}

		internal void Undo(object sender, RoutedEventArgs e) {
			_game.UndoCommand();
		}
	}
}
