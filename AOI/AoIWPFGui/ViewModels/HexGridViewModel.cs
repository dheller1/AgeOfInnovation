﻿using AoICore.Map;
using AoIWPFGui.Util;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AoIWPFGui.ViewModels
{
	public interface IHexCell {
		int Q { get; }
		int R { get; }

		Brush Fill { get; }
	}

	public class HexCell : IHexCell
	{
		public HexCell(int q, int r, Brush fill) {
			Q = q;
			R = r;
			Fill = fill;
		}

		public int Q { get; }

		public int R { get; }

		public Brush Fill { get; }
	}

	public class HexGridViewModel : ReactiveObject
	{
		private Brush GetBrush(Terrain terrain) {
			switch(terrain) {
				case Terrain.Plains:
					return new SolidColorBrush(Colors.SaddleBrown);
				case Terrain.Wasteland:
					return new SolidColorBrush(Colors.Orange);
				case Terrain.Mountain:
					return new SolidColorBrush(Colors.Gray);
				case Terrain.Swamp:
					return new SolidColorBrush(Colors.DarkSlateGray);
				case Terrain.Lake:
					return new SolidColorBrush(Colors.Blue);
				case Terrain.Forest:
					return new SolidColorBrush(Colors.DarkGreen);
				case Terrain.Desert:
					return new SolidColorBrush(Colors.Yellow);
				case Terrain.River:
					return new SolidColorBrush(Colors.Cyan) { Opacity = 0.3 };
				default:
					throw new ArgumentException();
			}
		}

		public HexGridViewModel(string name) {
			Name = name;

			var smallMap = new SmallMap();


			//Cells = new (smallMap.Select(hex => new HexCell(hex.Q, hex.R, GetBrush(hex.Terrain))));

			//Cells = [new HexCell(0, 0, new SolidColorBrush(Colors.SandyBrown)), new HexCell(0, 1, new SolidColorBrush(Colors.RoyalBlue)), new HexCell(1, 0, new SolidColorBrush(Colors.Teal))];
			/*var r1 = new Rectangle {
				Stroke = new SolidColorBrush(Colors.Black),
				Fill = new SolidColorBrush(Colors.White),
				StrokeThickness = 3,
				Width = 100,
				Height = 100
			};
			Canvas.SetLeft(r1, 300);
			Canvas.SetTop(r1, 75);*/

			//Shapes = [ r1, new Ellipse { Stroke = new SolidColorBrush(Colors.Cyan), Fill = new SolidColorBrush(Colors.Teal), StrokeThickness = 2, Width=75, Height=85 } ];
			//Shapes = [];

			Cells = new(smallMap.Select(hex => new TerrainHexViewModel(hex)));
		}

		public string Name { get; }

		public double CellRadius { get; set; } = 50; // equal to the length of each hexagon's edges
		public double CellMargin { get; set; } = 6;

		public Orientation Orientation { get; set; } = Orientation.Horizontal;

		public ObservableCollection<TerrainHexViewModel> Cells { get; }
	}
}
