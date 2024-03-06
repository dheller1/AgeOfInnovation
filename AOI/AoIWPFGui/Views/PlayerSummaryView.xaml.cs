using AoIWPFGui.ViewModels;
using System.Reactive.Disposables;

namespace AoIWPFGui.Views
{
	public partial class PlayerSummaryView : ReactiveUserControl<PlayerSummaryViewModel>
	{
		public PlayerSummaryView() {
			InitializeComponent();
			this.WhenActivated(disposableRegistration => {
				
				this.OneWayBind(ViewModel,
					vm => vm.Player.Name,
					view => view.PlayerNameRun.Text)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.Player.Resources.Coins,
					view => view.CoinsRun.Text)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.Player.Resources.Tools,
					view => view.ToolsRun.Text)
				.DisposeWith(disposableRegistration);

			});
		}
	}
}
