using AoICore.AoIResources;
using AoICore.Map;

namespace AoICore.Players
{
	public interface IPlayer
	{
		string Name { get; }
		Terrain AssociatedTerrain { get; }
		Supply Resources { get; }
		PowerSupply Power { get; }
		VictoryPoints VictoryPoints { get; set; }

		int TerraformingLevel { get; }
	}
}
