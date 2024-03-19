using AoICore.AoIResources;
using AoICore.Map;

namespace AoICore.Players
{
	internal class Player : IPlayer
	{
		public Player(string name, Terrain terrain = Terrain.None) {
			Name = name;
			AssociatedTerrain = terrain;
			Resources.Add((Coins)15);
			Resources.Add((Tools) 6);
		}

		public string Name { get; }
		public Terrain AssociatedTerrain { get; set; }

		public Supply Resources { get; } = new();
		public PowerSupply Power { get; } = new();
		public VictoryPoints VictoryPoints { get; set; } = (VictoryPoints)20;

		public int TerraformingLevel => 0;

		public override string ToString() => $"{Name} ({AssociatedTerrain})";
	}
}
