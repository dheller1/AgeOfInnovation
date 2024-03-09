using AoIWPFGui.ViewModels;
using System.Reactive.Disposables;

namespace AoIWPFGui.Views
{
	public partial class ResourceCostView : ReactiveUserControl<ResourceCostViewModel>, IViewFor<ResourceCostViewModel>
	{
		public ResourceCostView() {
			InitializeComponent();
			this.WhenActivated(disposableRegistration => {
				
				this.OneWayBind(ViewModel,
					vm => vm.CoinsText,
					view => view.CoinsRun.Text)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.ToolsText,
					view => view.ToolsRun.Text)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.CoinsBrush,
					view => view.CoinsRun.Foreground)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.ToolsBrush,
					view => view.ToolsRun.Foreground)
				.DisposeWith(disposableRegistration);

			});
		}
	}
}
