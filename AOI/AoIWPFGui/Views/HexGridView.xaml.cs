using AoIWPFGui.ViewModels;
using System.Reactive.Disposables;

namespace AoIWPFGui.Views
{
	public partial class HexGridView : ReactiveUserControl<HexGridViewModel>
	{
		public HexGridView() {
			InitializeComponent();

			this.WhenActivated(disposableRegistration => {
				this.OneWayBind(ViewModel,
					vm => vm.Cells,
					view => view.HexItems.ItemsSource)
				.DisposeWith(disposableRegistration);
			});
		}
	}
}
