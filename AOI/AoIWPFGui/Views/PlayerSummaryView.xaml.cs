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


				// resources
				this.OneWayBind(ViewModel,
					vm => vm.Player.Resources.Coins,
					view => view.CoinsRun.Text)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.Player.Resources.Tools,
					view => view.ToolsRun.Text)
				.DisposeWith(disposableRegistration);

				// power
				this.OneWayBind(ViewModel,
					vm => vm.Player.Power.Bowl_I,
					view => view.PowerI_Run.Text)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.Player.Power.Bowl_II,
					view => view.PowerII_Run.Text)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.Player.Power.Bowl_III,
					view => view.PowerIII_Run.Text)
				.DisposeWith(disposableRegistration);



				this.OneWayBind(ViewModel,
					vm => vm.BorderOpacity,
					view => view.BorderRect.Opacity)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.ColorBrush,
					view => view.PlayerColorRect.Fill)
				.DisposeWith(disposableRegistration);
			});
		}
	}
}
