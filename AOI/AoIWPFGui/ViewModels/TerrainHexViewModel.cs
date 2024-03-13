using AoICore.Buildings;
using AoICore.Map;
using AoIWPFGui.Resources;
using AoIWPFGui.Util;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static AoICore.Buildings.BuildingTypes;

namespace AoIWPFGui.ViewModels
{
	public class TerrainHexViewModel : ReactiveObject
	{
		public TerrainHexViewModel(TerrainHex terrainHex) {
			Fill = new SolidColorBrush(GetColor(terrainHex.Terrain));
			if(terrainHex.Terrain == Terrain.River) {
				Fill.Opacity = 0.3;
			}
			TerrainHex = terrainHex;
			terrainHex.PropertyChanged += OnTerrainHex_PropertyChanged;

			var baseVectorQ = Orientation == Orientation.Horizontal ? new Vector(1, 0) : new Vector(0, 1);
			var baseVectorR = baseVectorQ.Rotated(60);

			var distCenterToEdgeCenter = CellRadius * Math.Cos(Math.PI / 6.0);  // cos(30°) = sqrt(3)/2
			var position = (2 * distCenterToEdgeCenter + CellMargin) * (terrainHex.Q * baseVectorQ + terrainHex.R * baseVectorR);

			CanvasLeft = 100 + position.X;
			CanvasTop = 100 + position.Y;
		}

		private void OnTerrainHex_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch(e.PropertyName) {
				case nameof(TerrainHex.Building):
					UpdateVisualization();
					break;
			}
		}

		private void UpdateVisualization() {
			BuildingOpacity = 1.0;
			if(TerrainHex.Building != null) {
				ImageSource = ImageResources.GetBuilding(TerrainHex.Building.Type, TerrainHex.Terrain);
			}
			else {
				ImageSource = null;
			}
		}

		private static Color GetColor(Terrain terrain) {
			return terrain switch {
				Terrain.Plains => Colors.SaddleBrown,
				Terrain.Wasteland => Colors.Orange,
				Terrain.Mountain => Colors.Gray,
				Terrain.Swamp => Colors.DarkSlateGray,
				Terrain.Lake => Colors.Blue,
				Terrain.Forest => Colors.DarkGreen,
				Terrain.Desert => Colors.Yellow,
				Terrain.River => Colors.Cyan,
				_ => throw new ArgumentException(nameof(terrain)),
			};
		}

		public bool IsMouseOver { 
			get => _isMouseOver;
			internal set {
				if(_isMouseOver != value) {
					this.RaiseAndSetIfChanged(ref _isMouseOver, value);
					UpdateMouseOverVisualization();
				}
			}
		}

		public double Opacity {
			get => _opacity; 
			internal set => this.RaiseAndSetIfChanged(ref _opacity, value);
		}

		public double BuildingOpacity {
			get => _buildingOpacity;
			internal set => this.RaiseAndSetIfChanged(ref _buildingOpacity, value);
		}

		public Orientation Orientation { get; } = Orientation.Horizontal;
		public TerrainHex TerrainHex { get; }
		public ImageSource? ImageSource { 
			get => _imageSource; 
			set => this.RaiseAndSetIfChanged(ref _imageSource, value);
		}

		private object? _popupContent;
		public object? PopupContent {
			get => _popupContent;
			set => this.RaiseAndSetIfChanged(ref _popupContent, value);
		}

		public bool IsPopupVisible => _popupContent != null && IsMouseOver;

		public BuildingType? PreviewBuildingOnMouseOver { get; internal set; }
		public void ResetPreviewBuildingAndPopup() {
			PreviewBuildingOnMouseOver = null;
			PopupContent = null;
		}

		public string Coordinates => $"({TerrainHex.Q}, {TerrainHex.R})";

		public double CanvasLeft { get; }
		public double CanvasTop { get; }

		public Brush Fill { get; set; }
		public double CellRadius { get; set; } = 50;
		public double CellMargin { get; set; } = 6;

		private void UpdateMouseOverVisualization() {
			this.RaisePropertyChanged(nameof(IsPopupVisible));
			if(IsMouseOver && PreviewBuildingOnMouseOver != null) {
				ImageSource = ImageResources.GetBuilding(PreviewBuildingOnMouseOver, TerrainHex.Terrain);
				BuildingOpacity = 0.7;
			}
			else {
				UpdateVisualization();
			}
		}

		internal void OnMouseDown(object sender, MouseButtonEventArgs e) {
			MouseDown?.Invoke(sender, e);
		}

		public event Action<object, MouseButtonEventArgs>? MouseDown;

		private double _opacity = 1.0;
		private bool _isMouseOver = false;
		private ImageSource? _imageSource;
		private double _buildingOpacity = 1.0;
	}
}
