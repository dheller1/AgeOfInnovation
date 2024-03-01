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

		public override string ToString() => $"{Name} ({AssociatedTerrain})";
	}
}
