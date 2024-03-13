using AoICore.Buildings;
using AoICore.Players;
using Meeple.HexMap;
using Meeple.Util;

namespace AoICore.Map
{
	public class TerrainHex : NotificationBase, IHexField, IBuildSlot
	{
		public TerrainHex(Terrain terrain) {
			Terrain = terrain;
		}

		public override string ToString() => $"{nameof(TerrainHex)}({Terrain})";

		public int Q { get; set; }
		public int R { get; set; }
		public Terrain Terrain { get; }

		public bool IsPlayerAdjacent(IPlayer player, SmallMap map) {
			var adjacent = map.GetAdjacentHexes(this);
			return adjacent.Any(hex => hex.Controller == player);
		}

		public IBuilding? Building { get => _building; internal set => SetProperty(ref _building, value); }
		public IPlayer? Controller => Building?.Owner;
		
		private IBuilding? _building = null;
	}
}
