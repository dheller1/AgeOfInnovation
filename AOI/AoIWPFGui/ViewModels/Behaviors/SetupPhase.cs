using AoICore.Buildings;
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
				if(CurrentState == null) { throw new InvalidOperationException(); }

				foreach(var cell in AssociatedObject.Cells) {
					if(cell.TerrainHex.Terrain == CurrentState.Player.AssociatedTerrain) {
						cell.PreviewBuildingOnMouseOver = BuildingType.Workshop;
						cell.BuildingOpacity = 0.7;
						cell.Opacity = 1.0;
					}
					else {
						ResetCell(cell);
					}
				}
			}
			else if(wasActive) {
				foreach(var cell in AssociatedObject.Cells) {
					ResetCell(cell);
				}
			}
		}

		private static void ResetCell(TerrainHexViewModel cell) {
			cell.ResetPreviewBuilding();
			cell.Opacity = 0.25;
			cell.BuildingOpacity = 1.0;
		}
	}
}
