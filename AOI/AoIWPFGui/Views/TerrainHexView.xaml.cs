using AoIWPFGui.Util;
using AoIWPFGui.ViewModels;
using System.Reactive.Disposables;
using System.Windows.Controls;
using System.Windows.Media;

namespace AoIWPFGui.Views
{
	public partial class TerrainHexView : ReactiveUserControl<TerrainHexViewModel>
	{
		public TerrainHexView() {
			InitializeComponent();

			this.WhenActivated(disposableRegistration => {
				InitHexagonPath();
				SetPosition();

				this.OneWayBind(ViewModel,
					vm => vm.ImageSource,
					view => view.BuildingImage.Source)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.BuildingOpacity,
					view => view.BuildingImage.Opacity)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.Coordinates,
					view => view.CoordinateText.Text)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.Opacity,
					view => view.Opacity)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.PopupContent,
					view => view.PopupContent.Content)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.IsPopupVisible,
					view => view.Popup.IsOpen)
				.DisposeWith(disposableRegistration);

				this.OneWayBind(ViewModel,
					vm => vm.Fill,
					view => view.HexagonPath.Fill)
				.DisposeWith(disposableRegistration);

			});
		}

		private void SetPosition() {
			if(ViewModel == null) { throw new NullReferenceException("ViewModel not set!"); }

			var cell = ViewModel.TerrainHex;
			var baseVectorQ = ViewModel.Orientation == Orientation.Horizontal ? new Vector(1, 0) : new Vector(0, 1);
			var baseVectorR = baseVectorQ.Rotated(60);


			var distCenterToEdgeCenter = ViewModel.CellRadius * Math.Cos(Math.PI / 6.0);  // cos(30°) = sqrt(3)/2
			var position = (2 * distCenterToEdgeCenter + ViewModel.CellMargin) * (cell.Q * baseVectorQ + cell.R * baseVectorR);

			Canvas.SetLeft(this, 100 + position.X);
			Canvas.SetTop(this, 100 + position.Y);
		}

		private void InitHexagonPath() {
			if(ViewModel == null) { throw new NullReferenceException("ViewModel not set!"); }

			var baseVectorQ = ViewModel.Orientation == Orientation.Horizontal ? new Vector(1, 0) : new Vector(0, 1);
			var radius = ViewModel.CellRadius;

			var offset = new Vector(Math.Cos(Math.PI / 6.0) * radius, radius);

			var fig = new PathFigure { IsFilled = true, IsClosed = true };
			fig.StartPoint = (Point)(offset + baseVectorQ.Rotated(-30) * radius);
			fig.Segments.Add(new LineSegment((Point)(offset + baseVectorQ.Rotated(+30) * radius), true));
			fig.Segments.Add(new LineSegment((Point)(offset + baseVectorQ.Rotated(+90) * radius), true));
			fig.Segments.Add(new LineSegment((Point)(offset + baseVectorQ.Rotated(+150) * radius), true));
			fig.Segments.Add(new LineSegment((Point)(offset + baseVectorQ.Rotated(+210) * radius), true));
			fig.Segments.Add(new LineSegment((Point)(offset + baseVectorQ.Rotated(+270) * radius), true));

			var geom = new PathGeometry();
			geom.Figures.Add(fig);
			HexagonPath.Data = geom;
		}

		private bool _isMouseOver = false;
		private bool _isMouseOverPopup = false;
		private void NotifyViewModelMouseOver() {
			if(ViewModel != null) {
				ViewModel.IsMouseOver = _isMouseOver || _isMouseOverPopup;
			}
		}

		private void OnMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			ViewModel?.OnMouseDown(sender, e);
		}

		private void OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
			_isMouseOver = true;
			NotifyViewModelMouseOver();
		}

		private void OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
			_isMouseOver = false;
			NotifyViewModelMouseOver();
		}

		private void OnPopupMouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
			_isMouseOverPopup = true;
			NotifyViewModelMouseOver();
		}

		private void OnPopupMouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
			_isMouseOverPopup = false;
			NotifyViewModelMouseOver();
		}
	}
}
