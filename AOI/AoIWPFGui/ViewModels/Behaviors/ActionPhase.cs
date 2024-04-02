using AoICore;
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
		private AoIGame Game => AssociatedObject.Game;
		private IMap Map => AssociatedObject.Game.Map;

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

				var upgradeCmd = new UpgradeBuildingCommand(activePlayer, Map, hex, BuildingTypes.Workshop.UpgradeOptions.First());
				if(upgradeCmd.CanExecute_IgnoreCost) {
					resetPreview = false;
					cell.PreviewBuildingOnMouseOver = BuildingTypes.Guild;
					cell.PopupContent = new ResourceCostView {
						ViewModel = new ResourceCostViewModel(hex.Building!.Type.UpgradeOptions.First().Cost, activePlayer)
					};
					_onClickCommands[hex] = upgradeCmd;
				}

				var terraformAndBuildCmd = new TerraformAndBuildCommand(activePlayer, Map, hex);
				if(terraformAndBuildCmd.CanExecute_IgnoreCost) {
					resetPreview = false;
					cell.PreviewBuildingOnMouseOver = BuildingTypes.Workshop;
					cell.PopupContent = new ResourceCostView {
						ViewModel = new ResourceCostViewModel(Terraform.GetTerraformAndBuildCost(activePlayer, hex), activePlayer)
					};
					_onClickCommands[hex] = terraformAndBuildCmd;
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
			_onClickCommands.Clear();
			AssociatedObject.CellMouseDown -= OnCellMouseDown;
		}

		private readonly Dictionary<TerrainHex, AoICore.Commands.ICommand> _onClickCommands = [];

		private void OnCellMouseDown(TerrainHexViewModel cell, MouseButtonEventArgs e) {
			if(!IsActive) { throw new InvalidOperationException("event should be unsubscribed when inactive!"); }
			if(e.ChangedButton == MouseButton.Left) {
				if(_onClickCommands.TryGetValue(cell.TerrainHex, out var command) && command.CanExecute) {
					Game.InvokeCommand(command);
				}
			}
		}

	}
}
