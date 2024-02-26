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
			});
		}
	}
}