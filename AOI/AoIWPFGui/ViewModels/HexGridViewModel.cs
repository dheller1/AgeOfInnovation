using AoICore;
using AoICore.Buildings;
using AoICore.Map;
using AoICore.Players;
using AoICore.StateMachine.States;
using AoIWPFGui.Util;
using AoIWPFGui.ViewModels.Behaviors;
using AoIWPFGui.Views;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AoIWPFGui.ViewModels
{
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
		private readonly IMap _map;

		public HexGridViewModel(AoIGame game, IObservable<IGameState> gameState) {
			_map = game.Map;
			Cells = new(_map.Select(hex => new TerrainHexViewModel(hex)));
			Game = game;
			SubscribeCellEvents();

			var placeInitialWorkshopsBehavior = new PlaceInitialWorkshopsBehavior(this);
			gameState.Subscribe(placeInitialWorkshopsBehavior);

			var actionPhaseBehavior = new ActionPhaseBehavior(this);
			gameState.Subscribe(actionPhaseBehavior);
		}

		public double CellRadius { get; set; } = 50; // equal to the length of each hexagon's edges
		public double CellMargin { get; set; } = 6;

		public Orientation Orientation { get; set; } = Orientation.Horizontal;

		public event Action<TerrainHexViewModel, MouseButtonEventArgs>? CellMouseDown;

		public ObservableCollection<TerrainHexViewModel> Cells { get; }
		internal AoIGame Game { get; }

		private void SubscribeCellEvents() {
			foreach(var cell in Cells) {
				cell.MouseDown += OnCell_MouseDown;
			}
		}

		private void OnCell_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			var senderVm = ((TerrainHexView)sender).ViewModel;
			if(senderVm != null) {
				CellMouseDown?.Invoke(senderVm, e);
			}
		}
	}
}
