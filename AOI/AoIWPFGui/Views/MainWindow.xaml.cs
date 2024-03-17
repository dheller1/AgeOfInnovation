using AoIWPFGui.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Text;
using System.Windows;

namespace AoIWPFGui.Views
{
	public partial class MainWindow : ReactiveWindow<AppViewModel>
	{
		public MainWindow() {
			InitializeComponent();
			ViewModel = new AppViewModel();

			this.WhenActivated(disposableRegistration => {
				this.OneWayBind(ViewModel,
					vm => vm.HexGridVM,
					view => view.hexGridView.ViewModel)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.Player1SummaryVM,
					view => view.Player1Summary.ViewModel)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.Player2SummaryVM,
					view => view.Player2Summary.ViewModel)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.Player3SummaryVM,
					view => view.Player3Summary.ViewModel)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.CommandHistoryVM,
					view => view.CommandHistoryView.ViewModel)
				.DisposeWith(disposableRegistration);
			});
		}
	}
}