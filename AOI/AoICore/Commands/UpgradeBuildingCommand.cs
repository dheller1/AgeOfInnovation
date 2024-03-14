using AoICore.Buildings;
using AoICore.Map;
using AoICore.Players;
using static AoICore.Buildings.BuildingTypes;

namespace AoICore.Commands
{
	public sealed class UpgradeBuildingCommand : IPlayerCommand
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
				if(!CanExecute_IgnoreCost) {
					return false;
				}
				return Player.Resources.CanPay(UpgradeType.Cost);
			}
		}

		public bool CanExecute_IgnoreCost {
			get {
				if(Player.AssociatedTerrain != Position.Terrain || Position.Building == null) {
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
