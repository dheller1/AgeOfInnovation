using AoICore.Players;

namespace AoICore.Buildings
{
	public interface IBuilding
	{
		IPlayer Owner { get; }
		BuildingType Type { get; }
	}
}
