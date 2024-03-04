using AoICore.Buildings;
using AoICore.Commands;
using AoICore.StateMachine.States;
using AoIWPFGui.Util;
using System.Windows.Input;

namespace AoIWPFGui.ViewModels.Behaviors
{
	internal class ActionPhaseBehavior : StateBehavior<HexGridViewModel, IGameState>
	{
		public ActionPhaseBehavior(HexGridViewModel associatedObject)
			: base(associatedObject, state => state is ActionPhaseState)
		{
		}

		private ActionPhaseState? CurrentState => _currentState as ActionPhaseState;

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
				if(cell.TerrainHex.Terrain == CurrentState.ActivePlayer.AssociatedTerrain) {
					if(cell.TerrainHex.Building?.Type == BuildingTypes.Workshop) {
						cell.PreviewBuildingOnMouseOver = BuildingTypes.Guild;
					}
				}
				else {
					cell.ResetPreviewBuilding();
				}
			}

			if(!wasActive) {
				AssociatedObject.CellMouseDown += OnCellMouseDown;
			}
		}
		
		private void Deactivate() {
			foreach(var cell in AssociatedObject.Cells) {
				cell.ResetPreviewBuilding();
			}
			AssociatedObject.CellMouseDown -= OnCellMouseDown;
		}
		private void OnCellMouseDown(TerrainHexViewModel cell, MouseButtonEventArgs e) {
			if(!IsActive) { throw new InvalidOperationException("event should be unsubscribed when inactive!"); }
			if(e.ChangedButton == MouseButton.Left) {
				var player = CurrentState?.ActivePlayer ?? throw new InvalidOperationException();
				if(cell.TerrainHex.Terrain == player.AssociatedTerrain && cell.TerrainHex.Building?.Type == BuildingTypes.Workshop) {
					throw new NotImplementedException();
					//var cmd = new PlaceInitialWorkshopCommand(player, cell.TerrainHex);
					//AssociatedObject.Game.InvokeCommand(cmd);
				}
			}
		}

	}
}
