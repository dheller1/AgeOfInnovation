using AoICore.Buildings;
using AoICore.Player;
using Meeple.HexMap;

namespace AoICore.Map
{
	public class TerrainHex : IHexField, IBuildSlot
	{
		public TerrainHex(Terrain terrain) {
			Terrain = terrain;
		}

		public override string ToString() => $"{nameof(TerrainHex)}({Terrain})";

		public int Q { get; set; }
		public int R { get; set; }
		public Terrain Terrain { get; }

		public IBuilding? Building { get; set; } = null;
		public IPlayer? Controller => Building?.Owner;
	}
}
