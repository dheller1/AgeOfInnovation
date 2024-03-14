using AoICore.AoIResources;
using AoICore.Buildings;
using AoICore.Map;
using AoICore.Players;

namespace AoICore.Commands
{
	public sealed class TerraformAndBuildCommand : IPlayerCommand
	{
		public TerraformAndBuildCommand(IPlayer player, IMap map, TerrainHex position) {
			Player = player ?? throw new ArgumentNullException(nameof(player));
			Map = map;
			Position = position ?? throw new ArgumentNullException(nameof(position));

			_cost = Terraform.GetTerraformAndBuildCost(Player, Position);
			_originalTerrain = position.Terrain;
		}

		public IPlayer Player { get; }
		public IMap Map { get; }
		public TerrainHex Position { get; }

		private readonly Cost _cost;
		private Terrain _originalTerrain;

		public bool CanExecute {
			get {
				if(Position.Building != null || Position.Terrain == Terrain.River) {
					return false;
				}
				else if(!Position.IsPlayerAdjacent(Player, Map)) {
					return false;
				}
				else if(!Player.Resources.CanPay(_cost)) {
					return false;
				}
				return true;
			}
		}

		public void Execute() {
			Player.Resources.Pay(_cost);
			Position.Terrain = Player.AssociatedTerrain;
			Position.Building = new Building(Player, BuildingTypes.Workshop);
		}

		public void Undo() {
			Position.Terrain = _originalTerrain;
			Position.Building = null;
			Player.Resources.Add(_cost);
		}
	}
}
