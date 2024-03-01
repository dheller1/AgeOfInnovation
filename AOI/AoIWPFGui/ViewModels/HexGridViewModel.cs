using AoICore.Buildings;
using AoICore.Map;
using AoICore.Players;
using AoICore.StateMachine.States;
using AoIWPFGui.Util;
using AoIWPFGui.ViewModels.Behaviors;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
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

	// unused
	internal class GameStateObserver : IObserver<IGameState>
	{
		public void OnCompleted() {
			throw new NotImplementedException();
		}

		public void OnError(Exception error) {
			throw new NotImplementedException();
		}

		public void OnNext(IGameState value) {
			StateChanged?.Invoke(value);
		}

		public event Action<IGameState>? StateChanged = null;
	}

	public class HexGridViewModel : ReactiveObject
	{
		private readonly SmallMap _map;

		public HexGridViewModel(SmallMap map, IObservable<IGameState> gameState) {
			_map = map;
			Cells = new(_map.Select(hex => new TerrainHexViewModel(hex)));

			var placeInitialWorkshopsBehavior = new PlaceInitialWorkshopsBehavior(this);
			gameState.Subscribe(placeInitialWorkshopsBehavior);
		}

		public double CellRadius { get; set; } = 50; // equal to the length of each hexagon's edges
		public double CellMargin { get; set; } = 6;

		public Orientation Orientation { get; set; } = Orientation.Horizontal;

		public ObservableCollection<TerrainHexViewModel> Cells { get; }
	}
}
