using AoICore.AoIResources;
using AoICore.Map;

namespace AoICore.Players
{
	internal class Player : IPlayer
	{
		public Player(string name, Terrain terrain = Terrain.None) {
			Name = name;
			AssociatedTerrain = terrain;
		}

		public string Name { get; }
		public Terrain AssociatedTerrain { get; set; }

		public Coins Coins { get; set; } = (Coins)15;
		public Tools Tools { get; set; } = (Tools) 3;

		public override string ToString() => $"{Name} ({AssociatedTerrain})";
	}
}
