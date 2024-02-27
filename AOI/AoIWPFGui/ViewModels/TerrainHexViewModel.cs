using AoICore.Map;
using AoIWPFGui.Util;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

			var baseVectorQ = Orientation == Orientation.Horizontal ? new Vector(1, 0) : new Vector(0, 1);
			var baseVectorR = baseVectorQ.Rotated(60);


			var distCenterToEdgeCenter = CellRadius * Math.Cos(Math.PI / 6.0);  // cos(30°) = sqrt(3)/2
			var position = (2 * distCenterToEdgeCenter + CellMargin) * (terrainHex.Q * baseVectorQ + terrainHex.R * baseVectorR);

			CanvasLeft = 100 + position.X;
			CanvasTop = 100 + position.Y;

			if(terrainHex.Terrain == Terrain.Desert) {
				ImageSource = Resources.Get<BitmapImage>("WorkshopYellow");
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

		public ImageSource? ImageSource { get; set; }

		public double CanvasLeft { get; }
		public double CanvasTop { get; }

		public Brush Fill { get; set; }
		public double CellRadius { get; set; } = 50;
		public double CellMargin { get; set; } = 6;

		public Orientation Orientation { get; } = Orientation.Horizontal;
		public TerrainHex TerrainHex { get; }
	}
}
