using AoICore.Buildings;
using AoICore.Commands;
using AoICore.StateMachine.States;
using AoIWPFGui.Util;

namespace AoIWPFGui.ViewModels.Behaviors
{
	internal class PlaceInitialWorkshopsBehavior : StateBehavior<HexGridViewModel, IGameState>
	{
		public PlaceInitialWorkshopsBehavior(HexGridViewModel associatedObject)
			: base(associatedObject, state => state is PlaceInitialWorkshopState)
		{
		}

		private PlaceInitialWorkshopState? CurrentState => _currentState as PlaceInitialWorkshopState;

		protected override void OnNextState(bool wasActive, bool isActive) {
			if(isActive) {
				Activate(wasActive);
			}
			else if(wasActive) {
				Deactivate();
			}
		}

		private void Activate(bool wasActive) {
			if(CurrentState == null) { throw new InvalidOperationException(); }

			foreach(var cell in AssociatedObject.Cells) {
				if(cell.TerrainHex.Terrain == CurrentState.Player.AssociatedTerrain) {
					cell.PreviewBuildingOnMouseOver = BuildingType.Workshop;
					cell.Opacity = 1.0;
				}
				else {
					cell.ResetPreviewBuilding();
					cell.Opacity = 0.25;
				}
			}

			if(!wasActive) {
				AssociatedObject.CellMouseDown += OnCellMouseDown;
			}
		}
		private void Deactivate() {
			foreach(var cell in AssociatedObject.Cells) {
				ResetCell(cell);
			}
			AssociatedObject.CellMouseDown -= OnCellMouseDown;
		}

		private void OnCellMouseDown(TerrainHexViewModel cell, System.Windows.Input.MouseButtonEventArgs e) {
			if(!IsActive) { throw new InvalidOperationException("event should be unsubscribed when inactive!"); }
			if(e.ChangedButton == System.Windows.Input.MouseButton.Left) {
				var player = CurrentState?.Player ?? throw new InvalidOperationException();
				if(cell.TerrainHex.Terrain == player.AssociatedTerrain) {
					var cmd = new PlaceInitialWorkshopCommand(player, cell.TerrainHex);
					AssociatedObject.Game.InvokeCommand(cmd);
				}
			}
		}

		private static void ResetCell(TerrainHexViewModel cell) {
			cell.ResetPreviewBuilding();
			cell.Opacity = 1.0;
		}
	}
}
