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

			var fig = new PathFigure { IsFilled = true, IsClosed = true };
			fig.StartPoint = (Point)(baseVectorQ.Rotated(-30) * radius);
			fig.Segments.Add(new LineSegment((Point)(baseVectorQ.Rotated(+30) * radius), true));
			fig.Segments.Add(new LineSegment((Point)(baseVectorQ.Rotated(+90) * radius), true));
			fig.Segments.Add(new LineSegment((Point)(baseVectorQ.Rotated(+150) * radius), true));
			fig.Segments.Add(new LineSegment((Point)(baseVectorQ.Rotated(+210) * radius), true));
			fig.Segments.Add(new LineSegment((Point)(baseVectorQ.Rotated(+270) * radius), true));

			var geom = new PathGeometry();
			geom.Figures.Add(fig);

			HexagonPath.Fill = ViewModel.Fill;
			HexagonPath.Data = geom;
		}
	}
}
