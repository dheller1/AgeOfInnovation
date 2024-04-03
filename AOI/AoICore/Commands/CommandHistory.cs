using Meeple.Util;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace AoICore.Commands
{
	public class CommandHistory : NotificationBase, IReadOnlyCollection<ICommand>, IEnumerable<ICommand>, INotifyCollectionChanged
	{
		internal void AddExecuted(ICommand command) {
			RemoveUndoneCommands();

			Commands.Add(command);
			_lastExecutedIndex = Commands.Count - 1;
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, command));
			UpdateProperties();
		}

		internal void Undo() {
			if(LastExecutedCommand == null) { throw new InvalidOperationException(); }

			var undoCmd = LastExecutedCommand;
			undoCmd.Undo();
			--_lastExecutedIndex;
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, undoCmd));
			UpdateProperties();
		}

		internal void Redo() {
			if(Count == 0 || _lastExecutedIndex >= Commands.Count - 1) { throw new InvalidOperationException(); }

			var redoCommand = Commands[_lastExecutedIndex + 1];
			redoCommand.Execute();
			++_lastExecutedIndex;
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, redoCommand));
			UpdateProperties();
		}

		public bool CanUndo => LastExecutedCommand != null;
		public bool CanRedo => Count >= 1 && _lastExecutedIndex < Count - 1;

		public IEnumerator<ICommand> GetEnumerator() => Commands.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => Commands.GetEnumerator();

		public ICommand? LastExecutedCommand => _lastExecutedIndex >= 0 ? Commands[_lastExecutedIndex] : null;

		public int Count => Commands.Count;

		public ObservableCollection<ICommand> Commands = [];

		public event NotifyCollectionChangedEventHandler? CollectionChanged;

		private void UpdateProperties() {
			NotifyPropertyChanged(nameof(CanUndo));
			NotifyPropertyChanged(nameof(CanRedo));
		}

		private void RemoveUndoneCommands() {
			bool lastCommandWasUndone() => Commands.Any() && _lastExecutedIndex < Commands.Count - 1;
			while(lastCommandWasUndone()) {
				Commands.RemoveAt(Commands.Count - 1);
			}
		}

		private int _lastExecutedIndex = -1;
	}
}
