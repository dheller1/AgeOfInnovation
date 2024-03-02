using AoICore.Players;

namespace AoICore.Buildings
{
	public class Building : IBuilding
	{
		public Building(IPlayer owner, BuildingType type) {
			Owner = owner ?? throw new ArgumentNullException(nameof(owner));
			if(type == BuildingType.None) { throw new ArgumentException("BuildingType must not be None"); }
			Type = type;
		}

		public IPlayer Owner { get; }
		public BuildingType Type { get; }
	}
}
