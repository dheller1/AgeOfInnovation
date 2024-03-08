using AoICore.Buildings;
using AoICore.Map;
using AoICore.Players;
using static AoICore.Buildings.BuildingTypes;

namespace AoICore.Commands
{
	public sealed class UpgradeBuildingCommand : ICommand
	{
		public UpgradeBuildingCommand(IPlayer player, TerrainHex position, BuildingType upgradeType) {
			Player = player ?? throw new ArgumentNullException(nameof(player));
			Position = position ?? throw new ArgumentNullException(nameof(position));
			UpgradeType = upgradeType ?? throw new ArgumentNullException(nameof(upgradeType));
		}

		public IPlayer Player { get; }
		public TerrainHex Position { get; }
		public BuildingType UpgradeType { get; }

		private IBuilding? _replacedBuilding;

		public bool CanExecute {
			get {
				if(Player.AssociatedTerrain != Position.Terrain || Position.Building == null) {
					return false;
				}
				if(!Player.Resources.CanPay(UpgradeType.Cost)) {
					return false;
				}
				return true;
			}
		}

		public void Execute() {
			_replacedBuilding = Position.Building;
			Player.Resources.Pay(UpgradeType.Cost);
			Position.Building = new Building(Player, UpgradeType);

		}
		public void Undo() {
			Position.Building = _replacedBuilding;
			Player.Resources.Add(UpgradeType.Cost);
		}

		public override string ToString() => $"{nameof(UpgradeBuildingCommand)}";

	}
}
