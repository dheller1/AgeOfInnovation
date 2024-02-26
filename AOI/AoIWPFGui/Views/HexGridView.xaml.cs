using AoIWPFGui.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AoIWPFGui.Views
{
	public partial class HexGridView : ReactiveUserControl<HexGridViewModel>
	{
		public HexGridView() {
			InitializeComponent();

			this.WhenActivated(disposableRegistration => {
				this.OneWayBind(ViewModel,
					vm => vm.Shapes,
					view => view.itemsControl.ItemsSource)
				.DisposeWith(disposableRegistration);
			});
		}
	}
}
