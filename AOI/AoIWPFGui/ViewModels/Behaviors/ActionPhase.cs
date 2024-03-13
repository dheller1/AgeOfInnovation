using AoICore.Buildings;
using AoICore.Commands;
using AoICore.Map;
using AoICore.StateMachine.States;
using AoIWPFGui.Util;
using AoIWPFGui.Views;
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
				bool resetPreview = true;

				var hex = cell.TerrainHex;
				var activePlayer = CurrentState.ActivePlayer;
				var map = AssociatedObject.Game.Map;

				if(hex.Terrain == activePlayer.AssociatedTerrain && hex.Building?.Type == BuildingTypes.Workshop) {
					resetPreview = false;
					cell.PreviewBuildingOnMouseOver = BuildingTypes.Guild;
					cell.PopupContent = new ResourceCostView {
						ViewModel = new ResourceCostViewModel(hex.Building.Type.UpgradeOptions.First().Cost, activePlayer)
					};
				}
				else if(hex.Building == null && hex.Terrain != Terrain.River && cell.TerrainHex.IsPlayerAdjacent(activePlayer, map)) {
					resetPreview = false;
					cell.PreviewBuildingOnMouseOver = BuildingTypes.Workshop;
					var cost = BuildingTypes.Workshop.Cost;
					cost.Add(Terraform.GetCost(activePlayer.TerraformingLevel, hex.Terrain, activePlayer.AssociatedTerrain));
					cell.PopupContent = new ResourceCostView {
						ViewModel = new ResourceCostViewModel(cost, activePlayer)
					};
				}
				
				
				if(resetPreview) {
					cell.ResetPreviewBuildingAndPopup();
				}
			}

			if(!wasActive) {
				AssociatedObject.CellMouseDown += OnCellMouseDown;
			}
		}
		
		private void Deactivate() {
			foreach(var cell in AssociatedObject.Cells) {
				cell.ResetPreviewBuildingAndPopup();
			}
			AssociatedObject.CellMouseDown -= OnCellMouseDown;
		}

		private void OnCellMouseDown(TerrainHexViewModel cell, MouseButtonEventArgs e) {
			if(!IsActive) { throw new InvalidOperationException("event should be unsubscribed when inactive!"); }
			if(e.ChangedButton == MouseButton.Left) {
				var player = CurrentState?.ActivePlayer ?? throw new InvalidOperationException();
				if(cell.TerrainHex.Terrain == player.AssociatedTerrain && cell.TerrainHex.Building?.Type == BuildingTypes.Workshop) {
					var cmd = new UpgradeBuildingCommand(player, cell.TerrainHex, cell.TerrainHex.Building.Type.UpgradeOptions.First());
					AssociatedObject.Game.InvokeCommand(cmd);
				}
			}
		}

	}
}
