using AoIWPFGui.ViewModels;
using System.Reactive.Disposables;

namespace AoIWPFGui.Views
{
	public partial class CommandHistoryView : ReactiveUserControl<CommandHistoryViewModel>
	{
		public CommandHistoryView() {
			InitializeComponent();

			this.WhenActivated(disposableRegistration => {

				this.OneWayBind(ViewModel,
					vm => vm.History.Commands,
					view => view.CommandListBox.ItemsSource)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.History.CanUndo,
					view => view.Btn_Undo.IsEnabled)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.History.CanRedo,
					view => view.Btn_Redo.IsEnabled)
				.DisposeWith(disposableRegistration);

				Btn_Undo.Click += ViewModel!.Undo;
				Btn_Redo.Click += ViewModel!.Redo;
			});
		}
	}
}
