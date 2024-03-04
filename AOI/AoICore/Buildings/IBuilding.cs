using AoICore.Players;
using static AoICore.Buildings.BuildingTypes;

namespace AoICore.Buildings
{
	public interface IBuilding
	{
		IPlayer Owner { get; }
		BuildingType Type { get; }
	}
}
