using AoICore.Players;
using static AoICore.Buildings.BuildingTypes;

namespace AoICore.Buildings
{
	public class Building : IBuilding
	{
		public Building(IPlayer owner, BuildingType type) {
			Owner = owner ?? throw new ArgumentNullException(nameof(owner));
			Type = type;
		}

		public IPlayer Owner { get; }
		public BuildingType Type { get; }
	}
}
